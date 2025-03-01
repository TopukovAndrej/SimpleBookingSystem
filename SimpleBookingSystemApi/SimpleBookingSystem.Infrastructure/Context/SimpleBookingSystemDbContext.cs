namespace SimpleBookingSystem.Infrastructure.Context
{
    using Microsoft.EntityFrameworkCore;
    using SimpleBookingSystem.Infrastructure.Common.Interfaces;
    using SimpleBookingSystem.Infrastructure.Data.Models;
    using System.Threading.Tasks;

    public class SimpleBookingSystemDbContext : DbContext, ISimpleBookingSystemDbContext
    {
        public DbSet<Resource> Resources { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public SimpleBookingSystemDbContext(DbContextOptions<SimpleBookingSystemDbContext> options) : base(options: options) { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
