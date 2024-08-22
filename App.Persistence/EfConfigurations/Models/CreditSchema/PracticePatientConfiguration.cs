using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;
/// <summary>
///  Represents the configuration for the PracticePatient entity.
/// </summary>
public class PracticePatientConfiguration
    : IEntityTypeConfiguration<PracticePatient>
{
    /// <summary>
    ///  Configures the properties of the PracticePatient entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<PracticePatient> builder)
    {
        // Set the primary key
        builder.HasKey(x => x.Id);
        
        // Set the properties of the PracticePatient entity
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

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
