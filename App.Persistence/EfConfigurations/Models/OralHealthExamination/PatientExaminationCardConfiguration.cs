using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

internal class PatientExaminationCardConfiguration
    : IEntityTypeConfiguration<PatientExaminationCard>
{
    public void Configure(EntityTypeBuilder<PatientExaminationCard> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DoctorComment)
            .HasMaxLength(500);

        builder.HasOne(e => e.Patient)
            .WithMany(e => e.PatientExaminationCards)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PatientExaminationRegularMode)
            .WithOne(e => e.PatientExaminationCard)
            .HasForeignKey<PatientExaminationCard>(e => e.PatientExaminationRegularModeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PatientExaminationTestMode)
            .WithOne(e => e.PatientExaminationCard)
            .HasForeignKey<PatientExaminationCard>(e => e.PatientExaminationTestModeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.RiskFactorAssessment)
            .WithOne(e => e.PatientExaminationCard)
            .HasForeignKey<PatientExaminationCard>(e => e.RiskFactorAssesmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PatientExaminationResult)
            .WithOne(e => e.PatientExaminationCard)
            .HasForeignKey<PatientExaminationCard>(e => e.PatientExaminationResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
