namespace Infrastructure.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BasicWrapperTool;
    using Domain.Orders;
    using Domain.SeedWork;
    using CrossCutting.Specification;

    public class OrderRepository : IRepository<Order>
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork;

        public OrderRepository(UnitOfWork.UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Order>>> GetAsync(CancellationToken cancellationToken = default)
        {
            var orders = await _unitOfWork.Get<Order>(cancellationToken);

            return new ResultBuilder()
                .EnsureNotNull(orders, nameof(orders))
                .Ensure(() => orders.Any(), nameof(orders))
                .Build(orders.ToList().AsEnumerable());
        }

        public async Task<Result<IEnumerable<Order>>> GetAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default)
        {
            var orders = await _unitOfWork.Get(specification, cancellationToken);

            return new ResultBuilder()
                .EnsureNotNull(orders, nameof(orders))
                .Ensure(() => orders.Any(), nameof(orders))
                .Build(orders.ToList().AsEnumerable());
        }

        public async Task<Result<Order>> FindAsync(ISpecification<Order> specification, CancellationToken cancellationToken = default)
        {
            var order = await _unitOfWork.Find(specification, cancellationToken);

            return new ResultBuilder()
                .EnsureNotNull(order, nameof(order))
                .Build(order);
        }

        public async Task SaveAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.SaveOrUpdate(order, cancellationToken);
        }

        public async Task RemoveAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.DeleteAsync(order, cancellationToken);
        }
    }
}