namespace SimpleBookingSystem.Domain.Entities.Resources
{
    public class BookingResource
    {
        public int Id { get; }

        public string Name { get; }

        public BookingResource(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
