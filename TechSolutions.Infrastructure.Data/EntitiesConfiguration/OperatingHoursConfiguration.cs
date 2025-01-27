using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Infrastructure.Data.EntitiesConfiguration;

public class OperatingHoursConfiguration : IEntityTypeConfiguration<OperatingHours>
{
    public void Configure(EntityTypeBuilder<OperatingHours> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DayOfWeek).IsRequired();
        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.EndTime).IsRequired();

        builder
            .HasMany(x => x.Rooms)
            .WithMany(x => x.OperatingHours)
            .UsingEntity<Dictionary<string, object>>(
                "RoomOperatingHours",
                x => x.HasOne<Room>().WithMany().HasForeignKey("RoomId"),
                x => x.HasOne<OperatingHours>().WithMany().HasForeignKey("OperatingHoursId"),
                x =>
                {
                    x.HasKey("RoomId", "OperatingHoursId");
                });

    }
}