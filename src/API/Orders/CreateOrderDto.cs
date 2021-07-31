namespace API.Orders
{
    using System;
    using System.Collections.Generic;

    public class CreateOrderDto
    {
        public DateTime DeliveryDate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerTaxNumber { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        public class ProductDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Ingredients { get; set; }
            public decimal Price { get; set; }
        }
    }
}