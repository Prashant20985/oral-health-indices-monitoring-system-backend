using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;
/// <summary>
///  Represents the configuration for the Exam entity.
/// </summary>
public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    /// <summary>
    ///  Configures the properties of the Exam entity and the relationships between entities.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        // Set the primary key
        builder.HasKey(e => e.Id);

        // Set the properties of the Exam entity
        builder.Property(x => x.DateOfExamination)
            .IsRequired();
        
        builder.Property(x => x.ExamTitle)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.PublishDate)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.DurationInterval)
            .IsRequired();

        builder.Property(x => x.ExamStatus)
            .HasConversion<string>()
            .IsRequired();

        // Set the relationships between entities
        builder.HasOne(x => x.Group)
            .WithMany(x => x.Exams)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
