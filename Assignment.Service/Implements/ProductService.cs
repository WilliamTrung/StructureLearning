using Assignment.Data.Models;
using Assignment.Data.Repositories.Abstractions;
using Assignment.Service.Abstractions;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests.Product;
using Assignment.Shared.Responses.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Implements
{
    public class ProductService : ServiceBase<Product, ProductAddRequest, ProductResponse, ProductGetRequest>, IProductService
    {
        public ProductService(IProductRepository repository, ICoreProvider coreProvider) : base(repository, coreProvider)
        {
        }
    }
}
