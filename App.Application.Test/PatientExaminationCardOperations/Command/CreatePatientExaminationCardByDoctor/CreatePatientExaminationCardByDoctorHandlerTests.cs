using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByDoctor;
using App.Domain.DTOs.Common.Request;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.Common.Tooth;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Models.Users;
using AutoMapper;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByDoctor;

public class CreatePatientExaminationCardByDoctorHandlerTests : TestHelper
{
    private readonly CreatePatientExaminationCardByDoctorCommand command;
    private readonly CreatePatientExaminationCardByDoctorHandler handler;

    public CreatePatientExaminationCardByDoctorHandlerTests()
    {
        MapperConfiguration mapperConfiguration = new(cfg => cfg.AddProfile<MappingProfile>());
        IMapper mapper = mapperConfiguration.CreateMapper();

        var riskFactorAssesmentModel = new RiskFactorAssessmentModel();

        CreateDMFT_DMFSRequestDto dmft_dmfs = new()
        {
            Comment = "Comment",
            DMFSResult = 10,
            DMFTResult = 10,
            DMFT_DMFSAssessmentModel = new DMFT_DMFSAssessmentModel()
        };

        CreateBeweRequestDto bewe = new()
        {
            Comment = "test",
            BeweAssessmentModel = new BeweAssessmentModel
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
            }
        };

        CreateAPIRequestDto api = new()
        {
            Comment = "test",
            APIAssessmentModel = new APIBleedingAssessmentModel

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
            }
        };

        CreateBleedingRequestDto bleeding = new()
        {
            Comment = "test",
            BleedingAssessmentModel = new APIBleedingAssessmentModel
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
            }
        };

        CreatePatientExaminationCardByDoctorInputParams inputParams = new(
            "Test Comment",
            riskFactorAssesmentModel,
            dmft_dmfs,
            bewe,
            api,
            bleeding);

        command = new CreatePatientExaminationCardByDoctorCommand(Guid.NewGuid(), "doctorId", inputParams);
        
        handler = new CreatePatientExaminationCardByDoctorHandler(
            patientExaminationCardRepositoryMock.Object,
            patientRepositoryMock.Object,
            userRepositoryMock.Object,
            mapper);
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
        var applicationRoleDoctor = new ApplicationRole { Name = "Dentist_Teacher_Researcher" };
        var applicationUserDoctor = new ApplicationUser("test2@test.com", "Jhon", "Doe", "741852863", null)
        {
            ApplicationUserRoles = [new ApplicationUserRole { ApplicationRole = applicationRoleDoctor }]
        };

        patientRepositoryMock.Setup(x => x.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(patient);

        userRepositoryMock.Setup(x => x.GetApplicationUserWithRolesById(It.IsAny<string>()))
            .ReturnsAsync(applicationUserDoctor);

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
        Assert.IsType<PatientExaminationCardDto>(result.ResultValue);
    }
}
