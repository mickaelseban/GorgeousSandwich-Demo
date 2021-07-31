namespace Application.Commands
{
    using BasicWrapperTool;
    using MediatR;

    public class DeliveryOrderCommand : IRequest<Result>
    {
        public DeliveryOrderCommand(string customerId, string orderNumber)
        {
            CustomerId = customerId;
            OrderNumber = orderNumber;
        }

        public string CustomerId { get; }
        public string OrderNumber { get; }
    }
}