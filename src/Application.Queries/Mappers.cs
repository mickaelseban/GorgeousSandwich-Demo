namespace Application.Queries
{
    using System.Linq;
    using Domain.Orders;

    internal static class Mappers
    {
        public static OrderDto Map(Order domain)
        {
            return new OrderDto
            {
                CreationDate = domain.Status.CreationDate,
                ScheduledDate = domain.Status.ScheduledDate,
                DeliveryDate = domain.Status.DeliveryDate,
                OrderType = domain.Type.ToString(),
                TotalPriceOrder = domain.TotalPrice,
                OrderItems = domain.OrderItems.Select(Map)
            };
        }

        private static OrderItemDto Map(OrderItem domain)
        {
            return new OrderItemDto
            {
                Product = Map(domain.Product),
                Quantity = (int)domain.Quantity,
            };
        }

        private static ProductDto Map(Product domain)
        {
            return new ProductDto
            {
                Description = domain.Description,
                Name = domain.Name,
                Price = domain.Price
            };
        }
    }
}