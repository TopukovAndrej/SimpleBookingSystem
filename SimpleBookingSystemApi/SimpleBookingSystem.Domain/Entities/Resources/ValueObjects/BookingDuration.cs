namespace SimpleBookingSystem.Domain.Entities.Resources.ValueObjects
{
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Domain.Common.Entities;
    using System.Collections.Generic;

    public class BookingDuration : ValueObject
    {
        public DateTime FromDate { get; init; }

        public DateTime ToDate { get; init; }

        private BookingDuration(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }

        public static Result<BookingDuration> Create(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
            {
                return Result<BookingDuration>.Failed(errorMessage: "The from date cannot be after the to date!");
            }

            return Result<BookingDuration>.Success(value: new BookingDuration(fromDate: fromDate, toDate: toDate));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FromDate;
            yield return ToDate;
        }
    }
}
