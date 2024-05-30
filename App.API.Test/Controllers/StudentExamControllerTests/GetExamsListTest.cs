using App.API.Controllers;
using App.Application.Core;
using App.Application.StudentExamOperations.CommonOperations.Query.ExamsList;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class GetExamsListTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public GetExamsListTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetExamsList_WhenCalled_ReturnsExamsList()
    {
        // Arrange
        var group1Id = Guid.NewGuid();
        var group2Id = Guid.NewGuid();

        var exam1 = new Exam(DateTime.Now, "Exam1", "Description1", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group1Id);
        var exam2 = new Exam(DateTime.Now, "Exam2", "Description2", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group1Id);

        var expectedExamsList = new List<ExamDto>
        {
            new ExamDto
            {
               Id = exam1.Id,
               DateOfExamination = exam1.DateOfExamination,
               ExamTitle = exam1.ExamTitle,
               Description = exam1.Description,
               PublishDate = exam1.PublishDate,
               StartTime = exam1.StartTime,
               EndTime = exam1.EndTime,
               DurationInterval = exam1.DurationInterval,
               MaxMark = exam1.MaxMark,
               ExamStatus = exam1.ExamStatus.ToString()
            },
            new ExamDto
            {
                Id = exam2.Id,
                DateOfExamination = exam2.DateOfExamination,
                ExamTitle = exam2.ExamTitle,
                Description = exam2.Description,
                PublishDate = exam2.PublishDate,
                StartTime = exam2.StartTime,
                EndTime = exam2.EndTime,
                DurationInterval = exam2.DurationInterval,
                MaxMark = exam2.MaxMark,
                ExamStatus = exam2.ExamStatus.ToString()
            }
        };

        _mediator.Setup(m => m.Send(It.IsAny<FetchExamsListByGroupIdQuery>(), default))
            .ReturnsAsync(OperationResult<List<ExamDto>>.Success(expectedExamsList));

        // Act
        var result = await _studentExamController.GetExamsList(group1Id);

        // Assert
        var okResult = Assert.IsType<ActionResult<List<ExamDto>>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var examsList = Assert.IsAssignableFrom<List<ExamDto>>(okObjectResult.Value);
        Assert.Equal(expectedExamsList.Count, examsList.Count);
    }

    [Fact]
    public async Task GetExamsList_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var group1Id = Guid.NewGuid();

        _mediator.Setup(m => m.Send(It.IsAny<FetchExamsListByGroupIdQuery>(), default))
            .ReturnsAsync(OperationResult<List<ExamDto>>.Failure("Invalid data"));

        // Act
        var result = await _studentExamController.GetExamsList(group1Id);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Invalid data", badRequestResult.Value);
    }
}
