using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

/// <summary>
///  Represents the configuration for the Patient entity.
/// </summary>
internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    /// <summary>
    ///  Configures the properties of the Patient entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        // Set the primary key
        builder.HasKey(x => x.Id);

        // Set the properties of the Patient entity
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.Gender)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.EthnicGroup)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.OtherGroup)
            .HasMaxLength(50);

        builder.Property(x => x.OtherData)
            .HasMaxLength(50);

        builder.Property(x => x.OtherData2)
            .HasMaxLength(50);

        builder.Property(x => x.OtherData3)
            .HasMaxLength(50);

        builder.Property(x => x.Location)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Age)
            .IsRequired();

        builder.Property(x => x.ArchiveComment)
            .HasMaxLength(500);

        // Set the relationships between entities
        builder.HasOne(x => x.ResearchGroup)
            .WithMany(x => x.Patients)
            .HasForeignKey(x => x.ResearchGroupId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Doctor)
            .WithMany(x => x.Patients)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}