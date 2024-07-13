using App.Application.StudentExamOperations.StudentOperations.Command.AddPracticePatientExmaintionCard;
using App.Domain.DTOs.Common.Request;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.DTOs.PatientDtos.Request;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.CreditSchema;
using Moq;

namespace App.Application.Test.StudentExamOperations.StudentOperations.Command.AddPracticePatientExmaintionCard;

public class AddPracticePatientExaminationCardHandlerTests : TestHelper
{
    private readonly AddPracticePatientExaminationCardHandler handler;
    private readonly AddPracticePatientExaminationCardCommand command;

    public AddPracticePatientExaminationCardHandlerTests()
    {
        var patientDto = new CreatePatientDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Gender = "Male",
            EthnicGroup = "Group A",
            Location = "Location X",
            Age = 30,
            OtherGroup = "Group B",
            OtherData = "Data 1",
            OtherData2 = "Data 2",
            OtherData3 = "Data 3",
            YearsInSchool = 10
        };
        var riskFactorAssesmentModel = new RiskFactorAssessmentModel();
        var practiceBeweDto = new PracticeBeweDto();
        var practiceApiDto = new PracticeAPIDto
        {
            APIResult = 22,
        };
        var practiceApiBleedingDto = new PracticeBleedingDto
        {
            BleedingResult = 22,
        };
        var practiceDmftDmfsDto = new PracticeDMFT_DMFSDto();

        var summary = new SummaryRequestDto();

        handler = new AddPracticePatientExaminationCardHandler(studentExamRepositoryMock.Object);
        command = new AddPracticePatientExaminationCardCommand(Guid.NewGuid(), "studentId", new PracticePatientExaminationCardInputModel(
            patientDto,
            summary,
            riskFactorAssesmentModel,
            practiceApiDto,
            practiceApiBleedingDto,
            practiceBeweDto,
            practiceDmftDmfsDto
            ));
    }

    [Fact]
    public async Task Handle_WhenExamDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Given Exam doesn't Exists", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenStudentAlreadyTookExam_ShouldReturnFailureResult()
    {
        // Arrange
        var examId = Guid.NewGuid();
        var studentId = Guid.NewGuid();

        var exam = new Exam(DateTime.Now, "Title", "Description", new TimeOnly(10, 0), new TimeOnly(11, 0), TimeSpan.FromMinutes(60), 100, Guid.NewGuid());

        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(exam);

        studentExamRepositoryMock.Setup(x => x.CheckIfStudentHasAlreadyTakenTheExam(It.IsAny<Guid>(), "studentId"))
            .ReturnsAsync(true);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Exam Already Completed", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenValidRequest_ShouldReturnSuccessResult()
    {
        // Arrange
        var examId = Guid.NewGuid();
        var studentId = Guid.NewGuid();

        var exam = new Exam(DateTime.Now, "Title", "Description", new TimeOnly(10, 0), new TimeOnly(11, 0), TimeSpan.FromMinutes(60), 100, Guid.NewGuid());

        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(exam);

        studentExamRepositoryMock.Setup(x => x.CheckIfStudentHasAlreadyTakenTheExam(It.IsAny<Guid>(), "studentId"))
            .ReturnsAsync(false);

        studentExamRepositoryMock.Setup(x => x.AddPracticePatient(It.IsAny<PracticePatient>()))
            .Returns(Task.CompletedTask);

        studentExamRepositoryMock.Setup(x => x.AddPracticeAPI(It.IsAny<PracticeAPI>()))
            .Returns(Task.CompletedTask);

        studentExamRepositoryMock.Setup(x => x.AddPracticeBleeding(It.IsAny<PracticeBleeding>()))
            .Returns(Task.CompletedTask);

        studentExamRepositoryMock.Setup(x => x.AddPracticeDMFT_DMFS(It.IsAny<PracticeDMFT_DMFS>()))
            .Returns(Task.CompletedTask);

        studentExamRepositoryMock.Setup(x => x.AddPracticeBewe(It.IsAny<PracticeBewe>()))
            .Returns(Task.CompletedTask);

        studentExamRepositoryMock.Setup(x => x.AddPracticePatientExaminationCard(It.IsAny<PracticePatientExaminationCard>()))
            .Returns(Task.CompletedTask);

        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardById(It.IsAny<Guid>()))
            .ReturnsAsync(new PracticePatientExaminationCard(Guid.NewGuid(), "studentId"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
    }
}
