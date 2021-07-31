namespace Infrastructure.Data.UnitOfWork
{
    using System.Data;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.SeedWork;
    using CrossCutting.Specification;
    using Session;
    using NHibernate;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;
        private bool _isAlive = true;

        public UnitOfWork(SessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        internal async Task Rollback(CancellationToken cancellationToken = default)
        {
            if (!_isAlive)
            {
                return;
            }

            try
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                _isAlive = false;
                _transaction.Dispose();
                _session.Dispose();
            }
        }

        internal Task DeleteAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            return _session.DeleteAsync(entity, cancellationToken);
        }

        internal Task<TOut> Find<TOut>(ISpecification<TOut> specification, CancellationToken cancellationToken = default) where TOut : class
        {
            return Task.FromResult(_session.Query<TOut>().SingleOrDefault(specification.ToExpression()));
        }

        internal Task<TOut> Find<TOut, TId>(TId id, CancellationToken cancellationToken = default) where TOut : class
        {
            return _session.GetAsync<TOut>(id, cancellationToken);
        }

        internal Task<IQueryable<TOut>> Get<TOut>(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_session.Query<TOut>());
        }

        internal Task<IQueryable<TOut>> Get<TOut>(ISpecification<TOut> specification, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_session.Query<TOut>().Where(specification.ToExpression()));
        }

        internal Task SaveOrUpdate<T>(T entity, CancellationToken cancellationToken = default)
        {
            return _session.SaveOrUpdateAsync(entity, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (!_isAlive)
            {
                return;
            }

            try
            {
                await _transaction.CommitAsync(cancellationToken);
            }
            finally
            {
                _isAlive = false;
                _transaction.Dispose();
                _session.Dispose();
            }
        }
    }
}