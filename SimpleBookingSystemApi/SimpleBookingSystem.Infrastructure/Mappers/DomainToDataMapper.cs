namespace SimpleBookingSystem.Infrastructure.Mappers
{
    using SimpleBookingSystem.Infrastructure.Data.Models;

    public class DomainToDataMapper
    {
        public static Resource MapResourceDomainToData(Domain.Entities.Resources.Resource domainResource)
        {
            return new()
            {
                Id = domainResource.Id,
                Uid = domainResource.Uid,
                IsDeleted = domainResource.IsDeleted,
                Name = domainResource.Name,
                TotalQuantity = domainResource.TotalQuantity,
            };
        }

        public static Booking MapBookingDomainToData(Domain.Entities.Resources.Booking domainBooking)
        {
            return new()
            {
                Id = domainBooking.Id,
                Uid = domainBooking.Uid,
                IsDeleted = domainBooking.IsDeleted,
                FromDate = domainBooking.BookingPeriod.FromDate,
                ToDate = domainBooking.BookingPeriod.ToDate,
                ResourceFk = domainBooking.ResourceFk,
            };
        }
    }
}
