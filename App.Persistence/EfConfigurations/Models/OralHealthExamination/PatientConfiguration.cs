using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(x => x.Id);

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

        builder.HasOne(x => x.PatientGroup)
            .WithMany(x => x.Patients)
            .HasForeignKey(x => x.PatientGroupId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Doctor)
            .WithMany(x => x.Patients)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}