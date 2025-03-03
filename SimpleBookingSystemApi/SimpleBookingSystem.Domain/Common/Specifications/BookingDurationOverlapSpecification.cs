﻿namespace SimpleBookingSystem.Domain.Common.Specifications
{
    using SimpleBookingSystem.Domain.Common.Interfaces;
    using SimpleBookingSystem.Domain.Entities.Resources.ValueObjects;
    using System.Linq.Expressions;

    public class BookingDurationOverlapSpecification : ISpecification<BookingDuration>
    {
        private readonly IReadOnlyList<BookingDuration> _bookingDurations;

        public BookingDurationOverlapSpecification(IReadOnlyList<BookingDuration> bookingDurations)
        {
            _bookingDurations = bookingDurations;
        }

        public bool IsSatisfiedBy(BookingDuration bookingDuration)
        {
            return _bookingDurations.Any(predicate: x => x.FromDate < bookingDuration.ToDate && x.ToDate > bookingDuration.FromDate);
        }
    }
}
