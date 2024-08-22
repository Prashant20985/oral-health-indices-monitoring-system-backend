using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;
/// <summary>
///  Represents the configuration for the DMFT_DMFS entity.
/// </summary>
internal class DMFT_DMFSConfiguration : IEntityTypeConfiguration<DMFT_DMFS>
{
    /// <summary>
    ///  Configures the properties of the DMFT_DMFS entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<DMFT_DMFS> builder)
    {
        // Set the primary key
        builder.HasKey(x => x.Id);

        // Set the properties of the DMFT_DMFS entity
        builder.Property(x => x.DMFTResult)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.DMFSResult)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.DoctorComment)
            .HasMaxLength(500);

        builder.Property(x => x.StudentComment)
            .HasMaxLength(500);

        builder.Property(x => x.ProstheticStatus)
            .HasMaxLength(1);

        // Set the relationships between entities
        builder.OwnsOne(x => x.AssessmentModel, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.ToJson();

            ownedNavigationBuilder.OwnsOne(y => y.UpperMouth,
                ownedOwnedNavigationBuilder =>
                {
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_18);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_17);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_16);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_15);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_14);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_13);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_12);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_11);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_21);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_22);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_23);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_24);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_25);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_26);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_27);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_28);
                });

            ownedNavigationBuilder.OwnsOne(y => y.LowerMouth,
                ownedOwnedNavigationBuilder =>
                {
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_48);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_47);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_46);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_45);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_44);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_43);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_42);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_41);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_31);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_32);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_33);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_34);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_35);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_36);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_37);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_38);
                });
        });
    }
}
