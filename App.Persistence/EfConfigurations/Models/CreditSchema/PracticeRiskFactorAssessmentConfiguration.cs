using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;

public class PracticeRiskFactorAssessmentConfiguration
    : IEntityTypeConfiguration<PracticeRiskFactorAssessment>
{
    public void Configure(EntityTypeBuilder<PracticeRiskFactorAssessment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.RiskFactorAssessmentModel, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.ToJson();
            ownedNavigationBuilder.OwnsMany(y => y.Questions,
                ownedOwnedNavigationBuilder => ownedOwnedNavigationBuilder.OwnsOne(z => z.Answer));
        });
    }
}
