using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshToken");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Users)
            .WithMany(y => y.RefreshTokens)
            .HasForeignKey(z => z.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

