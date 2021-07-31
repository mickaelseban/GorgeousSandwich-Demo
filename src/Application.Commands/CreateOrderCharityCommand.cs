namespace Application.Commands
{
    using System;
    using System.Collections.Generic;
    using BasicWrapperTool;
    using MediatR;

    public class CreateOrderCharityCommand : IRequest<Result>
    {
        public CreateOrderCharityCommand(DateTime deliveryDate,
            string institutionId,
            string institutionType,
            string institutionName,
            string institutionEmail,
            string institutionTaxNumber,
            IEnumerable<Product> products)
        {
            DeliveryDate = deliveryDate;
            InstitutionId = institutionId;
            InstitutionType = institutionType;
            InstitutionName = institutionName;
            InstitutionEmail = institutionEmail;
            InstitutionTaxNumber = institutionTaxNumber;
            Products = products;
        }

        public DateTime DeliveryDate { get; }
        public string InstitutionId { get; }
        public string InstitutionType { get; set; }
        public string InstitutionName { get; }
        public string InstitutionEmail { get; }
        public string InstitutionTaxNumber { get; }
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