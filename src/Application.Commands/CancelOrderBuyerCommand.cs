namespace Application.Commands
{
    using BasicWrapperTool;
    using MediatR;

    public class CancelOrderBuyerCommand : IRequest<Result>
    {
        public CancelOrderBuyerCommand(string customerId, string orderNumber)
        {
            CustomerId = customerId;
            OrderNumber = orderNumber;
        }

        public string CustomerId { get; }
        public string OrderNumber { get; }
    }
}