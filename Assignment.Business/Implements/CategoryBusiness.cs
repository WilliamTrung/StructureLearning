using Assignment.Business.Abstractions;
using Assignment.Data.Models;
using Assignment.Data.UnitOfWork;
using Assignment.Service.Abstractions;
using Assignment.Service.Implements;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests.Category;
using Assignment.Shared.Responses;
using Assignment.Shared.Responses.Category;
using Assignment.Shared.Responses.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Business.Implements
{
    public class CategoryBusiness : BusinessBase, ICategoryBusiness
    {
        protected readonly ICategoryService _categoryService;
        public CategoryBusiness(IUnitOfWorkService unitOfWorkService, ICoreProvider coreProvider, ICategoryService categoryService) : base(unitOfWorkService, coreProvider)
        {
            _categoryService = categoryService;
        }

        public async Task<CategoryResponse> Create(CategoryAddRequest request)
        {
            var result = await _categoryService.InsertAsync(request);
            await _unitOfWorkService.SaveChangesAsync();
            return _mapper.Map<CategoryResponse>(result);
        }

        public async Task Delete(string id)
        {
            _categoryService.SoftDeleteById(id);
            await _unitOfWorkService.SaveChangesAsync();
        }

        public async Task<PagedResult<CategoryResponse>> GetPaged(CategoryGetRequest request)
        {
            var categories = await _categoryService.GetAllPagingAsync(request);
            return categories;
        }
        public async Task<IEnumerable<CategoryResponse>> GetAll()
        {
            var entities = await _categoryService.FindAsync();
            var parent = entities.Where(c => c.ParentId == null);
            var result = _mapper.Map<IEnumerable<CategoryResponse>>(parent);
            result = result.Where(c => c.Parent == null);
            return result;
        }

        public async Task<CategoryResponse> Update(CategoryUpdateRequest request)
        {
            var entity = await _categoryService.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException("id not found for: " + request.Id);
            }
            if (request.Name != null)
            {
                entity.Name = request.Name;
            }
            if (request.ParentId != null)
            {
                entity.ParentId = request.ParentId;
            }
            _categoryService.Update(entity);
            await _unitOfWorkService.SaveChangesAsync();
            return _mapper.Map<CategoryResponse>(entity); ;
        }
    }
}
