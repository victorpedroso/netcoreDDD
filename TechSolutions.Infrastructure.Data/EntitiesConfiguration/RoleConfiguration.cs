using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Infrastructure.Data.EntitiesConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.HasData(
            new Role(1, "Admin"),
            new Role(2, "Manager"),
            new Role(3, "User"),
            new Role(4, "Client")
        ); 
    }
}
