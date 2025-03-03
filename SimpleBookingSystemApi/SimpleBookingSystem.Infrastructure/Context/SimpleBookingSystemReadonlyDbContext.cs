namespace SimpleBookingSystem.Infrastructure.Context
{
    using Microsoft.EntityFrameworkCore;
    using SimpleBookingSystem.Infrastructure.Common.Configurations;
    using SimpleBookingSystem.Infrastructure.Data.Models;

    public class SimpleBookingSystemReadonlyDbContext : DbContext, ISimpleBookingSystemReadonlyDbContext
    {
        public DbSet<Resource> Resources { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public SimpleBookingSystemReadonlyDbContext(DbContextOptions<SimpleBookingSystemReadonlyDbContext> options) : base(options: options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(configuration: new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(configuration: new BookingConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
