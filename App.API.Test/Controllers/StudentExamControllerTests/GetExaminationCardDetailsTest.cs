using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardDetails;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class GetExaminationCardDetailsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public GetExaminationCardDetailsTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetExaminationCardDetails_WhenCalled_ReturnsExaminationCardDetails()
    {
        // Arrange
        var group1Id = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "Exam1", "Description1", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group1Id);

        var student1Id = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, student1Id);


        var expectedExaminationCardDetails = new PracticePatientExaminationCardDto
        {
            Id = practicePatientExaminationCard.Id,

        };

        _mediator.Setup(m => m.Send(It.IsAny<FetchPracticePatientExaminationCardDetailsQuery>(), default))
            .ReturnsAsync(OperationResult<PracticePatientExaminationCardDto>.Success(expectedExaminationCardDetails));

        // Act
        var result = await _studentExamController.GetExaminationCardDetails(practicePatientExaminationCard.Id);

        // Assert
        var okResult = Assert.IsType<ActionResult<PracticePatientExaminationCardDto>>(result);
        var examinationCardDetails = Assert.IsType<OkObjectResult>(okResult.Result);
        Assert.Equal(expectedExaminationCardDetails, examinationCardDetails.Value);
    }

    [Fact]
    public async Task GetExaminationCardDetails_WhenCalledWithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        var examinationCardId = Guid.NewGuid();

        _mediator.Setup(m => m.Send(It.IsAny<FetchPracticePatientExaminationCardDetailsQuery>(), default))
            .ReturnsAsync(OperationResult<PracticePatientExaminationCardDto>.Failure("Examination card not found."));

        // Act
        var result = await _studentExamController.GetExaminationCardDetails(examinationCardId);

        // Assert
        var badRequestResult = Assert.IsType<ActionResult<PracticePatientExaminationCardDto>>(result);
        var error = Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
        Assert.Equal("Examination card not found.", error.Value);
    }

}
