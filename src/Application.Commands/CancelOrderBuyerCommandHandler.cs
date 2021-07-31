namespace Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using BasicWrapperTool;
    using Domain.Orders;
    using Domain.SeedWork;
    using MediatR;

    internal class CancelOrderBuyerCommandHandler : IRequestHandler<CancelOrderBuyerCommand, Result>
    {
        private readonly IRepository<Order> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderBuyerCommandHandler(IRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CancelOrderBuyerCommand request, CancellationToken cancellationToken)
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

            if (orderResult.Value.Type != Order.OrderType.Buyer)
                return Result.Fail($"The order is not of the type: {nameof(OrderBuyer)}");

            var orderBuyer = (OrderBuyer)orderResult.Value;

            var cancelResult = orderBuyer.Cancel();

            if (cancelResult.IsFail)
                return cancelResult;

            await _repository.SaveAsync(orderBuyer, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}