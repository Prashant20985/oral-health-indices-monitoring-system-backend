using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Query.ExamResults;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class GetStudentExamResultsTests
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public GetStudentExamResultsTests()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetStudentExamResults_WhenCalled_ReturnsStudentExamResults()
    {
        // Arrange
        var examId = Guid.NewGuid();

        var studentExamResult1 = new StudentExamResultResponseDto
        {
            UserName = "test",
            FirstName = "test",
            LastName = "test",
            Email = "Test@test.com",
            StudentMark = 100
        };

        var studentExamResult2 = new StudentExamResultResponseDto
        {
            UserName = "test2",
            FirstName = "test2",
            LastName = "test2",
            Email = "test2@test.com",
            StudentMark = 90
        };

        var expectedStudentExamResults = new List<StudentExamResultResponseDto>
        {
            studentExamResult1,
            studentExamResult2
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchExamResultsQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentExamResultResponseDto>>.Success(expectedStudentExamResults));

        // Act
        var result = await _studentExamController.GetStudentExamResults(examId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<StudentExamResultResponseDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var actualResults = Assert.IsType<List<StudentExamResultResponseDto>>(okResult.Value);

        Assert.Equal(expectedStudentExamResults.Count, actualResults.Count);
        Assert.Equal(expectedStudentExamResults[0].UserName, actualResults[0].UserName);
        Assert.Equal(expectedStudentExamResults[1].UserName, actualResults[1].UserName);
    }

    [Fact]
    public async Task GetStudentExamResults_WhenExamNotFound_ReturnsNotBadRequest()
    {
        // Arrange
        var examId = Guid.NewGuid();

        _mediator.Setup(x => x.Send(It.IsAny<FetchExamResultsQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentExamResultResponseDto>>.Failure("Exam not found."));

        // Act
        var result = await _studentExamController.GetStudentExamResults(examId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<StudentExamResultResponseDto>>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        Assert.Equal("Exam not found.", badRequestResult.Value);
    }
}
