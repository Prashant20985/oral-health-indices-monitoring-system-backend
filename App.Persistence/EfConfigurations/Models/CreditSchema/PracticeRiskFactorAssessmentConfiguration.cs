using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;

/// <summary>
///  Represents the configuration for the PracticeRiskFactorAssessment entity.
/// </summary>
public class PracticeRiskFactorAssessmentConfiguration
    : IEntityTypeConfiguration<PracticeRiskFactorAssessment>
{
    /// <summary>
    ///  Configures the properties of the PracticeRiskFactorAssessment entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<PracticeRiskFactorAssessment> builder)
    {
        // Set the primary key
        builder.HasKey(x => x.Id);

        // Set the properties of the PracticeRiskFactorAssessment entity
        builder.OwnsOne(x => x.RiskFactorAssessmentModel, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.ToJson();
            ownedNavigationBuilder.OwnsMany(y => y.Questions,
                ownedOwnedNavigationBuilder => ownedOwnedNavigationBuilder.OwnsOne(z => z.Answer));
        });
    }
}
