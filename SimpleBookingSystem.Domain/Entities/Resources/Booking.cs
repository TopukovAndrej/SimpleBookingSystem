namespace SimpleBookingSystem.Domain.Entities.Resources
{
    using SimpleBookingSystem.Domain.Common.Entities;
    using SimpleBookingSystem.Domain.Entities.Resources.ValueObjects;

    public class Booking : Entity
    {
        public BookingPeriod BookingPeriod { get; private set; }

        public int ResourceFk { get; private set; }

        public Booking(int id,
                       Guid uid,
                       bool isDeleted,
                       DateTime bookedFromDate,
                       DateTime bookedToDate,
                       int resourceFk) : base(id: id, uid: uid, isDeleted: isDeleted)
        {
            BookingPeriod = new(fromDate: bookedFromDate, toDate: bookedToDate);

            ResourceFk = resourceFk;
        }
    }
}
