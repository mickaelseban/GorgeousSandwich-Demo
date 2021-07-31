namespace Domain.SeedWork
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using BasicWrapperTool;
    using Infrastructure.CrossCutting.Specification;

    public interface IRepository<T> where T : IAggregateRoot
    {
        Task SaveAsync(T aggregate, CancellationToken cancellationToken = default);

        Task<Result<IEnumerable<T>>> GetAsync(CancellationToken cancellationToken = default);

        Task<Result<IEnumerable<T>>> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

        Task<Result<T>> FindAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

        Task RemoveAsync(T aggregate, CancellationToken cancellationToken = default);
    }
}