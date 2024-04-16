using Assignment.Data.Models;
using Assignment.Shared.Requests.Product;
using Assignment.Shared.Responses.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service.Abstractions
{
    public interface IProductService : IService<Product, ProductAddRequest, ProductResponse, ProductGetRequest>
    {
    }
}
