using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace Catalog.API.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddApiVersioning();
        services.AddHealthChecks()
            .AddMongoDb(configuration["DatabaseSettings:ConnectionString"], 
                name: "Catalog MongoDb Health Check", HealthStatus.Degraded, timeout: TimeSpan.FromSeconds(3), tags: new[] { "ready" });
        services.AddSwaggerGen((c)=>{
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
        });
        
        
        return services;
    }
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        

        return app;
    }
}