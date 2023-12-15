using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

internal class PatientExaminationRegularModeConfiguration
    : IEntityTypeConfiguration<PatientExaminationRegularMode>
{
    public void Configure(EntityTypeBuilder<PatientExaminationRegularMode> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DateOfExamination)
            .IsRequired();

        builder.HasOne(e => e.Doctor)
            .WithMany(e => e.PatientExaminationRegularModeDoctorNavigation)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
