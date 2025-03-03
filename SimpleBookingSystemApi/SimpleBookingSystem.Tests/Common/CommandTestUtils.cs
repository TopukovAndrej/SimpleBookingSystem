namespace SimpleBookingSystem.Tests.Common
{
    using SimpleBookingSystem.Contracts.Requests.Resource;

    public static class CommandTestUtils
    {
        public static BookResourceRequest GetBookResourceRequestTestSample(int resourceId,
                                                                           DateTime fromDate,
                                                                           DateTime toDate,
                                                                           int quantity)
        {
            return new()
            {
                ResourceId = resourceId,
                FromDate = fromDate,
                ToDate = toDate,
                Quantity = quantity
            };
        }
    }
}
