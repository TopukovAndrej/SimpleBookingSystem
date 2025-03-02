namespace SimpleBookingSystem.Tests.Commands
{
    using MediatR;
    using Moq;
    using SimpleBookingSystem.Application.Commands.Resource;
    using SimpleBookingSystem.Infrastructure.Data.Repositories.ResourceRepository;

    public class BookResourceCommandTests
    {
        private Mock<IMediator> _mediator;
        private Mock<IResourceRepository> _resourceRepository;

        private BookResourceCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mediator = new();
            _resourceRepository = new();

            _handler = new(mediator: _mediator.Object, _resourceRepository.Object);
        }
    }
}
