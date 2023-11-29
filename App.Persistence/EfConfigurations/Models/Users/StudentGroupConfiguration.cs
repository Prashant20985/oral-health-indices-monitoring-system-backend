using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users
{
    /// <summary>
    /// Entity configuration for the StudentGroup model in the database.
    /// </summary>
    internal class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
    {
        /// <summary>
        /// Configures the entity properties for the StudentGroup model.
        /// </summary>
        /// <param name="builder">The entity type builder for the StudentGroup model.</param>
        public void Configure(EntityTypeBuilder<StudentGroup> builder)
        {
            builder.HasKey(x => new { x.GroupId, x.StudentId });

            builder.HasOne(x => x.Group)
                .WithMany(y => y.StudentGroups)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.StudentGroups)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
