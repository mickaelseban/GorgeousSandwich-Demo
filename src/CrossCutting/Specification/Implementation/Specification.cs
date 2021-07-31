namespace Infrastructure.CrossCutting.Specification.Implementation
{
    using System;
    using System.Linq.Expressions;

    public abstract class Specification<T> : ISpecification<T>
    {
        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}