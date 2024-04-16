using Assignment.MongoService.Abstractions;
using Assignment.MongoService.Implements;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.MongoService
{
    public static class BootstrapperExtension
    {
        public static void RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
