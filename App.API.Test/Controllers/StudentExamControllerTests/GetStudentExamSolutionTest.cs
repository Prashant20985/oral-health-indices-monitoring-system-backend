using System.Security.Claims;
using App.Application.Core;
using App.Application.StudentExamOperations.StudentOperations.Query;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class GetStudentExamSolutionTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public GetStudentExamSolutionTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetStudentExamSolution_WithValidRequest_ShouldReturnStudentExamSolution()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var practicepatientExaminationCardDto = new PracticePatientExaminationCardDto
        {
            Id = Guid.NewGuid(),
            StudentMark = 10,
            DoctorComment = "comment",
            DoctorName = "doctor",
            StudentName = "student"
        };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.NameIdentifier, studentId.ToString())
        }));

        _studentExamController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchStudentExamSolutionQuery>(), default))
            .ReturnsAsync(
                OperationResult<PracticePatientExaminationCardDto>.Success(practicepatientExaminationCardDto));


        // Act
        var result = await _studentExamController.GetStudentExamSolution(exam.Id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PracticePatientExaminationCardDto>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedSolution = Assert.IsType<PracticePatientExaminationCardDto>(okResult.Value);
        Assert.Equal(practicepatientExaminationCardDto, returnedSolution);
    }

    [Fact]
    public async Task GetStudentExamSolution_WithInvalidRequest_ShouldReturnBadRequest()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.NameIdentifier, studentId.ToString())
        }));

        _studentExamController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchStudentExamSolutionQuery>(), default))
            .ReturnsAsync(OperationResult<PracticePatientExaminationCardDto>.Failure("Examination card not found"));

        // Act
        var result = await _studentExamController.GetStudentExamSolution(exam.Id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PracticePatientExaminationCardDto>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal("Examination card not found", badRequestResult.Value);
    }
}