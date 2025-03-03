namespace SimpleBookingSystem.Infrastructure.Common.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SimpleBookingSystem.Infrastructure.Data.Models;

    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable(name: "Resources");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Uid).HasColumnType(typeName: "UNIQUEIDENTIFIER").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnType("BIT").HasDefaultValue(false);
            builder.Property(x => x.Name).HasColumnType("NVARCHAR").HasMaxLength(30).IsRequired();
            builder.Property(x => x.TotalQuantity).HasColumnType("INT").IsRequired();

            builder.HasMany(x => x.Bookings).WithOne(x => x.Resource).HasForeignKey(x => x.ResourceFk);
        }
    }
}
