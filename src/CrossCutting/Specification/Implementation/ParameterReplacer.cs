namespace Infrastructure.CrossCutting.Specification.Implementation
{
    using System.Linq.Expressions;

    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        internal ParameterReplacer(ParameterExpression parameter)
        {
            this._parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(this._parameter);
        }
    }
}
