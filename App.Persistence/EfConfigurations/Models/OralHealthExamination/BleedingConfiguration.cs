﻿using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

public class BleedingConfiguration : IEntityTypeConfiguration<Bleeding>
{
    public void Configure(EntityTypeBuilder<Bleeding> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(e => e.DoctorComment)
            .HasMaxLength(500);

        builder.Property(e => e.StudentComment)
            .HasMaxLength(500);

        builder.OwnsOne(e => e.AssessmentModel, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.ToJson();

            ownedNavigationBuilder.OwnsOne(y => y.Quadrant1, ownedOwnedNavigationBuilder =>
            {
                ownedOwnedNavigationBuilder.Property(z => z.Value1);
                ownedOwnedNavigationBuilder.Property(z => z.Value2);
                ownedOwnedNavigationBuilder.Property(z => z.Value3);
                ownedOwnedNavigationBuilder.Property(z => z.Value4);
                ownedOwnedNavigationBuilder.Property(z => z.Value5);
                ownedOwnedNavigationBuilder.Property(z => z.Value6);
                ownedOwnedNavigationBuilder.Property(z => z.Value7);
            });

            ownedNavigationBuilder.OwnsOne(y => y.Quadrant2, ownedOwnedNavigationBuilder =>
            {
                ownedOwnedNavigationBuilder.Property(z => z.Value1);
                ownedOwnedNavigationBuilder.Property(z => z.Value2);
                ownedOwnedNavigationBuilder.Property(z => z.Value3);
                ownedOwnedNavigationBuilder.Property(z => z.Value4);
                ownedOwnedNavigationBuilder.Property(z => z.Value5);
                ownedOwnedNavigationBuilder.Property(z => z.Value6);
                ownedOwnedNavigationBuilder.Property(z => z.Value7);
            });

            ownedNavigationBuilder.OwnsOne(y => y.Quadrant3, ownedOwnedNavigationBuilder =>
            {
                ownedOwnedNavigationBuilder.Property(z => z.Value1);
                ownedOwnedNavigationBuilder.Property(z => z.Value2);
                ownedOwnedNavigationBuilder.Property(z => z.Value3);
                ownedOwnedNavigationBuilder.Property(z => z.Value4);
                ownedOwnedNavigationBuilder.Property(z => z.Value5);
                ownedOwnedNavigationBuilder.Property(z => z.Value6);
                ownedOwnedNavigationBuilder.Property(z => z.Value7);
            });

            ownedNavigationBuilder.OwnsOne(y => y.Quadrant4, ownedOwnedNavigationBuilder =>
            {
                ownedOwnedNavigationBuilder.Property(z => z.Value1);
                ownedOwnedNavigationBuilder.Property(z => z.Value2);
                ownedOwnedNavigationBuilder.Property(z => z.Value3);
                ownedOwnedNavigationBuilder.Property(z => z.Value4);
                ownedOwnedNavigationBuilder.Property(z => z.Value5);
                ownedOwnedNavigationBuilder.Property(z => z.Value6);
                ownedOwnedNavigationBuilder.Property(z => z.Value7);
            });
        });
    }
}
