using App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;

public class CreatePatientExaminationCardTestModeHandlerTests : TestHelper
{
    private readonly CreatePatientExaminationCardTestModeHandler handler;
    private readonly CreatePatientExaminationCardTestModeCommand command;

    public CreatePatientExaminationCardTestModeHandlerTests()
    {
        var doctorId = Guid.NewGuid().ToString();
        var studentId = Guid.NewGuid().ToString();
        var APIResult = 1;
        var BleedingResult = 1;
        var BeweResult = 1;
        var DMFT_Result = 1;
        var DMFS_Result = 1;
        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();
        var dMFT_DMFSAssessmentModel = new DMFT_DMFSAssessmentModel();
        var beweAssessmentModel = new BeweAssessmentModel();
        var aPIAssessmentModel = new APIBleedingAssessmentModel();
        var bleedingAssessmentModel = new APIBleedingAssessmentModel();

        var createPatientExaminationCardTestModeInputParams = new CreatePatientExaminationCardTestModeInputParams(doctorId, studentId, APIResult, BleedingResult, BeweResult, DMFT_Result, DMFS_Result, riskFactorAssessmentModel, dMFT_DMFSAssessmentModel, beweAssessmentModel, aPIAssessmentModel, bleedingAssessmentModel);

        handler = new CreatePatientExaminationCardTestModeHandler(patientExaminationCardRepositoryMock.Object, patientRepositoryMock.Object, mapperMock.Object);
        command = new CreatePatientExaminationCardTestModeCommand(Guid.NewGuid(), createPatientExaminationCardTestModeInputParams);
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
        Assert.NotNull(result.ResultValue);
        Assert.IsType<PatientExaminationCardDto>(result.ResultValue);
    }
}
