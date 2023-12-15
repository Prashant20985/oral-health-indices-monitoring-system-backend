using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

internal class PatientExaminationTestModeConfiguration
    : IEntityTypeConfiguration<PatientExaminationTestMode>
{
    public void Configure(EntityTypeBuilder<PatientExaminationTestMode> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DateOfExamination)
            .IsRequired();

        builder.HasOne(e => e.Doctor)
            .WithMany(e => e.PatientExaminationTestModeDoctorNavigation)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Student)
            .WithMany(e => e.PatientExaminationTestModeStudentNavigation)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
