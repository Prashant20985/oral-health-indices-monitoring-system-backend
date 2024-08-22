using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;
/// <summary>
///  Represents the configuration for the PracticePatientExaminationResult entity.
/// </summary>
public class PracticePatientExaminationResultConfiguration
    : IEntityTypeConfiguration<PracticePatientExaminationResult>
{
    /// <summary>
    ///  Configures the properties of the PracticePatientExaminationResult entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<PracticePatientExaminationResult> builder)
    {
        // Set the primary key
        builder.HasKey(x => x.Id);

        // Set the properties of the PracticePatientExaminationResult entity
        builder.HasOne(x => x.API)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PracticePatientExaminationResult>(x => x.APIId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Bleeding)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PracticePatientExaminationResult>(x => x.BleedingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Bewe)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PracticePatientExaminationResult>(x => x.BeweId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.DMFT_DMFS)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PracticePatientExaminationResult>(x => x.DMFT_DMFSId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
