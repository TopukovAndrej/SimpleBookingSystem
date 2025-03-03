namespace SimpleBookingSystem.Domain.Entities.Resources
{
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Domain.Common.Entities;
    using SimpleBookingSystem.Domain.Entities.Resources.ValueObjects;

    public class Booking : Entity
    {
        public int BookedQuantity { get; private set; }

        public BookingDuration BookingDuration { get; private set; }

        public BookingResource BookingResource { get; private set; }

        private Booking(int id,
                        Guid uid,
                        bool isDeleted,
                        int bookedQuantity,
                        BookingDuration bookingDuration,
                        BookingResource bookingResource) : base(id: id, uid: uid, isDeleted: isDeleted)
        {
            BookedQuantity = bookedQuantity;
            BookingResource = bookingResource;
            BookingDuration = bookingDuration;
        }

        public static Result<Booking> Create(int id,
                                             Guid uid,
                                             bool isDeleted,
                                             int bookedQuantity,
                                             DateTime bookingDurationFromDate,
                                             DateTime bookingDurationToDate,
                                             int resourceId,
                                             string resourceName)
        {
            Result<BookingDuration> bookingDurationResult = BookingDuration.Create(fromDate: bookingDurationFromDate,
                                                                                   toDate: bookingDurationToDate);

            if (bookingDurationResult.IsFailure)
            {
                return Result<Booking>.Failed(errorMessage: bookingDurationResult.ErrorMessage);
            }

            BookingResource bookingResource = new(id: resourceId, name: resourceName);

            return Result<Booking>.Success(value: new Booking(id: id,
                                                              uid: uid,
                                                              isDeleted: isDeleted,
                                                              bookedQuantity: bookedQuantity,
                                                              bookingDuration: bookingDurationResult.Value!,
                                                              bookingResource: bookingResource));
        }
    }
}
