using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;
/// <summary>
///  Represents the configuration for the PracticeAPI entity.
/// </summary>
public class PracticeAPIConfiguration : IEntityTypeConfiguration<PracticeAPI>
{
    /// <summary>
    ///  Configures the properties of the PracticeAPI entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<PracticeAPI> builder)
    {
        // Set the primary key
        builder.HasKey(k => k.Id);

        // Set the properties of the PracticeAPI entity
        builder.Property(e => e.Comment)
            .HasMaxLength(500);

        // Set the relationships between entities
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
