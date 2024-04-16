using Assignment.Data.Repositories.Abstractions;
using Assignment.Data.Repositories.Implements;
using Assignment.Data.UnitOfWork;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Provider.Implements;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data
{
    public static class BootstrapperExtension
    {
        public static void RegisterRepositoryDepedencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
        public static void RegisterMongoRepositoryDepedencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            services.AddScoped<Assignment.Data.Repositories.Abstractions.Mongo.ICategoryRepository, Assignment.Data.Repositories.Implements.Mongo.CategoryRepository>();
            services.AddScoped<Assignment.Data.Repositories.Abstractions.Mongo.IProductRepository, Assignment.Data.Repositories.Implements.Mongo.ProductRepository>();
        }
    }
}
