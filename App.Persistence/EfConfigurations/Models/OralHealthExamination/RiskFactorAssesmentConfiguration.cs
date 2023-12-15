using App.Domain.Models.OralHealthExamination.RiskFactorAssessmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.OralHealthExamination;

internal class RiskFactorAssesmentConfiguration : IEntityTypeConfiguration<RiskFactorAssessment>
{
    public void Configure(EntityTypeBuilder<RiskFactorAssessment> builder)
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
