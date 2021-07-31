namespace Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderDto
    {
        public string OrderType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; } = Enumerable.Empty<OrderItemDto>();
        public decimal TotalPriceOrder { get; set; }
    }
}