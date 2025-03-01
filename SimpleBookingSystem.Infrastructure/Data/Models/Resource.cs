namespace SimpleBookingSystem.Infrastructure.Data.Models
{
    public class Resource
    {
        public int Id { get; internal set; }

        public Guid Uid { get; internal set; }

        public bool IsDeleted { get; internal set; }

        public string Name { get; internal set; }

        public int TotalQuantity { get; internal set; }

        public virtual ICollection<Booking> Bookings { get; internal set; }
    }
}
