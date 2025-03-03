namespace SimpleBookingSystem.Infrastructure.Context
{
    using Microsoft.EntityFrameworkCore;
    using SimpleBookingSystem.Infrastructure.Data.Models;

    public interface ISimpleBookingSystemReadonlyDbContext
    {
        DbSet<Resource> Resources { get; }

        DbSet<Booking> Bookings { get; }
    }
}
