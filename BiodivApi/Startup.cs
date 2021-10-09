using System;
using BiodivApi.Data.Repositories;
using BiodivApi.Extensions;
using BiodivApi.Services.ApiKeyService;
using BiodivApi.Services.LocalDistributionsService;
using BiodivApi.Services.LocalNamesService;
using BiodivApi.Services.SpeciePhotoComparisonService;
using BiodivApi.Services.SpeciesPhotosService;
using BiodivApi.Services.SpeciesService;
using BiodivApi.Services.StorageService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BiodivApi
{
    public class Startup
    {
        private const string OriginsAllowed = "_originsAllowed";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigurePgsql(Configuration);
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
            services.AddScoped<ISpecieRepository, SpecieRepository>();
            services.AddScoped<ISpeciePhotoRepository, SpeciePhotoRepository>();
            services.AddScoped<ILocalNameRepository, LocalNameRepository>();
            services.AddScoped<IApiKeyService, ApiKeyService>();
            services.AddScoped<ISpecieService, SpecieService>();
            services.AddScoped<IStorageService, LocalStorageService>();
            services.AddScoped<ILocalNamesService, LocalNamesService>();
            services.AddScoped<ISpeciePhotoService, SpeciePhotoService>();
            services.AddScoped<ISpeciePhotoComparisonService, DeltaEComparisonService>();
            services.AddScoped<ILocalDistributionsService, LocalDistributionsService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMemoryCache();
            services.AddControllers();
            services.ConfigureCors(OriginsAllowed);
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.InjectStylesheet("/themes/theme-dark.css");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biodiv v1");
                });
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(OriginsAllowed);
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}