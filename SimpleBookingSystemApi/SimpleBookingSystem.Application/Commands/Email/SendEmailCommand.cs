namespace SimpleBookingSystem.Application.Commands.Email
{
    using MediatR;

    public class SendEmailCommand : IRequest
    {
        public int ResourceId { get; }

        public string Email { get; }

        public SendEmailCommand(int resourceId, string email = "admin@admin.com")
        {
            Email = email;
        }
    }

    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
    {
        public async Task Handle(SendEmailCommand command, CancellationToken cancellationToken = default)
        {
            // Mocking the sending of the email as specified
            Console.WriteLine(value: $"EMAIL SENT TO {command.Email} FOR CREATED BOOKING WITH ID {command.ResourceId}.");
        }
    }
}
