namespace SimpleBookingSystem.Domain.Entities.Resources.ValueObjects
{
    using SimpleBookingSystem.Domain.Common.Entities;
    using System.Collections.Generic;

    public class BookingPeriod : ValueObject
    {
        public DateTime FromDate { get; private set; }

        public DateTime ToDate { get; private set; }

        public BookingPeriod(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FromDate;
            yield return ToDate;
        }
    }
}
