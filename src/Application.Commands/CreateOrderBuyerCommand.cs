namespace Application.Commands
{
    using System;
    using System.Collections.Generic;
    using BasicWrapperTool;
    using MediatR;

    public class CreateOrderBuyerCommand : IRequest<Result>
    {
        public CreateOrderBuyerCommand(DateTime deliveryDate,
            string customerId,
            string customerType,
            string customerName,
            string customerEmail,
            string customerTaxNumber,
            IEnumerable<Product> products)
        {
            DeliveryDate = deliveryDate;
            CustomerId = customerId;
            CustomerType = customerType;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerTaxNumber = customerTaxNumber;
            Products = products;
        }

        public DateTime DeliveryDate { get; }
        public string CustomerId { get; }
        public string CustomerType { get; set; }
        public string CustomerName { get; }
        public string CustomerEmail { get; }
        public string CustomerTaxNumber { get; }
        public IEnumerable<Product> Products { get; }

        public class Product
        {
            public Product(Guid productId,
                string name,
                string description,
                string ingredients,
                decimal price)
            {
                ProductId = productId;
                Name = name;
                Description = description;
                Ingredients = ingredients;
                Price = price;
            }

            public Guid ProductId { get; }
            public string Name { get; }
            public string Description { get; }
            public string Ingredients { get; }
            public decimal Price { get; }
        }
    }
}