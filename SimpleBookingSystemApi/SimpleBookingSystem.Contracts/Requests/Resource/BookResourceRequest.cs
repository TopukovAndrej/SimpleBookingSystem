namespace SimpleBookingSystem.Contracts.Requests.Resource
{
    public class BookResourceRequest
    {
        public int ResourceId { get; }

        public int Quantity { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }
    }
}
