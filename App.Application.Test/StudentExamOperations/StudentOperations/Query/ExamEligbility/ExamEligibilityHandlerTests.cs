using App.Application.StudentExamOperations.StudentOperations.Query.ExamEligbility;
using Moq;

namespace App.Application.Test.StudentExamOperations.StudentOperations.Query.ExamEligbility;

public class ExamEligibilityHandlerTests : TestHelper
{
    private readonly ExamEligibilityHandler handler;
    private readonly ExamEligibiltyQuery query;

    public ExamEligibilityHandlerTests()
    {
        handler = new ExamEligibilityHandler(studentExamRepositoryMock.Object);
        query = new ExamEligibiltyQuery(Guid.NewGuid(), "studentId");
    }

    [Fact]
    public async Task Handle_WhenStudentHasAlreadyTakenTheExam_ShouldReturnFalse()
    {
        // Arrange
        studentExamRepositoryMock
            .Setup(x => x.CheckIfStudentHasAlreadyTakenTheExam(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.False(result.ResultValue);
    }

    [Fact]
    public async Task Handle_WhenStudentHasNotTakenTheExam_ShouldReturnTrue()
    {
        // Arrange
        studentExamRepositoryMock
            .Setup(x => x.CheckIfStudentHasAlreadyTakenTheExam(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.True(result.ResultValue);
    }
}