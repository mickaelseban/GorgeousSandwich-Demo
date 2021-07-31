namespace Application.Queries
{
    using System.Collections.Generic;
    using BasicWrapperTool;
    using MediatR;

    public class GetCustomerOrdersQuery : IRequest<Result<IEnumerable<OrderDto>>>
    {
        public GetCustomerOrdersQuery(string customerNumber)
        {
            CustomerNumber = customerNumber;
        }

        public string CustomerNumber { get; }
    }
}