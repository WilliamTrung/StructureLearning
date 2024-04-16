using Assignment.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Extensions
{
    public static class DbContextExtension
    {
        public static void RegisterMongoDbContext(this IServiceCollection services, in IConfiguration configuration)
        {
            var host = configuration.GetConnectionString("Host");
            var db_name = configuration.GetConnectionString("DatabaseName");
            var identity_connection = configuration.GetConnectionString("Identity");
            if (host == null || db_name == null)
            {
                throw new Exception("MongoDB connection string is not configured.");
            }
            services.AddDbContextPool<MongoContext>(options =>
            {
                options.UseMongoDB(host, db_name);
            });
            services.AddDbContextPool<IdentityContext>(options =>
            {
                options.UseNpgsql(identity_connection);
            });
        }
        public static void RegisterPostgreSqlDbContext(this IServiceCollection services, in IConfiguration configuration)
        {
            var connectionStr = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<PostgreSqlContext>(options =>
            {
                options.UseNpgsql(connectionStr);
            });
        }
    }
}
