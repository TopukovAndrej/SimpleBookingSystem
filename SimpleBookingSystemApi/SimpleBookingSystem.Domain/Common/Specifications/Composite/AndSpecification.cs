namespace SimpleBookingSystem.Domain.Common.Specifications.Composite
{
    using SimpleBookingSystem.Domain.Common.Interfaces;

    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _firstSpecification;

        private readonly ISpecification<T> _secondSpecification;

        public AndSpecification(ISpecification<T> firstSpecification, ISpecification<T> secondSpecification)
        {
            _firstSpecification = firstSpecification;
            _secondSpecification = secondSpecification;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return _firstSpecification.IsSatisfiedBy(entity) && _secondSpecification.IsSatisfiedBy(entity);
        }
    }
}
