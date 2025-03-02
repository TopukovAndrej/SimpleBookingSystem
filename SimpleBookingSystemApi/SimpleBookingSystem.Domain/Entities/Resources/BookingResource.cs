namespace SimpleBookingSystem.Domain.Entities.Resources
{
    public class BookingResource
    {
        public int ResourceId { get; }

        public string ResourceName { get; }

        public BookingResource(int resourceId, string resourceName)
        {
            ResourceId = resourceId;
            ResourceName = resourceName;
        }
    }
}
