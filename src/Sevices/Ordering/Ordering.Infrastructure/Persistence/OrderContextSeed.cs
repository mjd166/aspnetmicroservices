using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreConfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("seed database associated with context {DbContextName}", typeof(Order).Name);
            }
        }

        public static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>
            {
                new Order(){UserName ="swm",FirstName ="Majid",LastName ="Ghafari",EmailAddress="mghafari41@yahoo.com",Country ="Iran",TotalPrice =550}
            };

        }
    }
}
