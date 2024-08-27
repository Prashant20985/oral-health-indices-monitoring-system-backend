using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCards;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class GetPatientExaminationCardsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public GetPatientExaminationCardsTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetPatientExaminationCards_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);
        var patientRxaminationCard2 = new PatientExaminationCard(patientId);

        var patientExaminationCardDtos = new List<PatientExaminationCardDto>
        {
            new() { Id = patientExaminationCard.Id },
            new() { Id = patientRxaminationCard2.Id }
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchPatientExaminationCardsQuery>(), default))
            .ReturnsAsync(OperationResult<List<PatientExaminationCardDto>>.Success(patientExaminationCardDtos));

        // Act
        var result = await _patientExaminationCardController.GetPatientExaminationCards(patientId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<PatientExaminationCardDto>>(okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetPatientExaminationCards_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        _mediator.Setup(x => x.Send(It.IsAny<FetchPatientExaminationCardsQuery>(), default))
            .ReturnsAsync(OperationResult<List<PatientExaminationCardDto>>.Failure("Patient Not Found"));

        // Act
        var result = await _patientExaminationCardController.GetPatientExaminationCards(Guid.NewGuid());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}