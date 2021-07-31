namespace Domain.SeedWork
{
    using System;

    public abstract class DomainEventBase : IDomainEvent
    {
        protected DomainEventBase()
        {
            CorrelationId = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }

        public Guid CorrelationId { get; }

        public DateTime OccurredOn { get; }
    }
}