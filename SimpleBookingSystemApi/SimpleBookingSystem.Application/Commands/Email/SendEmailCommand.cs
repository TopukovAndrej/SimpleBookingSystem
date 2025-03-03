namespace SimpleBookingSystem.Application.Commands.Email
{
    using MediatR;
    using SimpleBookingSystem.Contracts.Models;

    public class SendEmailCommand : IRequest<Result>
    {
        public int ResourceId { get; }

        public string Email { get; }

        public SendEmailCommand(int resourceId, string email = "admin@admin.com")
        {
            Email = email;
        }
    }

    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result>
    {
        public async Task<Result> Handle(SendEmailCommand command, CancellationToken cancellationToken = default)
        {
            // Mocking the sending of the email as specified
            await Task.Delay(millisecondsDelay: 2000, cancellationToken: cancellationToken);

            Console.WriteLine(value: $"EMAIL SENT TO {command.Email} FOR CREATED BOOKING WITH ID {command.ResourceId}.");

            return Result.Success();
        }
    }
}
