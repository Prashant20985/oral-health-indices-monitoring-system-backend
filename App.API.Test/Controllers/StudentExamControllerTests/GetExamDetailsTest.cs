using App.Application.Core;
using App.Application.StudentExamOperations.CommonOperations.Query.ExamDetails;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class GetExamDetailsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public GetExamDetailsTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetExamDetails_WhenCalled_ReturnsExamDetails()
    {
        // Arrange
        var examId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "Exam1", "Description1", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, examId);

        var expectedExamDetails = new ExamDto
        {
            Id = exam.Id,
            DateOfExamination = exam.DateOfExamination,
            ExamTitle = exam.ExamTitle,
            Description = exam.Description,
            PublishDate = exam.PublishDate,
            StartTime = exam.StartTime,
            EndTime = exam.EndTime,
            DurationInterval = exam.DurationInterval,
            MaxMark = exam.MaxMark,
            ExamStatus = exam.ExamStatus.ToString()
        };

        _mediator.Setup(m => m.Send(It.IsAny<FetchExamDetailsQuery>(), default))
            .ReturnsAsync(OperationResult<ExamDto>.Success(expectedExamDetails));

        // Act
        var result = await _studentExamController.GetExamDetails(examId);

        // Assert
        var okResult = Assert.IsType<ActionResult<ExamDto>>(result);
        var examDetails = Assert.IsType<OkObjectResult>(okResult.Result);
        Assert.Equal(expectedExamDetails, examDetails.Value);
    }

    [Fact]
    public async Task GetExamDetails_WhenCalledWithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        var examId = Guid.NewGuid();

        _mediator.Setup(m => m.Send(It.IsAny<FetchExamDetailsQuery>(), default))
            .ReturnsAsync(OperationResult<ExamDto>.Failure("exam not found"));

        // Act
        var result = await _studentExamController.GetExamDetails(examId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ExamDto>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal("exam not found", badRequestResult.Value);
    }
}