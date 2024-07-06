using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.UpdateBeweForm;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.UpdateBeweForm;

public class UpdateBeweFormHandlerTests : TestHelper
{
    private readonly UpdateBeweFormHandler handler;
    private readonly UpdateBeweFormCommand command;

    public UpdateBeweFormHandlerTests()
    {
        var beweAssesmentModel = new BeweAssessmentModel
        {
            Sectant1 = new Sectant1
            {
                Tooth_17 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_16 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_15 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_14 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
            },
            Sectant2 = new Sectant2
            {
                Tooth_13 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_12 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_11 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_21 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_22 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_23 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
            },
            Sectant3 = new Sectant3
            {
                Tooth_24 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_25 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_26 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_27 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
            },
            Sectant4 = new Sectant4
            {
                Tooth_34 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_35 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_36 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_37 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
            },
            Sectant5 = new Sectant5
            {
                Tooth_43 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_42 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_41 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_31 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_32 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
                Tooth_33 = new FourSurfaceTooth { B = "2", L = "3", D = "3", M = "3" },
            },
            Sectant6 = new Sectant6
            {
                Tooth_47 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_46 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_45 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
                Tooth_44 = new FiveSurfaceToothBEWE { O = "1", B = "2", L = "3", D = "3", M = "3" },
            }
        };

        handler = new UpdateBeweFormHandler(patientExaminationCardRepositoryMock.Object);
        command = new UpdateBeweFormCommand(Guid.NewGuid(), beweAssesmentModel);
    }

    [Fact]
    public async Task Handle_WhenBeweFormDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetBeweByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Bewe form not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenBeweFormExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var patienExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var bewe = new Bewe();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        patienExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        patientExaminationCardRepositoryMock.Setup(x => x.GetBeweByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(bewe);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.IsType<OperationResult<decimal>>(result);
        Assert.Equal(bewe.AssessmentModel, command.AssessmentModel);
    }
}
