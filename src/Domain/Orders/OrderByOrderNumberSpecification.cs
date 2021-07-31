namespace Domain.Orders
{
    using System;
    using System.Linq.Expressions;
    using Infrastructure.CrossCutting.Specification.Implementation;

    public class OrderByOrderNumberSpecification : Specification<Order>
    {
        private readonly Id _orderNumber;

        public OrderByOrderNumberSpecification(Id orderNumber)
        {
            _orderNumber = orderNumber;
        }

        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.OrderNumber.Equals(_orderNumber);
        }
    }
}