using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;
/// <summary>
///  Represents the configuration for the PatientExaminationCard entity.
/// </summary>
internal class PatientExaminationCardConfiguration
    : IEntityTypeConfiguration<PatientExaminationCard>
{
    /// <summary>
    ///  Configures the properties of the PatientExaminationCard entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<PatientExaminationCard> builder)
    {
        // Set the primary key
        builder.HasKey(e => e.Id);

        // Set the properties of the PatientExaminationCard entity
        builder.Property(e => e.DoctorComment)
            .HasMaxLength(500);

        builder.Property(e => e.StudentComment)
            .HasMaxLength(500);

        builder.Property(e => e.TotalScore)
            .HasColumnType("decimal(5, 2)");

        builder.Property(e => e.NeedForDentalInterventions)
            .HasMaxLength(1);

        builder.Property(e => e.ProposedTreatment)
            .HasMaxLength(500);

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.Property(e => e.PatientRecommendations)
            .HasMaxLength(500);

        // Set the relationships between entities
        builder.HasOne(e => e.Patient)
            .WithMany(e => e.PatientExaminationCards)
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Doctor)
            .WithMany(e => e.PatientExaminationCardDoctorNavigation)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Student)
            .WithMany(e => e.PatientExaminationCardStudentNavigation)
            .HasForeignKey(e => e.StudentId)
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
