using Assignment.Business.Abstractions;
using Assignment.Business.Implements;
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

namespace Assignment.Business
{
    public static class BootstrapperExtension
    {
        public static void RegisterBusinesses(this IServiceCollection services)
        {
            services.AddScoped<IAuthBusiness, AuthBusiness>();
            services.AddScoped<ICategoryBusiness, CategoryBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
        }
        public static void RegisterMongoBusinesses(this IServiceCollection services)
        {
            services.AddScoped<IAuthBusiness, AuthBusiness>();
            services.AddScoped<Assignment.Business.Abstractions.Mongo.ICategoryBusiness, Assignment.Business.Implements.Mongo.CategoryBusiness>();
            services.AddScoped<Assignment.Business.Abstractions.Mongo.IProductBusiness, Assignment.Business.Implements.Mongo.ProductBusiness>();
        }
        public static void RegisterProviders(this IServiceCollection services)
        {
            services.AddScoped<ICoreProvider, CoreProvider>();
            services.AddScoped<IIdentityProvider, IdentityProvider>();
        }
    }
}
