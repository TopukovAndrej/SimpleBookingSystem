namespace SimpleBookingSystem.Infrastructure.Common.Interfaces
{
    using System.Linq.Expressions;

    public interface ISpecification<T>
    {
        public Expression<Func<T, bool>> IsSatisfiedExpression();
    }
}
