using App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.Common.Tooth;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;

public class CreatePatientExaminationCardRegularModeHandlerTests : TestHelper
{
    private readonly CreatePatientExaminationCardRegularModeHandler handler;
    private readonly CreatePatientExaminationCardRegularModeCommand command;

    public CreatePatientExaminationCardRegularModeHandlerTests()
    {
        var riskFactorAssesmentModel = new RiskFactorAssessmentModel();
        var dmft_dmfsAssesmentModel = new DMFT_DMFSAssessmentModel();
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

        var apiAssesmentModel = new APIBleedingAssessmentModel
        {
            Quadrant1 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "-",
                Value4 = "+",
                Value5 = "+",
                Value6 = "-",
                Value7 = "+",
            },
            Quadrant2 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "-",
                Value4 = "+",
                Value5 = "+",
                Value6 = "-",
                Value7 = "+",
            },
            Quadrant3 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "-",
                Value4 = "+",
                Value5 = "+",
                Value6 = "-",
                Value7 = "+",
            },
            Quadrant4 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "-",
                Value4 = "+",
                Value5 = "+",
                Value6 = "-",
                Value7 = "+",
            }
        };

        var bleedingAssesmentModel = new APIBleedingAssessmentModel
        {
            Quadrant1 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "+",
                Value4 = "-",
                Value5 = "+",
                Value6 = "+",
                Value7 = "-",
            },
            Quadrant2 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "+",
                Value4 = "-",
                Value5 = "+",
                Value6 = "+",
                Value7 = "-",
            },
            Quadrant3 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "+",
                Value4 = "-",
                Value5 = "+",
                Value6 = "+",
                Value7 = "-",
            },
            Quadrant4 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "+",
                Value4 = "-",
                Value5 = "+",
                Value6 = "+",
                Value7 = "-",
            }
        };

        var createPatientExaminationCardRegularModeInputParams = new CreatePatientExaminationCardRegularModeInputParams("doctorId", riskFactorAssesmentModel, dmft_dmfsAssesmentModel, beweAssesmentModel, apiAssesmentModel, bleedingAssesmentModel);

        handler = new CreatePatientExaminationCardRegularModeHandler(patientExaminationCardRepositoryMock.Object, patientRepositoryMock.Object, mapperMock.Object);
        command = new CreatePatientExaminationCardRegularModeCommand(Guid.NewGuid(), createPatientExaminationCardRegularModeInputParams);
    }

    [Fact]
    public async Task Handle_WhenPatientDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        patientRepositoryMock.Setup(x => x.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Patient not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenPatientExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var patient = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 18, "test", "test", "test", "test", 1, "doctorId");

        patientRepositoryMock.Setup(x => x.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(patient);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.IsType<PatientExaminationCardDto>(result.ResultValue);
    }
}
