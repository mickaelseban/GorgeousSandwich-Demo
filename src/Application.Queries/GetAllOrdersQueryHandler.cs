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

    internal class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result<IEnumerable<OrderDto>>>
    {
        private readonly IRepository<Order> _repository;

        public GetAllOrdersQueryHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _repository.GetAsync(cancellationToken);

            return queryResult.IsFail
                ? Result<IEnumerable<OrderDto>>.Fail(queryResult.Messages)
                : Result<IEnumerable<OrderDto>>.Success(queryResult.Value.ToList().Select(Mappers.Map));
        }
    }
}