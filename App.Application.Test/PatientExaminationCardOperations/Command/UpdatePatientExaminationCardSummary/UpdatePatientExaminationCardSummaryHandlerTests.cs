using App.Application.PatientExaminationCardOperations.Command.UpdatePatientExaminationCardSummary;
using App.Domain.DTOs.Common.Request;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.UpdatePatientExaminationCardSummary;

public class UpdatePatientExaminationCardSummaryHandlerTests : TestHelper
{
    private readonly UpdatePatientExaminationCardSummaryCommand command;
    private readonly UpdatePatientExaminationCardSummaryHandler handler;

    public UpdatePatientExaminationCardSummaryHandlerTests()
    {
        command = new UpdatePatientExaminationCardSummaryCommand(Guid.NewGuid(), new SummaryRequestDto
        {
            Description = "Description",
            NeedForDentalInterventions = "NeedForDentalInterventions",
            PatientRecommendations = "PatientRecommendations",
            ProposedTreatment = "ProposedTreatment"
        });
        handler = new UpdatePatientExaminationCardSummaryHandler(patientExaminationCardRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenPatientExaminationCardDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetPatientExaminationCard(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Patient examination card not found.", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenPatientExaminationCardExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());

        patientExaminationCardRepositoryMock.Setup(x => x.GetPatientExaminationCard(It.IsAny<Guid>()))
            .ReturnsAsync(patientExaminationCard);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Equal(command.Summary.Description, patientExaminationCard.Description);
        Assert.Equal(command.Summary.NeedForDentalInterventions, patientExaminationCard.NeedForDentalInterventions);
        Assert.Equal(command.Summary.PatientRecommendations, patientExaminationCard.PatientRecommendations);
        Assert.Equal(command.Summary.ProposedTreatment, patientExaminationCard.ProposedTreatment);
    }
}