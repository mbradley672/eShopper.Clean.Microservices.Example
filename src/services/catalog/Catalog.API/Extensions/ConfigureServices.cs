using System.Reflection;
using Catalog.Application.Handlers;
using Catalog.Application.Mappers;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace Catalog.API.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddControllers();
        services.AddApiVersioning();
        services.AddHealthChecks()
            .AddMongoDb(configuration["DatabaseSettings:ConnectionString"], 
                name: "Catalog MongoDb Health Check", HealthStatus.Degraded, timeout: TimeSpan.FromSeconds(3), tags: new[] { "ready" });
        services.AddSwaggerGen((c)=>{
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
        });
        services.AddAutoMapper(c=>c.AddProfile<ProductMappingProfile>());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateProductHandler>());

        // Context and Repositories
        services.AddScoped<ICatalogContext, CatalogContext>();
        services.AddRepositories();
        
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, ProductRepository>();
        services.AddScoped<ITypesRepository, ProductRepository>();

        return services;
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
        }

        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        });
        

        return app;
    }
}