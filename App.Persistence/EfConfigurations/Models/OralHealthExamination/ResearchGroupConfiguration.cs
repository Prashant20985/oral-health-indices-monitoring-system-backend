using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

internal class ResearchGroupConfiguration : IEntityTypeConfiguration<ResearchGroup>
{
    public void Configure(EntityTypeBuilder<ResearchGroup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.GroupName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasOne(x => x.Doctor)
            .WithMany(x => x.PatientGroups)
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
