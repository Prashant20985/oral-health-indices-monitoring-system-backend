using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <summary>
    /// Implements the Configure method defined in the IEntityTypeConfiguration interface.
    /// It configures the AppUser entity using the provided EntityTypeBuilder.
    /// </summary>
    /// <param name="builder">The entity type builder for the ApplicationUser entity.</param>
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Configures the FirstName property of the ApplicationUser entity.
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(225);

        // Configures the LastName property of the ApplicationUser entity.
        builder.Property(x => x.LastName)
            .HasMaxLength(225);

        // Configures the Delete Comment property of the ApplicationUser entity.
        builder.Property(x => x.DeleteUserComment)
            .HasMaxLength(500);

        // Configures the Guest User Comment property of the ApplicationUser entity.
        builder.Property(x => x.GuestUserComment)
            .HasMaxLength(500);

        // Configures the Created At property of the ApplicationUser entity.
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // Configures the Email property of the ApplicationUser entity.
        builder.Property(x => x.Email)
            .IsRequired();

        // Defines the relationship between ApplicationUser and ApplicationUserRole entities using the foreign key UserId.
        builder.HasMany(x => x.ApplicationUserRoles)
            .WithOne(e => e.ApplicationUser)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        // Ignores the following properties of the ApplicationUser entity.
        builder.Ignore(x => x.AccessFailedCount);
        builder.Ignore(x => x.EmailConfirmed);
        builder.Ignore(x => x.ConcurrencyStamp);
        builder.Ignore(x => x.LockoutEnabled);
        builder.Ignore(x => x.LockoutEnd);
        builder.Ignore(x => x.PhoneNumberConfirmed);
        builder.Ignore(x => x.TwoFactorEnabled);
    }
}
