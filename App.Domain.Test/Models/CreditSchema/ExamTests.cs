using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.Enums;

namespace App.Domain.Test.Models.CreditSchema;

public class ExamTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var dateOfExamination = DateTime.Now;
        var examTitle = "Final Exam";
        var description = "Final exam description";
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(12, 0);
        var durationInterval = new TimeSpan(3, 0, 0);
        var maxMark = 100;
        var groupId = Guid.NewGuid();

        // Act
        var exam = new Exam(dateOfExamination, examTitle, description, startTime, endTime, durationInterval, maxMark,
            groupId);

        // Assert
        Assert.NotEqual(Guid.Empty, exam.Id);
        Assert.Equal(dateOfExamination, exam.DateOfExamination);
        Assert.Equal(examTitle, exam.ExamTitle);
        Assert.Equal(description, exam.Description);
        Assert.Equal(DateTime.UtcNow.Date, exam.PublishDate.Date);
        Assert.Equal(startTime, exam.StartTime);
        Assert.Equal(endTime, exam.EndTime);
        Assert.Equal(durationInterval, exam.DurationInterval);
        Assert.Equal(maxMark, exam.MaxMark);
        Assert.Equal(ExamStatus.Published, exam.ExamStatus);
        Assert.Equal(groupId, exam.GroupId);
        Assert.Empty(exam.PracticePatientExaminationCards);
    }

    [Fact]
    public void MarksAsGraded_ShouldUpdateExamStatus()
    {
        // Arrange
        var exam = new Exam(DateTime.Now, "Final Exam", "Final exam description", new TimeOnly(9, 0),
            new TimeOnly(12, 0), new TimeSpan(3, 0, 0), 100, Guid.NewGuid());

        // Act
        exam.MarksAsGraded();

        // Assert
        Assert.Equal(ExamStatus.Graded, exam.ExamStatus);
    }

    [Fact]
    public void UpdateExam_ShouldUpdateProperties()
    {
        // Arrange
        var exam = new Exam(DateTime.Now, "Final Exam", "Final exam description", new TimeOnly(9, 0),
            new TimeOnly(12, 0), new TimeSpan(3, 0, 0), 100, Guid.NewGuid());
        var updateExamDto = new UpdateExamDto
        {
            DateOfExamination = DateTime.Now.AddDays(1),
            ExamTitle = "Updated Exam",
            Description = "Updated description",
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(13, 0),
            DurationInterval = new TimeSpan(3, 0, 0),
            MaxMark = 150
        };

        // Act
        exam.UpdateExam(updateExamDto);

        // Assert
        Assert.Equal(updateExamDto.DateOfExamination, exam.DateOfExamination);
        Assert.Equal(updateExamDto.ExamTitle, exam.ExamTitle);
        Assert.Equal(updateExamDto.Description, exam.Description);
        Assert.Equal(updateExamDto.StartTime, exam.StartTime);
        Assert.Equal(updateExamDto.EndTime, exam.EndTime);
        Assert.Equal(updateExamDto.DurationInterval, exam.DurationInterval);
        Assert.Equal(updateExamDto.MaxMark, exam.MaxMark);
    }
}