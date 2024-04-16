using Assignment.Data.Models.MongoModels;
using Assignment.Shared.Responses.Category;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Profiles.CategoryMappingAction
{
    public class MongoCategoryToCategoryResponse : IMappingAction<Category, CategoryResponse>
    {
        private readonly IMapper _mapper;
        public MongoCategoryToCategoryResponse(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Process(Category source, CategoryResponse destination, ResolutionContext context)
        {
            destination.IsActive = source.IsActive;
            destination.Name = source.Name;
            destination.CreatedAt = source.CreatedAt;
            destination.CreatedBy = source.CreatedBy;
            destination.LastUpdatedAt = source.LastUpdatedAt;
            destination.LastUpdatedBy = source.LastUpdatedBy;
            destination.Id = source.Id;
            if (source.Children != null)
            {
                destination.Children = new List<CategoryResponse>();
                foreach (var child in source.Children)
                {
                    //child.Parent = null;
                    //child.Children = null;
                    var childModel = _mapper.Map<CategoryResponse>(child);
                    destination.Children.Add(childModel);
                }
                //destination.Children = _mapper.Map<List<CategoryResponse>>(source.Children);
            }
            //else if (source.Parent != null)
            //{
            //    source.Parent.Children = null;
            //    source.Children = null;
            //    destination.Parent = _mapper.Map<CategoryResponse>(source.Parent);
            //}
        }
    }
}
