using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users;

/// <summary>
/// Configuration for the Supervise entity and its relations with other entities in the database.
/// </summary>
public class SuperviseConfiguration : IEntityTypeConfiguration<Supervise>
{
    /// <summary>
    /// Configures the entity of the Supervise class.
    /// </summary>
    /// <param name="builder">The builder used to configure the Supervise entity.</param>
    public void Configure(EntityTypeBuilder<Supervise> builder)
    {
        // Set the primary key of the entity as the Id
        builder.HasKey(s => s.Id);

        // Set the relationships between entities
        builder.HasOne(s => s.Doctor)
            .WithMany(x => x.SuperviseDoctorNavigation)
            .HasForeignKey(s => s.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Student)
            .WithMany(x => x.SuperviseStudentNavigation)
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
