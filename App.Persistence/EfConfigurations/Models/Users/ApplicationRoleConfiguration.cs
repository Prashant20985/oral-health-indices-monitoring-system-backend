using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users;

internal class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    /// <summary>
    /// Implements the Configure method defined in the IEntityTypeConfiguration interface.
    /// It configures the AppUser entity using the provided EntityTypeBuilder.
    /// </summary>
    /// <param name="builder">The entity type builder for the ApplicationRole entity.</param>
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasKey(x => x.Id);

        // Defines the relationship between ApplicationRole and ApplicationUserRole entities using the foreign key RoleId.
        builder.HasMany(x => x.ApplicationUserRoles)
            .WithOne(e => e.ApplicationRole)
            .HasForeignKey(r => r.RoleId)
            .IsRequired();
    }
}
