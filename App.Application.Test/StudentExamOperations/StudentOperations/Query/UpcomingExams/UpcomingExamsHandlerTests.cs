using App.Application.StudentExamOperations.StudentOperations.Query.UpcomingExams;
using App.Domain.DTOs.ExamDtos.Response;
using Moq;

namespace App.Application.Test.StudentExamOperations.StudentOperations.Query.UpcomingExams;

public class UpcomingExamsHandlerTests : TestHelper
{
    private readonly UpcomingExamsHandler handler;
    private readonly UpcominExamsQuery query;

    public UpcomingExamsHandlerTests()
    {
        handler = new UpcomingExamsHandler(studentExamRepositoryMock.Object);
        query = new UpcominExamsQuery("studentId");
    }

    [Fact]
    public async Task Handle_WhenExamsExist_ShouldReturnExams()
    {
        // Arrange
        var exams = new List<ExamDto>
        {
            new ExamDto
            {
                Id = Guid.NewGuid(),
                DateOfExamination = DateTime.Now.AddDays(1),
                ExamTitle = "Exam 1",
                Description = "Description 1",
                PublishDate = DateTime.Now,
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(12, 0),
                DurationInterval = new TimeSpan(2, 0, 0),
                MaxMark = 100
            }
        };

        studentExamRepositoryMock.Setup(x => x.UpcomingExams(It.IsAny<string>()))
            .ReturnsAsync(exams);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(exams.Count, result.ResultValue.Count);
        Assert.Equal(exams.First().Id, result.ResultValue.First().Id);
    }

    [Fact]
    public async Task Handle_WhenExamsDoNotExist_ShouldReturnNull()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.UpcomingExams(It.IsAny<string>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ResultValue);
    }
}
