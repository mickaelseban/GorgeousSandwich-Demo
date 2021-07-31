namespace Domain.Orders
{
    using System.Collections.Generic;
    using System.Linq;
    using BasicWrapperTool;
    using SeedWork;

    public abstract class Order : Aggregate, IAggregateRoot
    {
        protected Order(Id orderNumber, IEnumerable<Product> products, Customer customer, OrderStatus status)
        {
            OrderNumber = orderNumber;
            Customer = customer;
            Status = status;
            OrderItems = GenerateOrderItems(products);
        }

        public abstract OrderType Type { get; }
        public abstract Money TotalPrice { get; }
        public IEnumerable<OrderItem> OrderItems { get; }
        protected internal Customer Customer { get; }
        public OrderStatus Status { get; protected set; }

        private string _orderNumber;

        public Id OrderNumber
        {
            get => (Id)_orderNumber;
            protected set => _orderNumber = value;
        }

        public Result Delivery()
        {
            var deliveryResult = Status.Delivery();

            if (deliveryResult.IsSuccess)
            {
                Status = deliveryResult.Value;
            }

            return deliveryResult.ToResult();
        }

        private static IEnumerable<OrderItem> GenerateOrderItems(IEnumerable<Product> products)
        {
            return products
                .GroupBy(product => product)
                .Select(group => new OrderItem((uint)group.Count(), group.Key));
        }

        public enum OrderType
        {
            Buyer = 0,
            Charity = 1
        }
    }
}