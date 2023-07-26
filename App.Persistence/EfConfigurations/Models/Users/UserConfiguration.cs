using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Implements the Configure method defined in the IEntityTypeConfiguration interface.
    /// It configures the AppUser entity using the provided EntityTypeBuilder.
    /// </summary>
    /// <param name="builder">The entity type builder for the AppUser entity.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Configures the FirstName property of the AppUser entity.
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(225);

        // Configures the LastName property of the AppUser entity.
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(225);

        // Configures the Delete Comment property of the AppUser entity.
        builder.Property(x => x.DeleteUserComment)
            .HasMaxLength(500);

        // Configures the Delete Comment property of the AppUser entity.
        builder.Property(x => x.GuestUserComment)
            .HasMaxLength(500);

        // Ignores the following properties of the AppUser entity.
        builder.Ignore(x => x.AccessFailedCount);
        builder.Ignore(x => x.EmailConfirmed);
        builder.Ignore(x => x.ConcurrencyStamp);
        builder.Ignore(x => x.LockoutEnabled);
        builder.Ignore(x => x.LockoutEnd);
        builder.Ignore(x => x.PhoneNumberConfirmed);
        builder.Ignore(x => x.TwoFactorEnabled);
    }
}
