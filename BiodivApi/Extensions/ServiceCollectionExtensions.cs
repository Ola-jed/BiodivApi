using System;
using BiodivApi.Data.Context;
using BiodivApi.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace BiodivApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureCors(this IServiceCollection serviceCollection,
            string policyName)
        {
            serviceCollection.AddCors(options =>
            {
                options.AddPolicy(policyName,
                    corsPolicyBuilder =>
                    {
                        corsPolicyBuilder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }

        public static void ConfigureSwagger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.OperationFilter<ApiKeyOperationFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BiodivApi",
                    Version = "v1",
                    Description = "An api to view information about animal species",
                    Contact = new OpenApiContact
                    {
                        Name = "Olabissi Gbangboche",
                        Email = "olabijed@gmail.com",
                        Url = new Uri("https://github.com/Ola-jed")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under GPLv3.0",
                        Url = new Uri("https://github.com/Ola-jed/getThis/blob/master/LICENSE")
                    }
                });
            });
        }

        public static void ConfigurePgsql(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                ConnectionString = configuration.GetConnectionString("DefaultConnection"),
                Password = configuration["Password"],
                Username = configuration["UserId"]
            };
            serviceCollection.AddDbContext<BiodivDbContext>(opt => opt.UseNpgsql(builder.ConnectionString));
        }
    }
}