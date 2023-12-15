using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

internal class PatientExaminationResultConfiguration
    : IEntityTypeConfiguration<PatientExaminationResult>
{
    public void Configure(EntityTypeBuilder<PatientExaminationResult> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Bewe)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PatientExaminationResult>(x => x.BeweId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.DMFT_DMFS)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PatientExaminationResult>(x => x.DMFT_DMFSId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.APIBleeding)
            .WithOne(x => x.PatientExaminationResult)
            .HasForeignKey<PatientExaminationResult>(x => x.APIBleedingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
