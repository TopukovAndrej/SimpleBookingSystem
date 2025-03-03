namespace SimpleBookingSystem.Application.Commands.Resource
{
    using MediatR;
    using SimpleBookingSystem.Application.Commands.Email;
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Contracts.Requests.Resource;
    using SimpleBookingSystem.Domain.Entities.Resources;
    using SimpleBookingSystem.Infrastructure.Common.Specifications;
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

    public class BookResourceCommandHandler(IMediator mediator, IResourceRepository resourceRepository) : IRequestHandler<BookResourceCommand, Result>
    {
        public async Task<Result> Handle(BookResourceCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Request.Quantity <= 0)
            {
                return Result.Failed(errorMessage: "Cannot book resource with a negative or zero quantity!");
            }

            if (command.Request.FromDate > command.Request.ToDate)
            {
                return Result.Failed(errorMessage: "Invalid book request!");
            }

            if (command.Request.FromDate < DateTime.UtcNow.AddHours(value: 1) || command.Request.ToDate.Date < DateTime.UtcNow.Date)
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

            bool overlapsWithExistingBookingDurations =
                await resourceRepository.CheckIfBookingDurationOverlapsWithExistingBookingDurationsAsync(resourceId: resourceResult.Value.Id,
                                                                                                         specification: new BookingDurationOverlapSpecification(fromDate: command.Request.FromDate,
                                                                                                                                                                toDate: command.Request.ToDate));

            if (overlapsWithExistingBookingDurations)
            {
                return Result.Failed(errorMessage: "Cannot book resource, since requested booking dates overlap with existing ones!");
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

            Result resourceTotalQuantityUpdatedResult = resourceResult.Value.UpdateTotalQuantity(bookedQuantity: newBookingResult.Value!.BookedQuantity);

            if (resourceTotalQuantityUpdatedResult.IsFailure)
            {
                return Result.Failed(errorMessage: resourceTotalQuantityUpdatedResult.ErrorMessage);
            }

            resourceRepository.UpdateResource(resource: resourceResult.Value);

            await resourceRepository.InsertBookingAsync(booking: newBookingResult.Value);

            await resourceRepository.SaveChangesAsync();

            Result emailSentResult = await mediator.Send(new SendEmailCommand(resourceId: resourceResult.Value.Id), cancellationToken);

            if (emailSentResult.IsFailure)
            {
                Console.WriteLine(value: "Email was not sent!");
            }

            return Result.Success();
        }
    }
}
