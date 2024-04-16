using Assignment.Data.Models.MongoModels;
using Assignment.Data.Repositories.Abstractions.Mongo;
using Assignment.Service.Abstractions.Mongo;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests.Product;
using Assignment.Shared.Responses.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Implements.Mongo
{
    public class ProductService : ServiceBase<Product, ProductAddRequest, ProductResponse, ProductGetRequest>, IProductService
    {
        public ProductService(IProductRepository repository, ICoreProvider coreProvider) : base(repository, coreProvider)
        {
        }
    }
}
