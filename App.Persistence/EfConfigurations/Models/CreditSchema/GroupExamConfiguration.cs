using App.Domain.Models.CreditSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.CreditSchema;

public class GroupExamConfiguration : IEntityTypeConfiguration<GroupExam>
{
    public void Configure(EntityTypeBuilder<GroupExam> builder)
    {
        builder.HasKey(e => new { e.GroupId, e.ExamId });

        builder.HasOne(e => e.Group)
            .WithMany(e => e.GroupExams)
            .HasForeignKey(e => e.GroupId);

        builder.HasOne(e => e.Exam)
            .WithMany(e => e.GroupExams)
            .HasForeignKey(e => e.ExamId);
    }
}
