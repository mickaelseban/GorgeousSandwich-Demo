namespace Infrastructure.CrossCutting.Specification
{
    using System;
    using System.Linq.Expressions;

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}