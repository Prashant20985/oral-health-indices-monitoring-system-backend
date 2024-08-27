using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Command.UpdateExam;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class UpdateExamTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public UpdateExamTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task UpdateExam_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var updateExamDto = new UpdateExamDto
        {
            DateOfExamination = DateTime.Now,
            ExamTitle = "Test Exam",
            Description = "This is a test exam.",
            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(10, 0),
            DurationInterval = new TimeSpan(0, 30, 0),
            MaxMark = 100
        };

        var examDto = new ExamDto
        {
            DateOfExamination = exam.DateOfExamination,
            ExamTitle = exam.ExamTitle,
            Description = exam.Description,
            StartTime = exam.StartTime,
            EndTime = exam.EndTime,
            DurationInterval = exam.DurationInterval,
            MaxMark = exam.MaxMark,
            ExamStatus = "Published"
        };

        _mediator.Setup(x => x.Send(It.IsAny<UpdateExamCommand>(), default))
            .ReturnsAsync(OperationResult<ExamDto>.Success(examDto));


        // Act
        var result = await _studentExamController.UpdateExam(exam.Id, updateExamDto);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ExamDto>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task UpdateExam_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var updateExamDto = new UpdateExamDto
        {
            DateOfExamination = DateTime.Now,
            ExamTitle = "Test Exam",
            Description = "This is a test exam.",
            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(10, 0),
            DurationInterval = new TimeSpan(0, 30, 0),
            MaxMark = 100
        };

        _mediator.Setup(x => x.Send(It.IsAny<UpdateExamCommand>(), default))
            .ReturnsAsync(OperationResult<ExamDto>.Failure("test"));

        // Act
        var result = await _studentExamController.UpdateExam(exam.Id, updateExamDto);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ExamDto>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task UpdateExam_WithInvalidExam_ShouldReturnBadRequest()
    {
        // Arrange
        var examId = Guid.NewGuid();
        var updateExamDto = new UpdateExamDto
        {
            DateOfExamination = DateTime.Now,
            ExamTitle = "Test Exam",
            Description = "This is a test exam.",
            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(10, 0),
            DurationInterval = new TimeSpan(0, 30, 0),
            MaxMark = 100
        };

        _mediator.Setup(x => x.Send(It.IsAny<UpdateExamCommand>(), default))
            .ReturnsAsync(OperationResult<ExamDto>.Failure("Exam not found"));

        // Act
        var result = await _studentExamController.UpdateExam(examId, updateExamDto);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ExamDto>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }
}