using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;
/// <summary>
///  Represents the configuration for the PracticePatientExaminationCard entity.
/// </summary>
public class PracticePatientExaminationCardConfiguration
    : IEntityTypeConfiguration<PracticePatientExaminationCard>
{
    /// <summary>
    ///  Configures the properties of the PracticePatientExaminationCard entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<PracticePatientExaminationCard> builder)
    {
        // Set the primary key
        builder.HasKey(e => e.Id);

        // Set the properties of the PracticePatientExaminationCard entity
        builder.Property(x => x.StudentMark)
            .HasPrecision(5, 2);

        builder.Property(x => x.DoctorComment)
            .HasMaxLength(500);

        builder.Property(x => x.NeedForDentalInterventions)
            .HasMaxLength(1);

        builder.Property(x => x.ProposedTreatment)
            .HasMaxLength(500);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.PatientRecommendations)
            .HasMaxLength(500);

        // Set the relationships between entities
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
