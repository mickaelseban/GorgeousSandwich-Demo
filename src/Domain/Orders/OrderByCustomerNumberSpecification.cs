namespace Domain.Orders
{
    using System;
    using System.Linq.Expressions;
    using Infrastructure.CrossCutting.Specification.Implementation;

    public class OrderByCustomerNumberSpecification : Specification<Order>
    {
        private readonly Id _customerId;

        public OrderByCustomerNumberSpecification(Id customerId) => _customerId = customerId;

        public override Expression<Func<Order, bool>> ToExpression() => order => order.Customer.CustomerNumber == _customerId;
    }
}