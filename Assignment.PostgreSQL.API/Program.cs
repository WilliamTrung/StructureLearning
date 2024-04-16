using Assignment.Data;
using Assignment.Data.Extensions;
using Assignment.Service;
using Assignment.Business;
using Assignment.Data.Profiles;
using Assignment.PostgreSQL.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Settings;
using Assignment.Service.Abstractions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Assignment.Shared.Responses;
using static Assignment.Shared.Constants.Enum;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterPostgreSqlDbContext(builder.Configuration);
builder.Services.RegisterPostgreSqlIdentityAuth(builder.Configuration);
builder.Services.RegisterProviders();
builder.Services.RegisterRepositoryDepedencies();
builder.Services.RegisterServiceDependencies();
builder.Services.RegisterBusinesses();
// Config Auth service
var jwtSection = builder.Configuration.GetRequiredSection("JwtSetting");
builder.Services.Configure<JwtSetting>(jwtSection);
var jwtSetting = jwtSection.Get<JwtSetting>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSetting.Issuer,
            ValidAudience = jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key))
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = ctx =>
            {
                if (ctx.Principal.Identity.IsAuthenticated)
                {
                    IIdentityProvider provider = ctx.HttpContext.RequestServices.GetRequiredService<IIdentityProvider>();
                    provider.UpdateIdentity(ctx.Principal);
                }

                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();
builder.Services.Configure<ApiBehaviorOptions>(option =>
{
    option.InvalidModelStateResponseFactory = actionContext => new BadRequestObjectResult(new FailActionResponse()
    {
        ErrorCode = ErrorCode.InvalidInput,
        ErrorMessage = actionContext.ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
// Add swagger configuration for auth feature
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    // Define security requirements
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigGlobalException();
app.UseAuthorization();

app.MapControllers();
app.Services.EnsureNSqlDatabase();
app.Run();
