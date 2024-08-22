using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

/// <summary>
/// Represents the configuration for the PatientExaminationResult entity.
/// </summary>
internal class PatientExaminationResultConfiguration
    : IEntityTypeConfiguration<PatientExaminationResult>
{
    /// <summary>
    ///  Configures the properties of the PatientExaminationResult entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<PatientExaminationResult> builder)
    {
        // Set the primary key
        builder.HasKey(x => x.Id);

        // Set the relationships between entities
        builder.HasOne(x => x.Bewe)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PatientExaminationResult>(x => x.BeweId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.DMFT_DMFS)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PatientExaminationResult>(x => x.DMFT_DMFSId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.API)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PatientExaminationResult>(x => x.APIId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Bleeding)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PatientExaminationResult>(x => x.BleedingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
