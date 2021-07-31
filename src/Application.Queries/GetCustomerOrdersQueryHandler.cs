namespace Application.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BasicWrapperTool;
    using Domain.Orders;
    using Domain.SeedWork;
    using MediatR;

    internal class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, Result<IEnumerable<OrderDto>>>
    {
        private readonly IRepository<Order> _repository;

        public GetCustomerOrdersQueryHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<OrderDto>>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var customerNumber = Id.Create(request.CustomerNumber);
            if (customerNumber.IsFail)
            {
                return Result<IEnumerable<OrderDto>>.Fail(customerNumber.Messages);
            }

            var queryResult = await _repository.GetAsync(new OrderByCustomerNumberSpecification((Id)customerNumber), cancellationToken);

            return queryResult.IsFail
                ? Result<IEnumerable<OrderDto>>.Fail(queryResult.Messages)
                : Result<IEnumerable<OrderDto>>.Success(queryResult.Value.ToList().Select(Mappers.Map));
        }
    }
}