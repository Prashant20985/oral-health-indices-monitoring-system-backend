using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users;

/// <summary>
///  Represents the configuration for the ApplicationUserRole entity.
/// </summary>
internal class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    /// <summary>
    ///  Configures the properties of the ApplicationUserRole entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        // Set the primary key
        builder.HasKey(x => new { x.UserId, x.RoleId });
    }
}
