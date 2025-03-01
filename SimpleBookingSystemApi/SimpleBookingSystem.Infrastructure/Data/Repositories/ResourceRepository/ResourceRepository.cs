namespace SimpleBookingSystem.Infrastructure.Data.Repositories.ResourceRepository
{
    using SimpleBookingSystem.Infrastructure.Common.Interfaces;
    using System.Threading.Tasks;

    public class ResourceRepository : IResourceRepository
    {
        private readonly ISimpleBookingSystemDbContext _dbContext;

        public ResourceRepository(ISimpleBookingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
