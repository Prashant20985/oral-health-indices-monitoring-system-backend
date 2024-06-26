﻿using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;

public class PracticePatientExaminationResultConfiguration
    : IEntityTypeConfiguration<PracticePatientExaminationResult>
{
    public void Configure(EntityTypeBuilder<PracticePatientExaminationResult> builder)
    {
        builder.HasKey(x => x.Id);

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
