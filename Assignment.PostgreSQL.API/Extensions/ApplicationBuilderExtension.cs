using Assignment.Data.Contexts;
using Assignment.Shared.Constants;
using Assignment.Shared.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using static Assignment.Shared.Constants.Enum;
using System.Net;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Assignment.PostgreSQL.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void EnsureNSqlDatabase(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var isNewDb = false;
                using (var dbContext = scope.ServiceProvider.GetRequiredService<PostgreSqlContext>())
                {
                    isNewDb = dbContext.Database.EnsureCreated();
                }
                using (var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>())
                {
                    if (!roleManager.RoleExistsAsync(RoleName.Guest).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = RoleName.Guest,
                            NormalizedName = RoleName.Guest,
                        }).Wait();
                    }
                    if (!roleManager.RoleExistsAsync(RoleName.Admin).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = RoleName.Admin,
                            NormalizedName = RoleName.Admin,
                        }).Wait();
                    }
                }
                using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>())
                {
                    if (userManager.FindByNameAsync("string").Result is null)
                    {
                        var adminAccount = new IdentityUser()
                        {
                            UserName = "string",
                        };
                        userManager.CreateAsync(adminAccount, "string").Wait();
                        userManager.AddToRoleAsync(adminAccount, RoleName.Admin).Wait();
                    }
                }
            }
        }
        public static void ConfigGlobalException<TApplicationBuilder>(this TApplicationBuilder applicationBuilder) where TApplicationBuilder : IApplicationBuilder
        {
            applicationBuilder.UseExceptionHandler(config =>
            {
                config.Run(async handler =>
                {
                    if (((int)HttpStatusCode.InternalServerError).Equals(handler.Response.StatusCode))
                    {
                        handler.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
                        var contextFeature = handler.Features.Get<IExceptionHandlerFeature>();
                        var httpStatusCode = (int)HttpStatusCode.BadRequest;
                        object? response = null;
                        if (contextFeature != null && contextFeature.Error is DbUpdateException dbUpdateException)
                        {
                            response = new FailActionResponse<object>()
                            {
                                Data = dbUpdateException.Data,
                                ErrorCode = ErrorCode.InvalidObject,
                                ErrorMessage = dbUpdateException.Message,
                                SubErrorCodeString = dbUpdateException.InnerException.Message
                            };
                        }
                        else if (contextFeature != null && contextFeature.Error is not null)
                        {
                            response = new FailActionResponse()
                            {
                                ErrorCode = ErrorCode.System,
                                ErrorMessage = contextFeature.Error.Message
                            };
                        }
                        if (response is not null)
                        {
                            handler.Response.StatusCode = httpStatusCode;
                            await handler.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings()
                            {
                                Formatting = Formatting.None
                            }));
                        }
                    }
                });
            });
        }
    }
}
