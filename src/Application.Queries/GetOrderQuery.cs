namespace Application.Queries
{
    using BasicWrapperTool;
    using MediatR;

    public class GetOrderQuery : IRequest<Result<OrderDto>>
    {
        public GetOrderQuery(string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public string OrderNumber { get; }
    }
}