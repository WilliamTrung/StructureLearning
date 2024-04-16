using Assignment.Business.Abstractions;
using Assignment.Data.Models;
using Assignment.Data.UnitOfWork;
using Assignment.Service.Abstractions;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests.Product;
using Assignment.Shared.Responses;
using Assignment.Shared.Responses.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Business.Implements
{
    public class ProductBusiness : BusinessBase, IProductBusiness
    {
        protected readonly ICategoryService _categoryService;
        protected readonly IProductService _productService;
        public ProductBusiness(IUnitOfWorkService unitOfWorkService, ICoreProvider coreProvider, IProductService productService, ICategoryService categoryService) : base(unitOfWorkService, coreProvider)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<ProductResponse> Create(ProductAddRequest request)
        {
            var result = await _productService.InsertAsync(request);
            try
            {
                await _unitOfWorkService.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return _mapper.Map<ProductResponse>(result);
        }

        public async Task Delete(string id)
        {
            _productService.SoftDeleteById(id);
            await _unitOfWorkService.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductResponse>> GetPaged(ProductGetRequest request)
        {
            var products = await _productService.GetAllPagingAsync(request);
            return products;
        }

        public async Task<IEnumerable<ProductResponse>> GetAll()
        {
            var entities = await _productService.FindAsync();
            foreach (var entity in entities)
            {
                entity.Categories = new List<Category>();
                var category = await _categoryService.FirstOrDefaultAsync(expression: c => c.Id == entity.CategoryId);
#pragma warning disable CS8604 // Possible null reference argument.
                entity.Categories.Add(category);
                //loop if category has parent then find and add to list
                while (category != null && category.ParentId != null)
                {
                    string parentId = category.ParentId;
                    category = await _categoryService.FirstOrDefaultAsync(expression: c => c.Id == parentId);
                    entity.Categories.Add(category);
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            var result = _mapper.Map<IEnumerable<ProductResponse>>(entities);
            return result;
        }

        public async Task<ProductResponse> Update(ProductUpdateRequest request)
        {
            var entity = await _productService.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("id not found for: " + request.Id);
            }
            if (request.Name != null)
            {
                entity.Name = request.Name;
            }
            if (request.Description != null)
            {
                entity.Description = request.Description;
            }
            if (request.CategoryId != null)
            {
                entity.CategoryId = request.CategoryId;
            }
            _productService.Update(entity);
            await _unitOfWorkService.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(entity); ;
        }

        public async Task<ProductResponse> UpdateCategory(ProductUpdateCategoryRequest request)
        {
            var entity = await _productService.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("id not found for: " + request.Id);
            }
            if (request.CategoryId != null)
            {
                entity.Name = request.CategoryId;
            }
            _productService.Update(entity);
            await _unitOfWorkService.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(entity); ;
        }
    }
}
