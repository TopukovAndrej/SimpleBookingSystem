namespace SimpleBookingSystem.Tests.Queries
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using SimpleBookingSystem.Application.Queries.Resource;
    using SimpleBookingSystem.Contracts.Dtos.Resource;
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Infrastructure.Context;
    using SimpleBookingSystem.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GetAllResourcesQueryTests
    {
        private Mock<ISimpleBookingSystemReadonlyDbContext> _readonlyDbContext;

        private Mock<DbSet<Resource>> _resourcesDbSet;

        private GetAllResourcesQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _readonlyDbContext = new();

            _resourcesDbSet = new();

            _handler = new(_readonlyDbContext.Object);
        }

        [Test]
        public async Task WhenNoResourcesAreFound_ReturnsFailedResult()
        {
            // Arrange
            var resourceData = new List<Resource>().AsQueryable();

            GetAllResourcesQuery query = new();

            // Act
            Result<IReadOnlyList<ResourceDto>> result = await _handler.Handle(query: query, cancellationToken: default);

            // Assert
            Assert.Multiple(testDelegate: () =>
            {
                Assert.That(actual: result.IsFailure, expression: Is.True);
                Assert.That(actual: result.ErrorMessage, expression: Is.EqualTo(expected: "No resources found!"));
            });
        }
    }
}