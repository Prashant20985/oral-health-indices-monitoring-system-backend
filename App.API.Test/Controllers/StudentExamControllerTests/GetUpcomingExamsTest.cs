using System.Security.Claims;
using App.Application.Core;
using App.Application.StudentExamOperations.StudentOperations.Query.UpcomingExams;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class GetUpcomingExamsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public GetUpcomingExamsTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetUpcomingExams_WithValidRequest_ShouldReturnUpcomingExams()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.MaxValue, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var upcomingExams = new List<ExamDto>
        {
            new()
            {
                Id = exam.Id,
                DateOfExamination = exam.DateOfExamination,
                ExamTitle = exam.ExamTitle,
                Description = exam.Description,
                PublishDate = exam.PublishDate,
                StartTime = exam.StartTime,
                EndTime = exam.EndTime,
                DurationInterval = exam.DurationInterval,
                MaxMark = exam.MaxMark
            }
        };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.NameIdentifier, studentId.ToString())
        }));

        _studentExamController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };

        _mediator.Setup(x => x.Send(It.IsAny<UpcominExamsQuery>(), default))
            .ReturnsAsync(OperationResult<List<ExamDto>>.Success(upcomingExams));

        // Act
        var result = await _studentExamController.GetUpcomingExams();

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<ExamDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedExams = Assert.IsType<List<ExamDto>>(okResult.Value);
        Assert.Equal(upcomingExams, returnedExams);
    }

    [Fact]
    public async Task GetUpcomingExams_WithInvalidRequest_ShouldReturnBadRequest()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.NameIdentifier, studentId.ToString())
        }));

        _studentExamController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };

        _mediator.Setup(x => x.Send(It.IsAny<UpcominExamsQuery>(), default))
            .ReturnsAsync(OperationResult<List<ExamDto>>.Failure("Upcoming exams not found."));

        // Act
        var result = await _studentExamController.GetUpcomingExams();

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<ExamDto>>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal("Upcoming exams not found.", badRequestResult.Value);
    }
}