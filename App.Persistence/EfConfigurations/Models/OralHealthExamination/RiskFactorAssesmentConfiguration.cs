using App.Domain.Models.OralHealthExamination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

/// <summary>
///  Represents the configuration for the RiskFactorAssessment entity.
/// </summary>
internal class RiskFactorAssesmentConfiguration : IEntityTypeConfiguration<RiskFactorAssessment>
{
    /// <summary>
    ///  Configures the properties of the RiskFactorAssessment entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<RiskFactorAssessment> builder)
    {
        // Set the primary key
        builder.HasKey(x => x.Id);

        // Set the properties of the RiskFactorAssessment entity
        builder.OwnsOne(x => x.RiskFactorAssessmentModel, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.ToJson();
            ownedNavigationBuilder.OwnsMany(y => y.Questions,
                ownedOwnedNavigationBuilder => ownedOwnedNavigationBuilder.OwnsOne(z => z.Answer));
        });
    }
}
