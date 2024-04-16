using Assignment.Data.Models;
using Assignment.Data.Profiles.ProductMappingAction;
using Assignment.Shared.Requests.Product;
using Assignment.Shared.Responses.Product;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductAddRequest, Product>();
            CreateMap<Product, ProductResponse>()
                .AfterMap<ProductToProductResponse>();
        }
    }
}
