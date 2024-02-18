using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;

public class PracticeBeweConfiguration
    : IEntityTypeConfiguration<PracticeBewe>
{
    public void Configure(EntityTypeBuilder<PracticeBewe> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BeweResult)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Comment)
            .HasMaxLength(500);

        builder.OwnsOne(x => x.AssessmentModel, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.ToJson();

            ownedNavigationBuilder.OwnsOne(y => y.Sectant1,
                ownedOwnedNavigationBuilder =>
                {
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_17);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_16);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_15);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_14);
                });

            ownedNavigationBuilder.OwnsOne(y => y.Sectant2,
                ownedOwnedNavigationBuilder =>
                {
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_13);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_12);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_11);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_21);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_22);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_23);
                });

            ownedNavigationBuilder.OwnsOne(y => y.Sectant3,
                ownedOwnedNavigationBuilder =>
                {
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_24);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_25);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_26);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_27);
                });

            ownedNavigationBuilder.OwnsOne(y => y.Sectant4,
                ownedOwnedNavigationBuilder =>
                {
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_34);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_35);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_36);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_37);
                });

            ownedNavigationBuilder.OwnsOne(y => y.Sectant5,
                ownedOwnedNavigationBuilder =>
                {
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_43);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_42);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_41);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_31);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_32);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_33);
                });

            ownedNavigationBuilder.OwnsOne(y => y.Sectant6,
                ownedOwnedNavigationBuilder =>
                {
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_47);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_46);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_45);
                    ownedOwnedNavigationBuilder.OwnsOne(z => z.Tooth_44);
                });
        });
    }
}
