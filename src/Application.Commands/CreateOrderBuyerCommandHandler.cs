namespace Application.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using BasicWrapperTool;
    using Domain.Orders;
    using Domain.SeedWork;
    using MediatR;

    internal class CreateOrderBuyerCommandHandler : IRequestHandler<CreateOrderBuyerCommand, Result>
    {
        private readonly IRepository<Order> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderBuyerCommandHandler(IRepository<Order> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateOrderBuyerCommand request, CancellationToken cancellationToken)
        {
            var customerId = Id.Create(request.CustomerId);
            var orderDate = OrderStatus.Create(request.DeliveryDate);
            var customerTaxNumber = TaxNumber.Create(request.CustomerTaxNumber);

            var validateCommand = new ResultValidationBuilder()
                .AddResult(customerId)
                .AddResult(orderDate)
                .AddResult(customerTaxNumber)
                .Build();

            if (validateCommand.IsFail)
                return validateCommand;

            var products = request.Products.Select(p => _mapper.Map<Product>(p));
            var customer = new Student(customerId.Value, request.CustomerName, request.CustomerEmail, customerTaxNumber.Value);
            var order = new OrderBuyer(Id.Generate(), products, customer, orderDate.Value);

            await _repository.SaveAsync(order, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}