namespace SimpleBookingSystem.Infrastructure.Common.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SimpleBookingSystem.Infrastructure.Data.Models;

    class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable(name: "Bookings");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Uid).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnType("BIT").HasDefaultValue(false);
            builder.Property(x => x.FromDate).HasColumnType("DATETIME2(0)").IsRequired();
            builder.Property(x => x.ToDate).HasColumnType("DATETIME2(0)").IsRequired();
            builder.Property(x => x.ResourceFk).HasColumnType("INT").IsRequired();

            builder.HasOne(x => x.Resource).WithMany(x => x.Bookings).HasForeignKey(x => x.ResourceFk);
        }
    }
}
