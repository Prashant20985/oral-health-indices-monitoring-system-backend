using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;

public class PracticePatientExaminationCardConfiguration
    : IEntityTypeConfiguration<PracticePatientExaminationCard>
{
    public void Configure(EntityTypeBuilder<PracticePatientExaminationCard> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(x => x.StudentMark)
            .HasPrecision(5, 2);

        builder.Property(x => x.DoctorComment)
            .HasMaxLength(500);

        builder.Property(x => x.NeedForDentalInterventions)
            .HasMaxLength(500);

        builder.Property(x => x.ProposedTreatment)
            .HasMaxLength(500);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.PatientRecommendations)
            .HasMaxLength(500);

        builder.HasOne(e => e.PracticeRiskFactorAssessment)
            .WithOne(e => e.PracticePatientExaminationCard)
            .HasForeignKey<PracticePatientExaminationCard>(e => e.RiskFactorAssessmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PracticePatientExaminationResult)
            .WithOne(e => e.PracticePatientExaminationCard)
            .HasForeignKey<PracticePatientExaminationCard>(e => e.PatientExaminationResultId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Exam)
            .WithMany(e => e.PracticePatientExaminationCards)
            .HasForeignKey(e => e.ExamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PracticePatient)
            .WithOne(e => e.PracticePatientExaminationCard)
            .HasForeignKey<PracticePatientExaminationCard>(e => e.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Student)
            .WithMany(e => e.PracticePatientExaminationCards)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
