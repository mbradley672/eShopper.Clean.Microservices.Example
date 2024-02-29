using System.Reflection;
using Discount.API.Services;
using Discount.Application.Commands;
using Discount.Application.Mapper;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Discount.Infrastructure.Repository;
using Npgsql;

namespace Discount.API.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();

        services.AddGrpc();
        services.AddAutoMapper(cfg=> cfg.AddProfile<DiscountMapper>());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DeleteDiscountCommand>());
        
        return services;
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount.API v1"));
        }
        
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        
#pragma warning disable ASP0014
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<DiscountService>();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync(
                    "Communication with gRPC endpoints must be made through a gRPC client. " +
                    "To learn how to create a client, " +
                    "visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            });
        });
#pragma warning enable ASP0014
        
        return app;
    } 
    
    
    public static WebApplication MigrateDatabase(this WebApplication host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger>();
        var config = services.GetRequiredService<IConfiguration>();
            
        try
        {
            logger.LogInformation("Migrating database associated with context");
                
            ApplyMigrations(config);
                
            logger.LogInformation("Migrated database associated with context");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context");
        }

        return host;
    }

    private static void ApplyMigrations(IConfiguration config)
    {
        using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
        connection.Open();
        
        using var command = new NpgsqlCommand
        {
            Connection = connection
        };
        
        command.CommandText = "DROP TABLE IF EXISTS Coupon";
        command.ExecuteNonQuery();
        command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, ProductName VARCHAR(500) NOT NULL, Description TEXT, Amount INT)";
        command.ExecuteNonQuery();
        
        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500);";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Discount', 700);";
        command.ExecuteNonQuery();
    }
}