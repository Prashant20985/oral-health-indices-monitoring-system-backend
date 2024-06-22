using App.Application.Core;
using App.Application.StudentExamOperations.StudentOperations.Command.AddPracticePatientExmaintionCard;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.DTOs.PatientDtos.Request;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class AddPracticePatientExaminationCardTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public AddPracticePatientExaminationCardTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task AddPracticePatientExaminationCard_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var group1Id = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "Exam1", "Description1", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group1Id);

        var student1Id = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, student1Id);

        var patientDto = new CreatePatientDto()
        {
            FirstName = "Patient1",
            LastName = "Patient1",
            Email = "Patient@email.com",
            Gender = "Male",
            EthnicGroup = "EthnicGroup1",
            OtherGroup = "OtherGroup1",
            YearsInSchool = 10,
            OtherData = "OtherData1",
            OtherData2 = "OtherData2",
            OtherData3 = "OtherData3",
            Location = "Location1",
            Age = 19
        };

        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();

        var PracticeAPI = new PracticeAPIDto
        {
            APIResult = 22,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBleeding = new PracticeBleedingDto
        {
            BleedingResult = 33,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBewe = new PracticeBeweDto
        {
            BeweResult = 44,
            AssessmentModel = new BeweAssessmentModel()
        };

        var PracticeDMFT_DMFS = new PracticeDMFT_DMFSDto
        {
            DMFTResult = 55,
            DMFSResult = 66,
            AssessmentModel = new DMFT_DMFSAssessmentModel()
        };

        var cardInputModel = new PracticePatientExaminationCardInputModel
            (patientDto,
            riskFactorAssessmentModel,
            PracticeAPI,
            PracticeBleeding,
            PracticeBewe,
            PracticeDMFT_DMFS);

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, student1Id),
            new Claim(ClaimTypes.Role, "Student")
        }));

        _studentExamController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = claimsPrincipal }
        };

        _mediator.Setup(x => x.Send(It.IsAny<AddPracticePatientExaminationCardCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _studentExamController.AddPracticePatientExaminationCard(exam.Id, cardInputModel);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
    }

    [Fact]
    public async Task AddPracticePatientExaminationCard_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var group1Id = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "Exam1", "Description1", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group1Id);

        var student1Id = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, student1Id);

        var patientDto = new CreatePatientDto()
        {
            FirstName = "Patient1",
            LastName = "Patient1",
            Email = "Patient@email.com",
            Gender = "Male",
            EthnicGroup = "EthnicGroup1",
            OtherGroup = "OtherGroup1",
            YearsInSchool = 10,
            OtherData = "OtherData1",
            OtherData2 = "OtherData2",
            OtherData3 = "OtherData3",
            Location = "Location1",
            Age = 19
        };

        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();

        var PracticeAPI = new PracticeAPIDto
        {
            APIResult = 22,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBleeding = new PracticeBleedingDto
        {
            BleedingResult = 33,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBewe = new PracticeBeweDto
        {
            BeweResult = 44,
            AssessmentModel = new BeweAssessmentModel()
        };

        var PracticeDMFT_DMFS = new PracticeDMFT_DMFSDto
        {
            DMFTResult = 55,
            DMFSResult = 66,
            AssessmentModel = new DMFT_DMFSAssessmentModel()
        };

        var cardInputModel = new PracticePatientExaminationCardInputModel
            (patientDto,
            riskFactorAssessmentModel,
            PracticeAPI,
            PracticeBleeding,
            PracticeBewe,
            PracticeDMFT_DMFS);


        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, student1Id),
            new Claim(ClaimTypes.Role, "Student")
        }));

        _studentExamController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = claimsPrincipal }
        };

        _mediator.Setup(x => x.Send(It.IsAny<AddPracticePatientExaminationCardCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<Unit>.Failure("Error"));

        // Act
        var result = await _studentExamController.AddPracticePatientExaminationCard(exam.Id, cardInputModel);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
    }

    [Fact]
    public async Task AddPracticePatientExaminationCard_WithInvalidRole_ShouldReturnNotfound()
    {
        // Arrange
        var group1Id = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "Exam1", "Description1", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group1Id);

        var student1Id = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, student1Id);

        var patientDto = new CreatePatientDto()
        {
            FirstName = "Patient1",
            LastName = "Patient1",
            Email = "Patient@email.com",
            Gender = "Male",
            EthnicGroup = "EthnicGroup1",
            OtherGroup = "OtherGroup1",
            YearsInSchool = 10,
            OtherData = "OtherData1",
            OtherData2 = "OtherData2",
            OtherData3 = "OtherData3",
            Location = "Location1",
            Age = 19
        };

        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();

        var PracticeAPI = new PracticeAPIDto
        {
            APIResult = 22,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBleeding = new PracticeBleedingDto
        {
            BleedingResult = 33,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBewe = new PracticeBeweDto
        {
            BeweResult = 44,
            AssessmentModel = new BeweAssessmentModel()
        };

        var PracticeDMFT_DMFS = new PracticeDMFT_DMFSDto
        {
            DMFTResult = 55,
            DMFSResult = 66,
            AssessmentModel = new DMFT_DMFSAssessmentModel()
        };

        var cardInputModel = new PracticePatientExaminationCardInputModel
            (patientDto,
            riskFactorAssessmentModel,
            PracticeAPI,
            PracticeBleeding,
            PracticeBewe,
            PracticeDMFT_DMFS);


        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, student1Id),
            new Claim(ClaimTypes.Role, "Dentist_Teacher_Examiner")
        }));

        _studentExamController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = claimsPrincipal }
        };

        // Act
        var result = await _studentExamController.AddPracticePatientExaminationCard(exam.Id, cardInputModel);

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, actionResult.StatusCode);
    }

    [Fact]
    public async Task AddPracticePatientExaminationCard_WithInvalidExamId_ShouldReturnError()
    {
        // Arrange
        var group1Id = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "Exam1", "Description1", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group1Id);

        var student1Id = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, student1Id);

        var patientDto = new CreatePatientDto()
        {
            FirstName = "Patient1",
            LastName = "Patient1",
            Email = "Patient@email.com",
            Gender = "Male",
            EthnicGroup = "EthnicGroup1",
            OtherGroup = "OtherGroup1",
            YearsInSchool = 10,
            OtherData = "OtherData1",
            OtherData2 = "OtherData2",
            OtherData3 = "OtherData3",
            Location = "Location1",
            Age = 19
        };

        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();

        var PracticeAPI = new PracticeAPIDto
        {
            APIResult = 22,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBleeding = new PracticeBleedingDto
        {
            BleedingResult = 33,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBewe = new PracticeBeweDto
        {
            BeweResult = 44,
            AssessmentModel = new BeweAssessmentModel()
        };

        var PracticeDMFT_DMFS = new PracticeDMFT_DMFSDto
        {
            DMFTResult = 55,
            DMFSResult = 66,
            AssessmentModel = new DMFT_DMFSAssessmentModel()
        };

        var cardInputModel = new PracticePatientExaminationCardInputModel
            (patientDto,
            riskFactorAssessmentModel,
            PracticeAPI,
            PracticeBleeding,
            PracticeBewe,
            PracticeDMFT_DMFS);


        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, student1Id),
            new Claim(ClaimTypes.Role, "Student")
        }));

        _studentExamController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = claimsPrincipal }
        };

        _mediator.Setup(x => x.Send(It.IsAny<AddPracticePatientExaminationCardCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<Unit>.Failure("Exam Not Found"));

        // Act
        var result = await _studentExamController.AddPracticePatientExaminationCard(Guid.NewGuid(), cardInputModel);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
    }

    [Fact]
    public async Task AddPracticePatientExaminationCard_WithExamCompleted_ShouldReturnError()
    {
        // Arrange
        var group1Id = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "Exam1", "Description1", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group1Id);

        var student1Id = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, student1Id);

        exam.MarksAsGraded();

        var patientDto = new CreatePatientDto()
        {
            FirstName = "Patient1",
            LastName = "Patient1",
            Email = "Patient@email.com",
            Gender = "Male",
            EthnicGroup = "EthnicGroup1",
            OtherGroup = "OtherGroup1",
            YearsInSchool = 10,
            OtherData = "OtherData1",
            OtherData2 = "OtherData2",
            OtherData3 = "OtherData3",
            Location = "Location1",
            Age = 19
        };

        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();

        var PracticeAPI = new PracticeAPIDto
        {
            APIResult = 22,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBleeding = new PracticeBleedingDto
        {
            BleedingResult = 33,
            AssessmentModel = new APIBleedingAssessmentModel()
        };

        var PracticeBewe = new PracticeBeweDto
        {
            BeweResult = 44,
            AssessmentModel = new BeweAssessmentModel()
        };

        var PracticeDMFT_DMFS = new PracticeDMFT_DMFSDto
        {
            DMFTResult = 55,
            DMFSResult = 66,
            AssessmentModel = new DMFT_DMFSAssessmentModel()
        };

        var cardInputModel = new PracticePatientExaminationCardInputModel
            (patientDto,
            riskFactorAssessmentModel,
            PracticeAPI,
            PracticeBleeding,
            PracticeBewe,
            PracticeDMFT_DMFS);


        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, student1Id),
            new Claim(ClaimTypes.Role, "Student")
        }));

        _studentExamController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = claimsPrincipal }
        };

        _mediator.Setup(x => x.Send(It.IsAny<AddPracticePatientExaminationCardCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<Unit>.Failure("Exam Already Completed"));

        // Act
        var result = await _studentExamController.AddPracticePatientExaminationCard(exam.Id, cardInputModel);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);
    }
}
