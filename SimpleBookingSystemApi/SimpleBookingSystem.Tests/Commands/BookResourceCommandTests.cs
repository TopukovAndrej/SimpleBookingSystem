namespace SimpleBookingSystem.Tests.Commands
{
    using MediatR;
    using Moq;
    using SimpleBookingSystem.Application.Commands.Resource;
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Infrastructure.Data.Repositories.ResourceRepository;
    using SimpleBookingSystem.Tests.Common;

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

        [Test]
        public async Task WhenBookResourceRequestHasQuantityLessThanZero_ReturnsFailedResult()
        {
            // Arrange
            var invalidRequest = CommandTestUtils.GetBookResourceRequestTestSample(resourceId: 1,
                                                                                   fromDate: new DateTime(day: 3, month: 3, year: 2026, hour: 20, minute: 58, second: 0, kind: DateTimeKind.Utc),
                                                                                   toDate: new DateTime(day: 3, month: 3, year: 2026, hour: 20, minute: 58, second: 0, kind: DateTimeKind.Utc),
                                                                                   quantity: -1);

            BookResourceCommand command = new(request: invalidRequest);

            // Act
            Result result = await _handler.Handle(command: command, cancellationToken: default);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual: result.IsFailure, expression: Is.True);
                Assert.That(actual: result.ErrorMessage, expression: Is.EqualTo(expected: "Cannot book resource with a negative qunatity!"));
            });
        }
    }
}
