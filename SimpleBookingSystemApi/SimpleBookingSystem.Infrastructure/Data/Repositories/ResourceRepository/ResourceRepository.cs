namespace SimpleBookingSystem.Infrastructure.Data.Repositories.ResourceRepository
{
    using Microsoft.EntityFrameworkCore;
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Infrastructure.Common.Specifications;
    using SimpleBookingSystem.Infrastructure.Context;
    using SimpleBookingSystem.Infrastructure.Data.Models;
    using SimpleBookingSystem.Infrastructure.Mappers;
    using System.Threading.Tasks;

    public class ResourceRepository(ISimpleBookingSystemDbContext _dbContext) : IResourceRepository
    {
        public async Task<Result<Domain.Entities.Resources.Resource>> GetResourceByIdAsync(int resourceId)
        {
            Resource? dbResource = await _dbContext.Resources.AsNoTracking().SingleOrDefaultAsync(predicate: x => !x.IsDeleted
                                                                                                               && x.Id == resourceId);

            if (dbResource == null)
            {
                return Result<Domain.Entities.Resources.Resource>.Failed(errorMessage: $"Resource with id: {resourceId} not found!");
            }

            return Result<Domain.Entities.Resources.Resource>.Success(DataToDomainMapper.MapResourceDataToDomain(dbResource: dbResource));
        }

        public async Task<bool> CheckIfBookingDurationOverlapsWithExistingBookingDurationsAsync(int resourceId,
                                                                                                BookingDurationOverlapSpecification specification)
        {
            return await _dbContext.Bookings.Where(predicate: x => !x.IsDeleted && x.ResourceFk == resourceId)
                                            .Where(predicate: specification.IsSatisfiedExpression())
                                            .AnyAsync();
        }

        public void UpdateResource(Domain.Entities.Resources.Resource resource)
        {
            Resource dbResource = DomainToDataMapper.MapResourceDomainToData(domainResource: resource);

            _dbContext.Resources.Update(dbResource);
        }

        public async Task InsertBookingAsync(Domain.Entities.Resources.Booking booking)
        {
            Booking dbBooking = DomainToDataMapper.MapBookingDomainToData(domainBooking: booking);

            await _dbContext.Bookings.AddAsync(entity: dbBooking);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
