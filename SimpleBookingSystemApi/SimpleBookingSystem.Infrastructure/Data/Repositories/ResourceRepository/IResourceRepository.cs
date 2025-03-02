namespace SimpleBookingSystem.Infrastructure.Data.Repositories.ResourceRepository
{
    using SimpleBookingSystem.Contracts.Models;
    using SimpleBookingSystem.Infrastructure.Common.Interfaces;
    using SimpleBookingSystem.Infrastructure.Data.Models;

    public interface IResourceRepository : IRepository<Resource>
    {
        Task<Result<Domain.Entities.Resources.Resource>> GetResourceByIdAsync(int resourceId);

        Task<Result<IReadOnlyList<Domain.Entities.Resources.Booking>>> GetExistingBookingsForResourceAsync(int resourceId);

        void UpdateResource(Domain.Entities.Resources.Resource resource);

        Task InsertBookingAsync(Domain.Entities.Resources.Booking booking);
    }
}
