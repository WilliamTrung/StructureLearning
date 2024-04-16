using Amazon.Runtime.Internal;
using Assignment.Business.Abstractions.Mongo;
using Assignment.Data.Models.MongoModels;
using Assignment.Data.UnitOfWork;
using Assignment.Service.Abstractions.Mongo;
using Assignment.Service.Implements.Mongo;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests.Category;
using Assignment.Shared.Responses;
using Assignment.Shared.Responses.Category;
using Assignment.Shared.Responses.Product;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Business.Implements.Mongo
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
            if (!await _categoryService.ValidateId(request.ParentId))
            {
                throw new InvalidDataException("No foreign id for: " + request.ParentId);
            }
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
        private void GetChildren(ref Category category, in List<Category> categories)
        {
            var sample = category;
            var children = categories.Where(child => child.ParentId == sample.Id).ToArray();
            if (children.Any())
            {
                for (int i = 0; i < children.Count(); i++)
                {
                    var child = children[i];
                    GetChildren(ref child, categories);
                }
                category.Children = children.ToList();
                //category.Children.ForEach(child => child.Parent = sample);
            }
        }
        public async Task<IEnumerable<CategoryResponse>> GetAll()
        {
            var entities = await _categoryService.FindAsync();
            var categories = entities.ToArray();
            var parent = entities.Where(c => c.ParentId == null).ToArray();
            for (int i = 0; i < parent.Count(); i++)
            {
                var category = parent[i];
                GetChildren(ref category, categories.ToList());
            }
            var result = _mapper.Map<IEnumerable<CategoryResponse>>(parent);
            return result;

            //throw new NotImplementedException();
        }

        public async Task<CategoryResponse> Update(CategoryUpdateRequest request)
        {
            if (!await _categoryService.ValidateId(request.ParentId))
            {
                throw new InvalidDataException("No foreign id for: " + request.ParentId);
            }
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
