using App.Application.StudentExamOperations.TeacherOperations.Command.PublishExam;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.Enums;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.PublishExam;

public class PublishExamHandlerTests : TestHelper
{
    private readonly PublishExamHandler handler;
    private readonly PublishExamCommand command;

    public PublishExamHandlerTests()
    {
        var publishExamDto = new PublishExamDto();
        handler = new PublishExamHandler(studentExamRepositoryMock.Object);
        command = new PublishExamCommand(publishExamDto);
    }

    [Fact]
    public async Task Handle_WhenExamExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, Guid.NewGuid());

        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(exam);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(ExamStatus.Published, exam.ExamStatus);
    }
}