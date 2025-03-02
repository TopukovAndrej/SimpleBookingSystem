namespace SimpleBookingSystem.Application.Commands.Resource
{
    using MediatR;
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Contracts.Requests.Resource;
    using SimpleBookingSystem.Domain.Common.Specifications;
    using SimpleBookingSystem.Domain.Entities.Resources;
    using SimpleBookingSystem.Domain.Entities.Resources.ValueObjects;
    using SimpleBookingSystem.Infrastructure.Data.Repositories.ResourceRepository;
    using System.Threading;
    using System.Threading.Tasks;

    public class BookResourceCommand : IRequest<Result>
    {
        public BookResourceRequest Request { get; }

        public BookResourceCommand(BookResourceRequest request)
        {
            Request = request;
        }
    }

    public class BookResourceCommandHandler(IResourceRepository resourceRepository) : IRequestHandler<BookResourceCommand, Result>
    {
        public async Task<Result> Handle(BookResourceCommand command, CancellationToken cancellationToken)
        {
            if (command.Request.Quantity < 0)
            {
                return Result.Failed(errorMessage: "Cannot book resource with a negative qunatity!");
            }

            if (command.Request.FromDate > command.Request.ToDate)
            {
                return Result.Failed(errorMessage: "Invalid book request!");
            }

            if (command.Request.FromDate.Date < DateTime.UtcNow.Date || command.Request.ToDate.Date < DateTime.UtcNow.Date)
            {
                return Result.Failed(errorMessage: "Cannot book resource with dates less than today's date!");
            }

            Result<Resource> resourceResult = await resourceRepository.GetResourceByIdAsync(resourceId: command.Request.ResourceId);

            if (resourceResult.IsFailure)
            {
                return Result.Failed(errorMessage: resourceResult.ErrorMessage);
            }

            if (resourceResult.Value!.TotalQuantity < command.Request.Quantity)
            {
                return Result.Failed(errorMessage: "Cannot book resource with quantity less than its total quantity!");
            }

            Result<Booking> newBookingResult = Booking.Create(id: 0,
                                                              uid: Guid.NewGuid(),
                                                              isDeleted: false,
                                                              bookedQuantity: command.Request.Quantity,
                                                              bookingDurationFromDate: command.Request.FromDate,
                                                              bookingDurationToDate: command.Request.ToDate,
                                                              resourceId: resourceResult.Value.Id,
                                                              resourceName: resourceResult.Value.Name);

            if (newBookingResult.IsFailure)
            {
                return Result.Failed(errorMessage: newBookingResult.ErrorMessage);
            }

            Result<IReadOnlyList<Booking>> existingBookingsForResourceResult = await resourceRepository.GetExistingBookingsForResourceAsync(resourceId: resourceResult.Value.Id);

            if (existingBookingsForResourceResult.IsFailure)
            {
                return Result.Failed(errorMessage: "Cannot book resource with quantity less than its total quantity!");
            }

            if (existingBookingsForResourceResult.Value!.Count > 0)
            {
                if (CheckIfRequestedBookingDurationOverlapsWithExistingDuration(newBookingDuration: newBookingResult.Value!.BookingDuration,
                                                                                existingBookingDurations: existingBookingsForResourceResult.Value.Select(x => x.BookingDuration).ToList()))
                {
                    return Result.Failed(errorMessage: "Cannot book resource, since requested booking dates overlap with existing ones!");
                }
            }

            Result resourceTotalQuantityUpdatedResult = resourceResult.Value.UpdateTotalQuantity(bookedQuantity: newBookingResult.Value!.BookedQuantity);

            if (resourceTotalQuantityUpdatedResult.IsFailure)
            {
                return Result.Failed(errorMessage: resourceTotalQuantityUpdatedResult.ErrorMessage);
            }

            resourceRepository.UpdateResource(resource: resourceResult.Value);

            await resourceRepository.InsertBookingAsync(booking: newBookingResult.Value);

            await resourceRepository.SaveChangesAsync();

            return Result.Success();
        }

        private static bool CheckIfRequestedBookingDurationOverlapsWithExistingDuration(BookingDuration newBookingDuration,
                                                                                        IReadOnlyList<BookingDuration> existingBookingDurations)
        {
            var bookingDurationOverlapSpecification = new BookingDurationOverlapSpecification(bookingDurations: existingBookingDurations);

            return bookingDurationOverlapSpecification.IsSatisfiedBy(bookingDuration: newBookingDuration);
        }
    }
}
