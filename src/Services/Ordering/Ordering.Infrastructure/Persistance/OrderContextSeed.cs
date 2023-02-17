using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetData());
                await context.SaveChangesAsync();
                logger.LogInformation("data seed.");
            }
        }

        public static List<Order> GetData()
        {
            return new List<Order>()
            {
                new(){UserName = "ashkan_mv",FirstName = "ashkan",LastName = "m"}
            };
        }
    }
}
