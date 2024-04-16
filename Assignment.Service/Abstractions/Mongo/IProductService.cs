using Assignment.Data.Models.MongoModels;
using Assignment.Shared.Requests.Product;
using Assignment.Shared.Responses.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Abstractions.Mongo
{
    public interface IProductService : IService<Product, ProductAddRequest, ProductResponse, ProductGetRequest>
    {
    }
}
