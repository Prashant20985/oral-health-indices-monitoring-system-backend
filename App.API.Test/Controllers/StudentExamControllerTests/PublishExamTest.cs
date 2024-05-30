using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Command.PublishExam;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class PublishExamTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public PublishExamTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task PublishExam_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var publishExamDto = new PublishExamDto
        {
            DateOfExamination = DateTime.Now,
            ExamTitle = "Test Exam",
            Description = "This is a test exam.",
            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(10, 0),
            DurationInterval = new TimeSpan(0, 30, 0),
            MaxMark = 100,
            GroupId = Guid.NewGuid()
        };

        var examDto = new ExamDto
        {
            DateOfExamination = publishExamDto.DateOfExamination,
            ExamTitle = publishExamDto.ExamTitle,
            Description = publishExamDto.Description,
            StartTime = publishExamDto.StartTime,
            EndTime = publishExamDto.EndTime,
            DurationInterval = publishExamDto.DurationInterval,
            MaxMark = publishExamDto.MaxMark,
            ExamStatus = "Published"
        };

        _mediator.Setup(x => x.Send(It.IsAny<PublishExamCommand>(), default))
            .ReturnsAsync(OperationResult<ExamDto>.Success(examDto));

        // Act
        var result = await _studentExamController.PublishExam(publishExamDto);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ExamDto>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedExamDto = Assert.IsType<ExamDto>(okResult.Value);
        Assert.Equal(examDto.DateOfExamination, returnedExamDto.DateOfExamination);
        Assert.Equal(examDto.ExamTitle, returnedExamDto.ExamTitle);
        Assert.Equal(examDto.Description, returnedExamDto.Description);
        Assert.Equal(examDto.StartTime, returnedExamDto.StartTime);
        Assert.Equal(examDto.EndTime, returnedExamDto.EndTime);
        Assert.Equal(examDto.DurationInterval, returnedExamDto.DurationInterval);
        Assert.Equal(examDto.MaxMark, returnedExamDto.MaxMark);
        Assert.Equal(examDto.ExamStatus, returnedExamDto.ExamStatus);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task PublishExam_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var publishExamDto = new PublishExamDto
        {
            DateOfExamination = DateTime.Now,
            ExamTitle = "Test Exam",
            Description = "This is a test exam.",
            StartTime = new TimeOnly(8, 0),
            EndTime = new TimeOnly(10, 0),
            DurationInterval = new TimeSpan(0, 30, 0),
            MaxMark = 100,
            GroupId = Guid.NewGuid()
        };

        _mediator.Setup(x => x.Send(It.IsAny<PublishExamCommand>(), default))
            .ReturnsAsync(OperationResult<ExamDto>.Failure("Invalid data."));

        // Act
        var result = await _studentExamController.PublishExam(publishExamDto);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ExamDto>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}
