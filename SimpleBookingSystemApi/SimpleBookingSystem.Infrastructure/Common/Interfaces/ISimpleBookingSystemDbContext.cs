namespace SimpleBookingSystem.Infrastructure.Common.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using SimpleBookingSystem.Infrastructure.Data.Models;

    public interface ISimpleBookingSystemDbContext
    {
        DbSet<Resource> Resources { get; }

        DbSet<Booking> Bookings { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
