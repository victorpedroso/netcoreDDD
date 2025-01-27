using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSolutions.Domain.Entities;

namespace TechSolutions.Infrastructure.Data.EntitiesConfiguration;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.RefreshToken).IsRequired();
        builder.Property(x => x.Expires).IsRequired();
        builder.HasOne(x => x.User).WithOne(x => x.Token).HasForeignKey<Token>(x => x.UserId).IsRequired();
    }
}
