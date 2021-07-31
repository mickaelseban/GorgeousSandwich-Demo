namespace Domain.Orders
{
    using System.Collections.Generic;

    public class OrderCharity : Order
    {
        public OrderCharity(Id orderNumber, IEnumerable<Product> products, Institution institution, OrderStatus status)
            : base(orderNumber, products, institution, status)
        {
        }

        public Institution Institution => (Institution)Customer;

        public override OrderType Type => OrderType.Buyer;

        public override Money TotalPrice => Money.Zero;
    }
}