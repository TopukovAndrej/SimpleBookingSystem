namespace SimpleBookingSystem.Infrastructure.Data.Models
{
    public class Booking
    {
        public int Id { get; internal set; }

        public Guid Uid { get; internal set; }

        public bool IsDeleted { get; internal set; }

        public int BookedQuantity { get; internal set; }

        public DateTime FromDate { get; internal set; }

        public DateTime ToDate { get; internal set; }

        public int ResourceFk { get; internal set; }

        public virtual Resource Resource { get; internal set; }

        private Booking() { }

        public Booking(int id, Guid uid, bool isDeleted, int bookedQuantity, DateTime fromDate, DateTime toDate, int resourceFk)
        {
            Id = id;
            Uid = uid;
            IsDeleted = isDeleted;
            BookedQuantity = bookedQuantity;
            FromDate = fromDate;
            ToDate = toDate;
            ResourceFk = resourceFk;
        }
    }
}
