namespace Domain.SeedWork
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDomainEventStream<T> where T : IDomainEvent
    {
        Task SaveAsync(IEnumerable<T> domainEvents, CancellationToken cancellationToken = default);
    }
}