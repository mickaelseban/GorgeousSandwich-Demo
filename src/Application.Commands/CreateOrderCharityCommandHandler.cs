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

    internal class CreateOrderCharityCommandHandler : IRequestHandler<CreateOrderCharityCommand, Result>
    {
        private readonly IRepository<Order> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderCharityCommandHandler(IRepository<Order> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateOrderCharityCommand request, CancellationToken cancellationToken)
        {
            var institutionId = Id.Create(request.InstitutionId);
            var orderDate = OrderStatus.Create(request.DeliveryDate);
            var customerTaxNumber = TaxNumber.Create(request.InstitutionTaxNumber);

            var validateCommand = new ResultValidationBuilder()
                .AddResult(orderDate)
                .AddResult(customerTaxNumber)
                .Build();

            if (validateCommand.IsFail)
                return validateCommand;

            var products = request.Products.Select(p => _mapper.Map<Product>(p));
            var institution = new Institution(institutionId.Value, request.InstitutionName, request.InstitutionEmail, customerTaxNumber.Value);
            var order = new OrderCharity(Id.Generate(), products, institution, orderDate.Value);

            await _repository.SaveAsync(order, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}