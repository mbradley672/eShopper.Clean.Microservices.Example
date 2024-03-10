using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", nameof(OrderContext));
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>()
        {
            new Order()
            {
                UserName = "system",
                FirstName = "Melvin",
                LastName = "Bradley",
                EmailAddress = "admin@eshop.net",
                AddressLine = "123 Main St",
                Country = "USA",
                TotalPrice = 750,
                State = "VA",
                ZipCode = "23294",

                CardName = "Visa",
                CardNumber = "1234567890123456",
                CreatedBy = "system",
                Expiration = "12/25",
                Cvv = "123",
                PaymentMethod = 1,
                LastModifiedBy = "system",
                LastModifiedDate = new DateTime(),
            }
        };
    }
}