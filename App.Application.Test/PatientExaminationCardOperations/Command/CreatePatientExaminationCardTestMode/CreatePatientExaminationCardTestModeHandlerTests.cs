using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;
using App.Domain.DTOs.Common.Request;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Models.Users;
using AutoMapper;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;

public class CreatePatientExaminationCardTestModeHandlerTests : TestHelper
{
    private readonly CreatePatientExaminationCardTestModeHandler handler;
    private readonly CreatePatientExaminationCardTestModeCommand command;
    private ApplicationUser ApplicationUserDoctor;
    private ApplicationUser ApplicationUserStudent;

    public CreatePatientExaminationCardTestModeHandlerTests()
    {
        MapperConfiguration mapperConfig = new(cfg => cfg.AddProfile<MappingProfile>());
        IMapper mapper = mapperConfig.CreateMapper();

        var applicationRoleDoctor = new ApplicationRole { Name = "Dentist_Teacher_Examiner" };
        var applicationRoleStudent = new ApplicationRole { Name = "Student" };

        ApplicationUserDoctor = new ApplicationUser("test@test.com", "Jhon", "Doe", "741852963", null)
        {
            ApplicationUserRoles = [new ApplicationUserRole { ApplicationRole = applicationRoleDoctor }]
        };

        ApplicationUserStudent = new ApplicationUser("test2@test.com", "Jhon", "Doe", "741852963", null)
        {
            ApplicationUserRoles = [new ApplicationUserRole { ApplicationRole = applicationRoleStudent }]
        };

        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();
        var createDMFT_DMFSRequest = new CreateDMFT_DMFSTestModeRequestDto
        {
            StudentComment = "test",
            DMFSResult = 10M,
            DMFTResult = 10M,
            DMFT_DMFSAssessmentModel = new DMFT_DMFSAssessmentModel(),
        };
        var createBeweRequest = new CreateBeweTestModeRequestDto
        {
            StudentComment = "test",
            BeweResult = 10M,
            BeweAssessmentModel = new BeweAssessmentModel(),
        };
        var createAPIRequest = new CreateAPITestModeRequestDto
        {
            StudentComment = "test",
            APIResult = 10,
            Maxilla = 10,
            Mandible = 10,
            APIAssessmentModel = new APIBleedingAssessmentModel(),
        };
        var createBleedingRequest = new CreateBleedingTestModeRequestDto
        {
            StudentComment = "test",
            BleedingResult = 10,
            Maxilla = 10,
            Mandible = 10,
            BleedingAssessmentModel = new APIBleedingAssessmentModel(),
        };

        var createPatientExaminationCardTestModeInputParams = 
            new CreatePatientExaminationCardTestModeInputParams(
                ApplicationUserDoctor.Id,
                "Test Comment",
                riskFactorAssessmentModel,
                createDMFT_DMFSRequest,
                createBeweRequest,
                createAPIRequest,
                createBleedingRequest);

        handler = new CreatePatientExaminationCardTestModeHandler(
            patientExaminationCardRepositoryMock.Object,
            patientRepositoryMock.Object,
            userRepositoryMock.Object,
            mapper);

        command = new CreatePatientExaminationCardTestModeCommand(
            Guid.NewGuid(),
            ApplicationUserStudent.Id,
            createPatientExaminationCardTestModeInputParams);
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

        userRepositoryMock.Setup(x => x.GetApplicationUserWithRolesById(ApplicationUserStudent.Id))
            .ReturnsAsync(ApplicationUserStudent);

        userRepositoryMock.Setup(x => x.GetApplicationUserWithRolesById(ApplicationUserDoctor.Id))
            .ReturnsAsync(ApplicationUserDoctor);

        patientExaminationCardRepositoryMock.Setup(x => x.AddBewe(It.IsAny<Bewe>()));
        patientExaminationCardRepositoryMock.Setup(x => x.AddAPI(It.IsAny<API>()));
        patientExaminationCardRepositoryMock.Setup(x => x.AddBleeding(It.IsAny<Bleeding>()));
        patientExaminationCardRepositoryMock.Setup(x => x.AddDMFT_DMFS(It.IsAny<DMFT_DMFS>()));
        patientExaminationCardRepositoryMock.Setup(x => x.AddPatientExaminationResult(It.IsAny<PatientExaminationResult>()));
        patientExaminationCardRepositoryMock.Setup(x => x.AddRiskFactorAssessment(It.IsAny<RiskFactorAssessment>()));
        patientExaminationCardRepositoryMock.Setup(x => x.AddPatientExaminationCard(It.IsAny<PatientExaminationCard>()));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.IsType<PatientExaminationCardDto>(result.ResultValue);
    }
}
