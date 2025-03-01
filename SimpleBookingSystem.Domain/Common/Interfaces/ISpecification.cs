namespace SimpleBookingSystem.Domain.Common.Interfaces
{
    public interface ISpecification<T>
    {
        public bool IsSatisfiedBy(T entity);
    }
}
