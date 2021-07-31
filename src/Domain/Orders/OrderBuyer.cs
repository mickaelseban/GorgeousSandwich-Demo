namespace Domain.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BasicWrapperTool;

    public class OrderBuyer : Order
    {
        public OrderBuyer(Id orderNumber, IEnumerable<Product> products, Student student, OrderStatus status)
            : base(orderNumber, products, student, status)
        {
        }

        public override Money TotalPrice => OrderItems.Aggregate(Money.Zero, (current, orderItem) => current + orderItem.Price);

        public override OrderType Type => OrderType.Buyer;

        public Student Student => (Student)Customer;

        public Result Cancel()
        {
            var cancelResult = Status.Cancel();

            if (cancelResult.IsSuccess)
            {
                Status = cancelResult.Value;
            }

            return cancelResult.ToResult();
        }

        public Result ChangeScheduledDate(DateTime deliveryDate)
        {
            var changeScheduledDateResult = Status.ChangeScheduledDate(deliveryDate);

            if (changeScheduledDateResult.IsSuccess)
            {
                Status = changeScheduledDateResult.Value;
            }

            return changeScheduledDateResult.ToResult();
        }
    }
}