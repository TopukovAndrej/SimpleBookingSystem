namespace SimpleBookingSystem.Infrastructure.Mappers
{
    using SimpleBookingSystem.Infrastructure.Data.Models;

    public class DomainToDataMapper
    {
        public static Resource MapResourceDomainToData(Domain.Entities.Resources.Resource domainResource)
        {
            return new(id: domainResource.Id,
                       uid: domainResource.Uid,
                       isDeleted: domainResource.IsDeleted,
                       name: domainResource.Name,
                       totalQuantity: domainResource.TotalQuantity);
        }

        public static Booking MapBookingDomainToData(Domain.Entities.Resources.Booking domainBooking)
        {
            return new(id: domainBooking.Id,
                       uid: domainBooking.Uid,
                       isDeleted: domainBooking.IsDeleted,
                       bookedQuantity: domainBooking.BookedQuantity,
                       fromDate: domainBooking.BookingDuration.FromDate,
                       toDate: domainBooking.BookingDuration.ToDate,
                       resourceFk: domainBooking.BookingResource.Id);
        }
    }
}
