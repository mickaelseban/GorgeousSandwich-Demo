namespace Application.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using BasicWrapperTool;
    using Domain.Orders;
    using Domain.SeedWork;
    using MediatR;

    internal class UpdateScheduledDateCommandHandler : IRequestHandler<UpdateScheduledDateCommand, Result>
    {
        private readonly IRepository<Order> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateScheduledDateCommandHandler(IRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateScheduledDateCommand request, CancellationToken cancellationToken)
        {
            var orderNumber = Id.Create(request.OrderNumber);
            var studentId = Id.Create(request.StudentId);

            var commandValidation = new ResultValidationBuilder()
                .AddResult(orderNumber)
                .AddResult(studentId)
                .Build();

            if (commandValidation.IsFail)
                return commandValidation;

            var specification = new OrderByOrderNumberSpecification(orderNumber.Value)
                .And(new OrderByCustomerNumberSpecification(studentId.Value));

            var orderResult = await _repository.FindAsync(specification, cancellationToken);

            if (orderResult.IsFail)
                return orderResult.ToResult();

            if (orderResult.Value.Type != Order.OrderType.Buyer)
                throw new InvalidOperationException($"The order is not of the type: {nameof(OrderBuyer)}");

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