using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Implements the Configure method defined in the IEntityTypeConfiguration interface.
    /// It configures the AppUser entity using the provided EntityTypeBuilder.
    /// </summary>
    /// <param name="builder">The entity type builder for the RefreshToken entity.</param>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id); // Sets the primary key for the entity to the "Id" property

        // Defines the relationship between RefreshToken and Users entities using the foreign key UserId.
        // When a user is deleted, its associated refresh tokens will also be deleted (Cascade delete).
        builder.HasOne(x => x.ApplicationUser)
            .WithMany(y => y.RefreshTokens)
            .HasForeignKey(z => z.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
