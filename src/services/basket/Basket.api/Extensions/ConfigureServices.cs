using Basket.Application.Handlers;
using Basket.Application.Mappers;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
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
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
        });
        services.AddHealthChecks().AddRedis(configuration.GetValue<string>("CacheSettings:ConnectionString"), "Redis Cache", HealthStatus.Degraded);
        services.AddSwaggerGen((c)=>{
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
        });
        services.AddAutoMapper(c=>c.AddProfile<MappingProfile>());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateShoppingCartCommandHandler>());
        services.AddRepositories();
        
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBasketRepository, BasketRepository>();

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
#pragma warning disable ASP0014
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        });
#pragma warning restore ASP0014
        

        return app;
    }
}