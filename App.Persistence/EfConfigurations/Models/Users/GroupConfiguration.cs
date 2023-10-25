using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users;

/// <summary>
/// Entity configuration for the Group model in the database.
/// </summary>
internal class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    /// <summary>
    /// Configures the entity properties for the Group model.
    /// </summary>
    /// <param name="builder">The entity type builder for the Group model.</param>
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.GroupName)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.Groups)
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
