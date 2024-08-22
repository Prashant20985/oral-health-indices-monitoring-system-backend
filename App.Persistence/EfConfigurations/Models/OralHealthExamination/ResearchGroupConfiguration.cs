using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;
/// <summary>
///  Represents the configuration for the ResearchGroup entity.
/// </summary>
internal class ResearchGroupConfiguration : IEntityTypeConfiguration<ResearchGroup>
{
    /// <summary>
    ///  Configures the properties of the ResearchGroup entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<ResearchGroup> builder)
    {
        // Set the primary key
        builder.HasKey(x => x.Id);

        // Set the properties of the ResearchGroup entity
        builder.Property(x => x.GroupName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        //  Set the relationships between entities
        builder.HasOne(x => x.Doctor)
            .WithMany(x => x.PatientGroups)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
