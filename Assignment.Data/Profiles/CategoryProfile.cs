using Assignment.Data.Models;
using Assignment.Data.Profiles.CategoryMappingAction;
using Assignment.Shared.Requests.Category;
using Assignment.Shared.Responses.Category;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddRequest, Category>();
            CreateMap<Category, CategoryResponse>().AfterMap<CategoryToCategoryResponse>();
            CreateMap<Category, CategoryProductResponse>();
        }
    }
}
