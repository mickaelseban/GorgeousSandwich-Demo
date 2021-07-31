namespace Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using BasicWrapperTool;
    using Domain.Orders;
    using Domain.SeedWork;
    using MediatR;

    internal class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Result<OrderDto>>
    {
        private readonly IRepository<Order> _repository;

        public GetOrderQueryHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var orderNumber = Id.Create(request.OrderNumber);
            if (orderNumber.IsFail)
            {
                return Result<OrderDto>.Fail(orderNumber.Messages);
            }

            var queryResult = await _repository.FindAsync(new OrderByOrderNumberSpecification((Id)orderNumber), cancellationToken);

            return queryResult.IsFail
                ? Result<OrderDto>.Fail(queryResult.Messages)
                : Result<OrderDto>.Success(Mappers.Map(queryResult.Value));
        }
    }
}