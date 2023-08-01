using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users;

/// <summary>
/// Entity Framework configuration for the <see cref="RefreshToken"/> entity.
/// </summary>
internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Configures the entity mappings for the <see cref="RefreshToken"/> entity.
    /// </summary>
    /// <param name="builder">The builder to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshToken"); // Sets the table name to "RefreshToken"

        builder.HasKey(x => x.Id); // Sets the primary key for the entity to the "Id" property

        // Defines the relationship between RefreshToken and Users entities using the foreign key UserId.
        // When a user is deleted, its associated refresh tokens will also be deleted (Cascade delete).
        builder.HasOne(x => x.Users)
            .WithMany(y => y.RefreshTokens)
            .HasForeignKey(z => z.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
