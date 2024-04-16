using Assignment.Data.Models.MongoModels;
using Assignment.Shared.Responses.Category;
using Assignment.Shared.Responses.Product;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Profiles.ProductMappingAction

{
    public class MongoProductToProductResponse : IMappingAction<Product, ProductResponse>
    {
        private readonly IMapper _mapper;
        public MongoProductToProductResponse(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void Process(Product source, ProductResponse destination, ResolutionContext context)
        {
            destination.IsActive = source.IsActive;
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.CreatedAt = source.CreatedAt;
            destination.CreatedBy = source.CreatedBy;
            destination.LastUpdatedAt = source.LastUpdatedAt;
            destination.LastUpdatedBy = source.LastUpdatedBy;
            destination.Id = source.Id;
            destination.Categories = _mapper.Map<List<CategoryProductResponse>>(source.Categories);
        }
    }
}
