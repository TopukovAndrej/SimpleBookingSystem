namespace SimpleBookingSystem.Infrastructure.Mappers
{
    using SimpleBookingSystem.Infrastructure.Data.Models;

    public class DataToDomainMapper
    {
        public static Domain.Entities.Resources.Resource MapResourceDataToDomain(Resource dbResource)
        {
            return new Domain.Entities.Resources.Resource(id: dbResource.Id,
                                                          uid: dbResource.Uid,
                                                          isDeleted: dbResource.IsDeleted,
                                                          name: dbResource.Name,
                                                          totalQuality: dbResource.TotalQuantity);
        }

        public static Domain.Entities.Resources.Booking MapBookingDataToDomain(Booking dbBooking)
        {
            return new Domain.Entities.Resources.Booking(id: dbBooking.Id,
                                                         uid: dbBooking.Uid,
                                                         isDeleted: dbBooking.IsDeleted,
                                                         bookedFromDate: dbBooking.FromDate,
                                                         bookedToDate: dbBooking.ToDate,
                                                         resourceFk: dbBooking.ResourceFk);
        }
    }
}
