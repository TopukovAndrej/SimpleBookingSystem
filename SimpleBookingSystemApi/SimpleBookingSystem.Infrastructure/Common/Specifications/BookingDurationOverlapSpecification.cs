namespace SimpleBookingSystem.Infrastructure.Common.Specifications
{
    using SimpleBookingSystem.Infrastructure.Common.Interfaces;
    using SimpleBookingSystem.Infrastructure.Data.Models;
    using System.Linq.Expressions;

    public class BookingDurationOverlapSpecification : ISpecification<Booking>
    {
        private readonly DateTime _fromDate;
        private readonly DateTime _toDate;

        public BookingDurationOverlapSpecification(DateTime fromDate, DateTime toDate)
        {
            _fromDate = fromDate;
            _toDate = toDate;
        }

        public Expression<Func<Booking, bool>> IsSatisfiedExpression()
        {
            return booking => booking.FromDate < _toDate && booking.ToDate > _fromDate;
        }
    }
}
