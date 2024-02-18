using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;

public class PracticePatientConfiguration
    : IEntityTypeConfiguration<PracticePatient>
{
    public void Configure(EntityTypeBuilder<PracticePatient> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.Gender)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.EthnicGroup)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.OtherGroup)
            .HasMaxLength(50);

        builder.Property(x => x.OtherData)
            .HasMaxLength(50);

        builder.Property(x => x.OtherData2)
            .HasMaxLength(50);

        builder.Property(x => x.OtherData3)
            .HasMaxLength(50);

        builder.Property(x => x.Location)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Age)
            .IsRequired();
    }
}
