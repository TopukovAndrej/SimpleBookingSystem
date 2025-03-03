namespace SimpleBookingSystem.Infrastructure.Mappers
{
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Domain.Entities.Resources.ValueObjects;
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

        public static Result<Domain.Entities.Resources.Booking> MapBookingDataToDomain(Booking dbBooking)
        {
            return Domain.Entities.Resources.Booking.Create(id: dbBooking.Id,
                                                            uid: dbBooking.Uid,
                                                            isDeleted: dbBooking.IsDeleted,
                                                            bookedQuantity: dbBooking.BookedQuantity,
                                                            bookingDurationFromDate: dbBooking.FromDate,
                                                            bookingDurationToDate: dbBooking.ToDate,
                                                            resourceId: dbBooking.ResourceFk,
                                                            resourceName: dbBooking.Resource.Name);
        }

        public static Result<IReadOnlyList<Domain.Entities.Resources.Booking>> MapBookingsDataToDomain(IReadOnlyList<Booking> dbBookings)
        {
            List<Domain.Entities.Resources.Booking> domainBookings = new();

            foreach(Booking dbBooking in dbBookings)
            {
                Result<Domain.Entities.Resources.Booking> mappedBookingResult = MapBookingDataToDomain(dbBooking: dbBooking);

                if (mappedBookingResult.IsFailure)
                {
                    return Result<IReadOnlyList<Domain.Entities.Resources.Booking>>.Failed(errorMessage: mappedBookingResult.ErrorMessage);
                }

                domainBookings.Add(item: mappedBookingResult.Value!);
            }

            return Result<IReadOnlyList<Domain.Entities.Resources.Booking>>.Success(value: domainBookings);
        }
    }
}
