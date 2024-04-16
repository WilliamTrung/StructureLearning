using Assignment.Service.Abstractions;
using Assignment.Service.Implements;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Service
{
    public static class BootstrapperExtension
    {
        public static void RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
        }
        public static void RegisterMongoServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<Assignment.Service.Abstractions.Mongo.ICategoryService, Assignment.Service.Implements.Mongo.CategoryService>();
            services.AddScoped<Assignment.Service.Abstractions.Mongo.IProductService, Assignment.Service.Implements.Mongo.ProductService>();
        }
    }
}
