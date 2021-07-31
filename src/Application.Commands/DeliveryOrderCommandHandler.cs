namespace Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using BasicWrapperTool;
    using Domain.Orders;
    using Domain.SeedWork;
    using MediatR;

    internal class DeliveryOrderCommandHandler : IRequestHandler<DeliveryOrderCommand, Result>
    {
        private readonly IRepository<Order> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeliveryOrderCommandHandler(IRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeliveryOrderCommand request, CancellationToken cancellationToken)
        {
            var orderNumber = Id.Create(request.OrderNumber);
            var customerId = Id.Create(request.CustomerId);

            var commandValidation = new ResultValidationBuilder()
                .AddResult(orderNumber)
                .AddResult(customerId)
                .Build();

            if (commandValidation.IsFail)
                return commandValidation;

            var specification = new OrderByOrderNumberSpecification(orderNumber.Value)
                .And(new OrderByCustomerNumberSpecification(customerId.Value));

            var orderResult = await _repository.FindAsync(specification, cancellationToken);

            if (orderResult.IsFail)
                return orderResult.ToResult();

            var order = orderResult.Value;
            var deliveryResult = order.Delivery();

            if (deliveryResult.IsFail)
                return deliveryResult;

            await _repository.SaveAsync(order, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}