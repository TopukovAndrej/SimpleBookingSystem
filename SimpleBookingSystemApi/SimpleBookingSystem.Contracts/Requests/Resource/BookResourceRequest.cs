namespace SimpleBookingSystem.Contracts.Requests.Resource
{
    public class BookResourceRequest
    {
        public int ResourceId { get; set; }

        public int Quantity { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
