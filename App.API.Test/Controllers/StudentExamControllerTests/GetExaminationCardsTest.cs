using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardsList;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class GetExaminationCardsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public GetExaminationCardsTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetExaminationCards_WhenCalled_ReturnsExaminationCards()
    {
        // Arrange
        var group1Id = Guid.NewGuid();
        var exam1 = new Exam(DateTime.Now, "Exam1", "Description1", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group1Id);
        var group2Id = Guid.NewGuid();
        var exam2 = new Exam(DateTime.Now, "Exam2", "Description2", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group2Id);

        var student1Id = Guid.NewGuid().ToString();
        var student2Id = Guid.NewGuid().ToString();

        var patientExaminationCard1 = new PracticePatientExaminationCard(exam1.Id, student1Id);
        var patientExaminationCard2 = new PracticePatientExaminationCard(exam1.Id, student2Id);

        var expectedExaminationCards = new List<PracticePatientExaminationCardDto>
        {
            new PracticePatientExaminationCardDto
            {
                Id = patientExaminationCard1.Id,

            },
            new PracticePatientExaminationCardDto
            {
                Id = patientExaminationCard2.Id,
            }
        };

        _mediator.Setup(m => m.Send(It.IsAny<FetchPracticePatientExaminationCardsByExamIdQuery>(), default))
            .ReturnsAsync(OperationResult<List<PracticePatientExaminationCardDto>>.Success(expectedExaminationCards));

        // Act
        var result = await _studentExamController.GetExaminationCards(exam1.Id);

        // Assert
        var okResult = Assert.IsType<ActionResult<List<PracticePatientExaminationCardDto>>>(result);
        var examinationCards = Assert.IsType<OkObjectResult>(okResult.Result);
        Assert.Equal(expectedExaminationCards, examinationCards.Value);
    }
}
