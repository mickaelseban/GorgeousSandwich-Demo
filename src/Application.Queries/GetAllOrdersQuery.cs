namespace Application.Queries
{
    using System.Collections.Generic;
    using BasicWrapperTool;
    using MediatR;

    public class GetAllOrdersQuery : IRequest<Result<IEnumerable<OrderDto>>>
    {
    }
}