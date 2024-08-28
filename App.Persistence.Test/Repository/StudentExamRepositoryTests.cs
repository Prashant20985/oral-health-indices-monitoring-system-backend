using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.Enums;
using App.Domain.Models.Users;
using App.Persistence.Contexts;
using App.Persistence.Repository;
using AutoMapper;
using MockQueryable.Moq;
using Moq;

namespace App.Persistence.Test.Repository;

public class StudentExamRepositoryTests
{
    private readonly Mock<OralEhrContext> _mockOralEhrContext;
    private readonly StudentExamRepository _studentExamRepository;

    public StudentExamRepositoryTests()
    {
        _mockOralEhrContext = new Mock<OralEhrContext>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Exam, ExamDto>()
                .ForMember(x => x.ExamStatus, o => o.MapFrom(s => Enum.GetName(s.ExamStatus)));
            cfg.CreateMap<PracticePatient, PatientResponseDto>();
            cfg.CreateMap<PracticeRiskFactorAssessment, RiskFactorAssessmentDto>();
            cfg.CreateMap<PracticeBewe, PracticeBeweResponseDto>();
            cfg.CreateMap<PracticeDMFT_DMFS, PracticeDMFT_DMFSRespnseDto>();
            cfg.CreateMap<PracticeAPI, PracticeAPIResponseDto>();
            cfg.CreateMap<PracticeBleeding, PracticeBleedingResponseDto>();
            cfg.CreateMap<PracticePatientExaminationResult, PracticePatientExaminationResultResponseDto>();
            cfg.CreateMap<PracticePatientExaminationCard, PracticePatientExaminationCardDto>();
        });
        var mapper = mapperConfig.CreateMapper();
        _studentExamRepository = new StudentExamRepository(_mockOralEhrContext.Object, mapper);
    }

    [Fact]
    public async Task GetPracticePatientExaminationCardbyId_ShouldReturnPracticePatientExaminationCard()
    {
        // Arrange
        var examId1 = Guid.NewGuid();
        var examId2 = Guid.NewGuid();

        var studentId1 = "studentId";
        var studentId2 = "studentId2";

        var practicePatientExaminationCard1 = new PracticePatientExaminationCard(examId1, studentId1);
        var practicePatientExaminationCard2 = new PracticePatientExaminationCard(examId2, studentId2);

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard>
            {
                practicePatientExaminationCard1,
                practicePatientExaminationCard2
            }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result =
            await _studentExamRepository.GetPracticePatientExaminationCardById(practicePatientExaminationCard1.Id);

        // Assert
        Assert.Equal(practicePatientExaminationCard1, result);
        Assert.Equal(studentId1, result.StudentId);
        Assert.Equal(examId1, result.ExamId);
        Assert.NotEqual(examId2, result.ExamId);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetPracticePatientExaminationCardbyId_ShouldReturnNull()
    {
        // Arrange
        var examId1 = Guid.NewGuid();
        var examId2 = Guid.NewGuid();

        var studentId1 = "studentId";
        var studentId2 = "studentId2";

        var practicePatientExaminationCard1 = new PracticePatientExaminationCard(examId1, studentId1);
        var practicePatientExaminationCard2 = new PracticePatientExaminationCard(examId2, studentId2);

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard>
            {
                practicePatientExaminationCard1,
                practicePatientExaminationCard2
            }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result = await _studentExamRepository.GetPracticePatientExaminationCardById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetExamById_ShouldReturnExam()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupId2 = Guid.NewGuid();

        var exam1 = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var exam2 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId2);

        var exams = new List<Exam>
            {
                exam1,
                exam2
            }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.Exams.FindAsync(exam1.Id))
            .ReturnsAsync(exam1);
        _mockOralEhrContext.Setup(x => x.Exams.FindAsync(exam2.Id))
            .ReturnsAsync(exam2);

        // Act
        var result = await _studentExamRepository.GetExamById(exam1.Id);

        // Assert
        Assert.Equal(exam1, result);
        Assert.Equal(exam1.Id, result.Id);
        Assert.Equal(exam1.GroupId, result.GroupId);
        Assert.NotEqual(exam2.Id, result.Id);
        Assert.NotEqual(exam2.GroupId, result.GroupId);
        Assert.Equal(groupId, result.GroupId);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetExamById_ShouldReturnNull()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupId2 = Guid.NewGuid();

        var exam1 = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var exam2 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId2);

        var exams = new List<Exam>
            {
                exam1,
                exam2
            }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.Exams.FindAsync(exam1.Id))
            .ReturnsAsync(exam1);
        _mockOralEhrContext.Setup(x => x.Exams.FindAsync(exam2.Id))
            .ReturnsAsync(exam2);

        // Act
        var result = await _studentExamRepository.GetExamById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetExamDtosByGroupId_ShouldReturnListOfExamDtos()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupId2 = Guid.NewGuid();

        var exam1 = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var exam2 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam3 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam4 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam5 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam6 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId2);

        var exams = new List<Exam>
            {
                exam1,
                exam2,
                exam3,
                exam4,
                exam5,
                exam6
            }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);

        // Act
        var result = await _studentExamRepository.GetExamDtosByGroupId(groupId);

        // Assert
        Assert.Equal(5, result.Count);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<ExamDto>>(result);
        Assert.Equal(exam5.DateOfExamination, result[0].DateOfExamination);
    }

    [Fact]
    public async Task GetExamDtosByGroupId_ShouldReturnEmptyList()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupId2 = Guid.NewGuid();

        var exam1 = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var exam2 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam3 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam4 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam5 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam6 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId2);

        var exams = new List<Exam>
            {
                exam1,
                exam2,
                exam3,
                exam4,
                exam5,
                exam6
            }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);

        // Act
        var result = await _studentExamRepository.GetExamDtosByGroupId(Guid.NewGuid());

        // Assert
        Assert.Empty(result);
        Assert.NotNull(result);
        Assert.IsType<List<ExamDto>>(result);
    }

    [Fact]
    public async Task GetExamDtoById_ShouldReturnExamDto()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupId2 = Guid.NewGuid();

        var exam1 = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var exam2 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam3 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam4 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam5 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam6 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId2);

        var exams = new List<Exam>
            {
                exam1,
                exam2,
                exam3,
                exam4,
                exam5,
                exam6
            }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);

        // Act
        var result = await _studentExamRepository.GetExamDtoById(exam1.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ExamDto>(result);
        Assert.Equal(exam1.Id, result.Id);
        Assert.NotEqual(exam2.Id, result.Id);
    }

    [Fact]
    public async Task GetExamDtoById_ShouldReturnNull()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var groupId2 = Guid.NewGuid();

        var exam1 = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var exam2 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam3 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam4 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam5 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId);
        var exam6 = new Exam(DateTime.Now, "title2", "description2", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, groupId2);

        var exams = new List<Exam>
            {
                exam1,
                exam2,
                exam3,
                exam4,
                exam5,
                exam6
            }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);

        // Act
        var result = await _studentExamRepository.GetExamDtoById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPracticePatientExaminationCardsByExamId_ShouldReturnCards()
    {
        // Arrange
        var teacher =
            new ApplicationUser("teacher@test.com", "TeacherFirstName", "TeacherLastName", "12345678", "hello");

        var group = new Group(teacher.Id, "group1")
        {
            Teacher = teacher
        };

        var exam1 = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, group.Id);

        var student1 = new ApplicationUser("test@test.com", "John", "Doe", "123456789", "hello");

        student1.StudentGroups.Add(new StudentGroup(group.Id, student1.Id) { Group = group, Student = student1 });

        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);

        var card1 = new PracticePatientExaminationCard(exam1.Id, student1.Id);
        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            Bewe = practiceBewe,
            DMFT_DMFS = practiceDMFT_DMFS,
            API = practiceAPI,
            Bleeding = practiceBleeding,
            PracticePatientExaminationCard = card1
        };

        card1.SetPatientId(practicePatient.Id);
        card1.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        card1.SetPatientExaminationResultId(practicePatientExaminationResult.Id);
        card1.SetDoctorComment("test");
        card1.SetStudentMark(20);
        card1.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        card1.SetStudent(student1);

        practiceBewe.Comment = "test";
        practiceDMFT_DMFS.AddComment("test");
        practiceBleeding.AddComment("test");
        practiceAPI.AddComment("test");

        exam1.PracticePatientExaminationCards.Add(card1);

        var users = new List<ApplicationUser> { student1, teacher }.AsQueryable().BuildMockDbSet();
        var groups = new List<Group> { group }.AsQueryable().BuildMockDbSet();
        var exams = new List<Exam> { exam1 }.AsQueryable().BuildMockDbSet();
        var paraticepatients = new List<PracticePatient> { practicePatient }.AsQueryable().BuildMockDbSet();
        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable().BuildMockDbSet();
        var practiceBewes = new List<PracticeBewe> { practiceBewe }.AsQueryable().BuildMockDbSet();
        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }.AsQueryable().BuildMockDbSet();
        var practiceAPIs = new List<PracticeAPI> { practiceAPI }.AsQueryable().BuildMockDbSet();
        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }.AsQueryable().BuildMockDbSet();
        var practicePatientExaminationResults =
            new List<PracticePatientExaminationResult> { practicePatientExaminationResult }.AsQueryable()
                .BuildMockDbSet();

        var cards = new List<PracticePatientExaminationCard> { card1 }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards).Returns(cards.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(paraticepatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBleedings).Returns(practiceBleedings.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(users.Object);


        // Act
        var result = await _studentExamRepository.GetPracticePatientExaminationCardsByExamId(exam1.Id);

        // Assert
        Assert.IsType<List<PracticePatientExaminationCardDto>>(result);
    }

    [Fact]
    public async Task GetPracticePatientExaminationCardsByExamId_ShouldReturnEmptyList()
    {
        // Arrange
        var teacher =
            new ApplicationUser("teacher@test.com", "TeacherFirstName", "TeacherLastName", "12345678", "hello");

        var group = new Group(teacher.Id, "group1")
        {
            Teacher = teacher
        };

        var exam1 = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, group.Id);

        var student1 = new ApplicationUser("test@test.com", "John", "Doe", "123456789", "hello");

        student1.StudentGroups.Add(new StudentGroup(group.Id, student1.Id) { Group = group, Student = student1 });

        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);

        var card1 = new PracticePatientExaminationCard(exam1.Id, student1.Id);
        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            Bewe = practiceBewe,
            DMFT_DMFS = practiceDMFT_DMFS,
            API = practiceAPI,
            Bleeding = practiceBleeding,
            PracticePatientExaminationCard = card1
        };

        card1.SetPatientId(practicePatient.Id);
        card1.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        card1.SetPatientExaminationResultId(practicePatientExaminationResult.Id);
        card1.SetDoctorComment("test");
        card1.SetStudentMark(20);
        card1.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        card1.SetStudent(student1);

        practiceBewe.Comment = "test";
        practiceDMFT_DMFS.AddComment("test");
        practiceBleeding.AddComment("test");
        practiceAPI.AddComment("test");

        exam1.PracticePatientExaminationCards.Add(card1);

        var users = new List<ApplicationUser> { student1, teacher }.AsQueryable().BuildMockDbSet();
        var groups = new List<Group> { group }.AsQueryable().BuildMockDbSet();
        var exams = new List<Exam> { exam1 }.AsQueryable().BuildMockDbSet();
        var paraticepatients = new List<PracticePatient> { practicePatient }.AsQueryable().BuildMockDbSet();
        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable().BuildMockDbSet();
        var practiceBewes = new List<PracticeBewe> { practiceBewe }.AsQueryable().BuildMockDbSet();
        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }.AsQueryable().BuildMockDbSet();
        var practiceAPIs = new List<PracticeAPI> { practiceAPI }.AsQueryable().BuildMockDbSet();
        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }.AsQueryable().BuildMockDbSet();
        var practicePatientExaminationResults =
            new List<PracticePatientExaminationResult> { practicePatientExaminationResult }.AsQueryable()
                .BuildMockDbSet();

        var cards = new List<PracticePatientExaminationCard> { card1 }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards).Returns(cards.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(paraticepatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBleedings).Returns(practiceBleedings.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(users.Object);


        // Act
        var result = await _studentExamRepository.GetPracticePatientExaminationCardsByExamId(Guid.NewGuid());

        // Assert
        Assert.IsType<List<PracticePatientExaminationCardDto>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPracticePatientExaminationCardDtoById_ShouldReturnCardDto()
    {
        //Arrange
        var groupId = Guid.NewGuid();

        var teacher =
            new ApplicationUser("teacher@test.com", "TeacherFirstName", "TeacherLastName", "12345678", "hello");
        var group = new Group(teacher.Id, "group1");

        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, group.Id);

        var student = new ApplicationUser("test@test.com", "John", "Doe", "12345678", "hello");

        var card = new PracticePatientExaminationCard(exam.Id, student.Id);

        var cards = new List<PracticePatientExaminationCard> { card }
            .AsQueryable()
            .BuildMockDbSet();

        var users = new List<ApplicationUser> { student, teacher }.AsQueryable().BuildMockDbSet();
        var groups = new List<Group> { group }.AsQueryable().BuildMockDbSet();
        var exams = new List<Exam> { exam }.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Users).Returns(users.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards).Returns(cards.Object);

        // Act
        var result = await _studentExamRepository.GetPracticePatientExaminationCardDtoById(card.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PracticePatientExaminationCardDto>(result);
        Assert.Equal(card.Id, result.Id);
    }

    [Fact]
    public async Task GetPracticePatientExaminationCardDtoById_ShouldReturnNull()
    {
        //Arrange
        var groupId = Guid.NewGuid();

        var teacher =
            new ApplicationUser("teacher@test.com", "TeacherFirstName", "TeacherLastName", "12345678", "hello");
        var group = new Group(teacher.Id, "group1");

        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, group.Id);

        var student = new ApplicationUser("test@test.com", "John", "Doe", "12345678", "hello");

        var card = new PracticePatientExaminationCard(exam.Id, student.Id);

        var cards = new List<PracticePatientExaminationCard> { card }
            .AsQueryable()
            .BuildMockDbSet();

        var users = new List<ApplicationUser> { student, teacher }.AsQueryable().BuildMockDbSet();
        var groups = new List<Group> { group }.AsQueryable().BuildMockDbSet();
        var exams = new List<Exam> { exam }.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Users).Returns(users.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards).Returns(cards.Object);

        // Act
        var result = await _studentExamRepository.GetPracticePatientExaminationCardDtoById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPracticeAPIByCardId_ShouldReturnAPI()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        practiceAPI.AddComment("test");
        practiceBleeding.AddComment("test");
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            API = practiceAPI,
            Bleeding = practiceBleeding
        };


        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var patientExaminationCard = new PracticePatientExaminationCard(exam.Id, "studentId");

        patientExaminationCard.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        patientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        patientExaminationCard.SetPatientId(practicePatient.Id);
        patientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);

        exam.PracticePatientExaminationCards.Add(patientExaminationCard);

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatients = new List<PracticePatient> { practicePatient }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBewes = new List<PracticeBewe> { practiceBewe }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceAPIs = new List<PracticeAPI> { practiceAPI }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>
                { practicePatientExaminationResult }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard> { patientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result = await _studentExamRepository.GetPracticeAPIByCardId(patientExaminationCard.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PracticeAPI>(result);
        Assert.Equal(practiceAPI.Id, result.Id);
        Assert.Equal(practiceAPI, result);
    }

    [Fact]
    public async Task GetPracticeBleedingByCardId_ShouldReturnBleeding()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        practiceAPI.AddComment("test");
        practiceBleeding.AddComment("test");
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            API = practiceAPI,
            Bleeding = practiceBleeding
        };


        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var patientExaminationCard = new PracticePatientExaminationCard(exam.Id, "studentId");

        patientExaminationCard.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        patientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        patientExaminationCard.SetPatientId(practicePatient.Id);
        patientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);

        exam.PracticePatientExaminationCards.Add(patientExaminationCard);

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatients = new List<PracticePatient> { practicePatient }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBewes = new List<PracticeBewe> { practiceBewe }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceAPIs = new List<PracticeAPI> { practiceAPI }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>
                { practicePatientExaminationResult }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard> { patientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result = await _studentExamRepository.GetPracticeBleedingByCardId(patientExaminationCard.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PracticeBleeding>(result);
        Assert.Equal(practiceBleeding.Id, result.Id);
        Assert.Equal(practiceBleeding, result);
    }

    [Fact]
    public async Task GetPracticeAPIByCardId_ShouldReturnNull()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        practiceAPI.AddComment("test");
        practiceBleeding.AddComment("test");
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            API = practiceAPI,
            Bleeding = practiceBleeding
        };


        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var patientExaminationCard = new PracticePatientExaminationCard(exam.Id, "studentId");

        patientExaminationCard.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        patientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        patientExaminationCard.SetPatientId(practicePatient.Id);
        patientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);

        exam.PracticePatientExaminationCards.Add(patientExaminationCard);

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatients = new List<PracticePatient> { practicePatient }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBewes = new List<PracticeBewe> { practiceBewe }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceAPIs = new List<PracticeAPI> { practiceAPI }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>
                { practicePatientExaminationResult }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard> { patientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result = await _studentExamRepository.GetPracticeAPIByCardId(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPracticeBleedingByCardId_ShouldReturnNull()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        practiceAPI.AddComment("test");
        practiceBleeding.AddComment("test");
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            API = practiceAPI,
            Bleeding = practiceBleeding
        };


        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var patientExaminationCard = new PracticePatientExaminationCard(exam.Id, "studentId");

        patientExaminationCard.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        patientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        patientExaminationCard.SetPatientId(practicePatient.Id);
        patientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);

        exam.PracticePatientExaminationCards.Add(patientExaminationCard);

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatients = new List<PracticePatient> { practicePatient }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBewes = new List<PracticeBewe> { practiceBewe }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceAPIs = new List<PracticeAPI> { practiceAPI }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>
                { practicePatientExaminationResult }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard> { patientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result = await _studentExamRepository.GetPracticeBleedingByCardId(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPracticeDMFT_DMFSByCardId_ShouldReturnDMFT_DMFS()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        practiceAPI.AddComment("test");
        practiceBleeding.AddComment("test");
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            API = practiceAPI,
            Bleeding = practiceBleeding,
            DMFT_DMFS = practiceDMFT_DMFS
        };


        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var patientExaminationCard = new PracticePatientExaminationCard(exam.Id, "studentId");

        patientExaminationCard.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        patientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        patientExaminationCard.SetPatientId(practicePatient.Id);
        patientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);

        exam.PracticePatientExaminationCards.Add(patientExaminationCard);

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatients = new List<PracticePatient> { practicePatient }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBewes = new List<PracticeBewe> { practiceBewe }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceAPIs = new List<PracticeAPI> { practiceAPI }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>
                { practicePatientExaminationResult }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard> { patientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);
        // Act
        var result = await _studentExamRepository.GetPracticeDMFT_DMFSByCardId(patientExaminationCard.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PracticeDMFT_DMFS>(result);
        Assert.Equal(practiceDMFT_DMFS.Id, result.Id);
        Assert.Equal(practiceDMFT_DMFS, result);
    }

    [Fact]
    public async Task GetPracticeDMFT_DMFSByCardId_ShouldReturnNull()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        practiceAPI.AddComment("test");
        practiceBleeding.AddComment("test");
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            API = practiceAPI,
            Bleeding = practiceBleeding,
            Bewe = practiceBewe
        };


        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var patientExaminationCard = new PracticePatientExaminationCard(exam.Id, "studentId");

        patientExaminationCard.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        patientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        patientExaminationCard.SetPatientId(practicePatient.Id);
        patientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);

        exam.PracticePatientExaminationCards.Add(patientExaminationCard);

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatients = new List<PracticePatient> { practicePatient }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBewes = new List<PracticeBewe> { practiceBewe }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceAPIs = new List<PracticeAPI> { practiceAPI }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>
                { practicePatientExaminationResult }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard> { patientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);
        var result = await _studentExamRepository.GetPracticeDMFT_DMFSByCardId(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPracticeBeweByCardId_ShouldReturnBewe()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        practiceAPI.AddComment("test");
        practiceBleeding.AddComment("test");
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            API = practiceAPI,
            Bleeding = practiceBleeding,
            Bewe = practiceBewe
        };


        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var patientExaminationCard = new PracticePatientExaminationCard(exam.Id, "studentId");

        patientExaminationCard.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        patientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        patientExaminationCard.SetPatientId(practicePatient.Id);
        patientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);

        exam.PracticePatientExaminationCards.Add(patientExaminationCard);

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatients = new List<PracticePatient> { practicePatient }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBewes = new List<PracticeBewe> { practiceBewe }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceAPIs = new List<PracticeAPI> { practiceAPI }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>
                { practicePatientExaminationResult }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard> { patientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result = await _studentExamRepository.GetPracticeBeweByCardId(patientExaminationCard.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PracticeBewe>(result);
        Assert.Equal(practiceBewe.Id, result.Id);
        Assert.Equal(practiceBewe, result);
    }

    [Fact]
    public async Task GetPracticeBeweByCardId_ShouldReturnNull()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        practiceAPI.AddComment("test");
        practiceBleeding.AddComment("test");
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id)
        {
            API = practiceAPI,
            Bleeding = practiceBleeding
        };


        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var patientExaminationCard = new PracticePatientExaminationCard(exam.Id, "studentId");

        patientExaminationCard.SetPracticePatientExaminationResult(practicePatientExaminationResult);
        patientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        patientExaminationCard.SetPatientId(practicePatient.Id);
        patientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);

        exam.PracticePatientExaminationCards.Add(patientExaminationCard);

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatients = new List<PracticePatient> { practicePatient }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBewes = new List<PracticeBewe> { practiceBewe }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceAPIs = new List<PracticeAPI> { practiceAPI }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>
                { practicePatientExaminationResult }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard> { patientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result = await _studentExamRepository.GetPracticeBeweByCardId(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public async Task AddPracticePatientExaminationCard_ShouldAddCard()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var studentId = "studentId";

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        await _studentExamRepository.AddPracticePatientExaminationCard(practicePatientExaminationCard);

        // Assert
        _mockOralEhrContext.Verify(
            x => x.PracticePatientExaminationCards.AddAsync(practicePatientExaminationCard, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task AddPracticePatient_ShouldAddPatient()
    {
        // Arrange
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);

        var practicePatients = new List<PracticePatient>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);

        // Act
        await _studentExamRepository.AddPracticePatient(practicePatient);

        // Assert
        _mockOralEhrContext.Verify(x => x.PracticePatients.AddAsync(practicePatient, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task AddPracticeRiskFactorAssessment_ShouldAddRiskFactorAssessment()
    {
        // Arrange
        var riskFactorAssessment = new PracticeRiskFactorAssessment();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);

        // Act
        await _studentExamRepository.AddPracticeRiskFactorAssessment(riskFactorAssessment);

        // Assert
        _mockOralEhrContext.Verify(
            x => x.PracticeRiskFactorAssessments.AddAsync(riskFactorAssessment, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddPracticeDMFT_DMFS_ShouldAddDMFTDMFS()
    {
        // Arrange
        var dmftDmfs = new PracticeDMFT_DMFS(22, 22);

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);

        // Act
        await _studentExamRepository.AddPracticeDMFT_DMFS(dmftDmfs);

        // Assert
        _mockOralEhrContext.Verify(x => x.PracticeDMFT_DMFSs.AddAsync(dmftDmfs, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddPracticeBewe_ShouldAddBewe()
    {
        // Arrange
        var bewe = new PracticeBewe(22);

        var practiceBewes = new List<PracticeBewe>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);

        // Act
        await _studentExamRepository.AddPracticeBewe(bewe);

        // Assert
        _mockOralEhrContext.Verify(x => x.PracticeBewes.AddAsync(bewe, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddPracticeAPI_ShouldAddAPI()
    {
        // Arrange
        var api = new PracticeAPI(22, 10, 10);

        var practiceAPIs = new List<PracticeAPI>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);

        // Act
        await _studentExamRepository.AddPracticeAPI(api);

        // Assert
        _mockOralEhrContext.Verify(x => x.PracticeAPIs.AddAsync(api, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddPracticeBleeding_ShouldAddBleeding()
    {
        // Arrange
        var bleeding = new PracticeBleeding(22, 10, 10);

        var practiceBleedings = new List<PracticeBleeding>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticeBleedings).Returns(practiceBleedings.Object);

        // Act
        await _studentExamRepository.AddPracticeBleeding(bleeding);

        // Assert
        _mockOralEhrContext.Verify(x => x.PracticeBleedings.AddAsync(bleeding, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddPracticePatientExaminationResult_ShouldAddPracticeExaminationResult()
    {
        // Arrange
        var bewe = new PracticeBewe(22);
        var dmftDmfs = new PracticeDMFT_DMFS(22, 22);
        var api = new PracticeAPI(22, 10, 10);
        var bleeding = new PracticeBleeding(22, 10, 10);

        var practicePatientExaminationResult =
            new PracticePatientExaminationResult(bewe.Id, dmftDmfs.Id, api.Id, bleeding.Id);

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);

        // Act
        await _studentExamRepository.AddPracticePatientExaminationResult(practicePatientExaminationResult);

        // Assert
        _mockOralEhrContext.Verify(
            x => x.PracticePatientExaminationResults.AddAsync(practicePatientExaminationResult, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task CheckIfStudentHasAlreadyTakenTheExam_ShouldReturnTrue_WhenStudentHasTakenExam()
    {
        // Arrange
        var studentId = "studentId";
        var groupId = Guid.NewGuid();

        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);


        var practicePatientExaminationCards = new List<PracticePatientExaminationCard>
                { practicePatientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result = await _studentExamRepository.CheckIfStudentHasAlreadyTakenTheExam(exam.Id, studentId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckIfStudentHasAlreadyTakenTheExam_ShouldReturnFalse_WhenStudentHasNotTakenExam()
    {
        // Arrange
        var studentId = "studentId";
        var groupId = Guid.NewGuid();

        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result = await _studentExamRepository.CheckIfStudentHasAlreadyTakenTheExam(exam.Id, "studentId2");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetPracticePatientExaminationCardByExamIdAndStudentId_ShouldReturnPatientExaminationCardDto()
    {
        // Arrange
        var teacher =
            new ApplicationUser("teacher@test.com", "TeacherFirstName", "TeacherLastName", "12345678", "hello");
        var student =
            new ApplicationUser("student@test.com", "StudentFirstName", "StudentLastName", "12345678", "hello");
        var group = new Group(teacher.Id, "groupName");

        var studentGroup = new StudentGroup(group.Id, student.Id);

        studentGroup.Student = student;

        group.StudentGroups.Add(studentGroup);

        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, group.Id);

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id);

        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);


        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, student.Id);
        practicePatientExaminationCard.SetPatientId(practicePatient.Id);
        practicePatientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        practicePatientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);
        practicePatientExaminationCard.SetStudentMark(20);
        practicePatientExaminationCard.SetDoctorComment("test");

        exam.PracticePatientExaminationCards.Add(practicePatientExaminationCard);

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard>
                { practicePatientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticePatients)
            .Returns(new List<PracticePatient> { practicePatient }.AsQueryable().BuildMockDbSet().Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(
            new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }.AsQueryable().BuildMockDbSet()
                .Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes)
            .Returns(new List<PracticeBewe> { practiceBewe }.AsQueryable().BuildMockDbSet().Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable().BuildMockDbSet().Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs)
            .Returns(new List<PracticeAPI> { practiceAPI }.AsQueryable().BuildMockDbSet().Object);
        _mockOralEhrContext.Setup(x => x.PracticeBleedings).Returns(new List<PracticeBleeding> { practiceBleeding }
            .AsQueryable().BuildMockDbSet().Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults).Returns(
            new List<PracticePatientExaminationResult> { practicePatientExaminationResult }.AsQueryable()
                .BuildMockDbSet().Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);
        _mockOralEhrContext.Setup(x => x.Exams).Returns(new List<Exam> { exam }.AsQueryable().BuildMockDbSet().Object);
        _mockOralEhrContext.Setup(x => x.Users)
            .Returns(new List<ApplicationUser> { student, teacher }.AsQueryable().BuildMockDbSet().Object);
        _mockOralEhrContext.Setup(x => x.Groups)
            .Returns(new List<Group> { group }.AsQueryable().BuildMockDbSet().Object);
        _mockOralEhrContext.Setup(x => x.StudentGroups)
            .Returns(new List<StudentGroup> { studentGroup }.AsQueryable().BuildMockDbSet().Object);

        // Act
        var result =
            await _studentExamRepository.GetPracticePatientExaminationCardByExamIdAndStudentId(exam.Id, student.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PracticePatientExaminationCardDto>(result);
        Assert.Equal(practicePatientExaminationCard.Id, result.Id);
    }

    [Fact]
    public async Task GetPracticePatientExaminationCardByExamIdAndStudentId_ShoudlReturnNull()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var studentId = "studentId";

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        var result =
            await _studentExamRepository.GetPracticePatientExaminationCardByExamIdAndStudentId(exam.Id, "studentId2");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteExam_ShouldDeleteExam()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatient = new PracticePatient("test", "test", "testpatient@test.com", Gender.Male, "test", "warsaw",
            19, "test", "test", "test", "test", 2);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practiceAPI = new PracticeAPI(22, 10, 10);
        var practiceBleeding = new PracticeBleeding(22, 10, 10);
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id,
            practiceDMFT_DMFS.Id, practiceAPI.Id, practiceBleeding.Id);

        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        var patientExaminationCard = new PracticePatientExaminationCard(exam.Id, "studentId")
        {
            PracticePatientExaminationResult = practicePatientExaminationResult
        };

        patientExaminationCard.SetRiskFactorAssessmentId(practiceRiskFactorAssessment.Id);
        patientExaminationCard.SetPatientId(practicePatient.Id);
        patientExaminationCard.SetPatientExaminationResultId(practicePatientExaminationResult.Id);

        exam.PracticePatientExaminationCards.Add(patientExaminationCard);

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatients = new List<PracticePatient> { practicePatient }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceRiskFactorAssessments = new List<PracticeRiskFactorAssessment> { practiceRiskFactorAssessment }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBewes = new List<PracticeBewe> { practiceBewe }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceDMFT_DMFSs = new List<PracticeDMFT_DMFS> { practiceDMFT_DMFS }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceAPIs = new List<PracticeAPI> { practiceAPI }
            .AsQueryable()
            .BuildMockDbSet();

        var practiceBleedings = new List<PracticeBleeding> { practiceBleeding }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationResults = new List<PracticePatientExaminationResult>
                { practicePatientExaminationResult }
            .AsQueryable()
            .BuildMockDbSet();

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard> { patientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatients).Returns(practicePatients.Object);
        _mockOralEhrContext.Setup(x => x.PracticeRiskFactorAssessments).Returns(practiceRiskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBewes).Returns(practiceBewes.Object);
        _mockOralEhrContext.Setup(x => x.PracticeDMFT_DMFSs).Returns(practiceDMFT_DMFSs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeAPIs).Returns(practiceAPIs.Object);
        _mockOralEhrContext.Setup(x => x.PracticeBleedings).Returns(practiceBleedings.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationResults)
            .Returns(practicePatientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);

        // Act
        await _studentExamRepository.DeleteExam(exam.Id);

        // Assert
        _mockOralEhrContext.Verify(x => x.Exams.Remove(exam), Times.Once);
        _mockOralEhrContext.Verify(x => x.PracticePatients.Remove(It.IsAny<PracticePatient>()), Times.Once);
        _mockOralEhrContext.Verify(
            x => x.PracticeRiskFactorAssessments.Remove(It.IsAny<PracticeRiskFactorAssessment>()), Times.Once);
        _mockOralEhrContext.Verify(x => x.PracticeBewes.Remove(It.IsAny<PracticeBewe>()), Times.Once);
        _mockOralEhrContext.Verify(x => x.PracticeDMFT_DMFSs.Remove(It.IsAny<PracticeDMFT_DMFS>()), Times.Once);
        _mockOralEhrContext.Verify(x => x.PracticeAPIs.Remove(It.IsAny<PracticeAPI>()), Times.Once);
        _mockOralEhrContext.Verify(x => x.PracticeBleedings.Remove(It.IsAny<PracticeBleeding>()), Times.Once);
    }

    [Fact]
    public async Task UpcomingExams_ShouldReturnExamDtos()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var studentId = Guid.NewGuid().ToString();

        var group = new Group(groupId.ToString(), "Group 1");

        var student1 = new ApplicationUser("test@test.com", "Test", "User", studentId, "comment");
        var studentGroup1 = new StudentGroup(groupId, student1.Id)
        {
            Student = student1
        };

        group.StudentGroups.Add(studentGroup1);

        var exam1 = new Exam(DateTime.UtcNow.AddDays(1), "title", "description", TimeOnly.Parse("15:00"),
            TimeOnly.Parse("18:00"), TimeSpan.MaxValue, 20, groupId)
        {
            Group = group
        };

        var studentGroups = new List<StudentGroup> { studentGroup1 }
            .AsQueryable()
            .BuildMockDbSet();

        var exams = new List<Exam> { exam1 }
            .AsQueryable()
            .BuildMockDbSet();

        var students = new List<ApplicationUser> { student1 }
            .AsQueryable()
            .BuildMockDbSet();

        var groups = new List<Group> { group }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Users).Returns(students.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);
        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);

        // Act
        var result = await _studentExamRepository.UpcomingExams(student1.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<ExamDto>>(result);
        Assert.Single(result);
        Assert.Equal(exam1.Id, result[0].Id);
    }

    [Fact]
    public async Task UpcomingExams_ShouldReturnEmptylist()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var studentId = Guid.NewGuid().ToString();

        var group = new Group(groupId.ToString(), "Group 1");

        var student1 = new ApplicationUser("test@test.com", "Test", "User", studentId, "comment");
        var studentGroup1 = new StudentGroup(groupId, student1.Id)
        {
            Student = student1
        };

        group.StudentGroups.Add(studentGroup1);

        var exam1 = new Exam(DateTime.UtcNow.AddDays(1), "title", "description", TimeOnly.Parse("15:00"),
            TimeOnly.Parse("18:00"), TimeSpan.MaxValue, 20, groupId)
        {
            Group = group
        };

        var studentGroups = new List<StudentGroup> { studentGroup1 }
            .AsQueryable()
            .BuildMockDbSet();

        var exams = new List<Exam> { exam1 }
            .AsQueryable()
            .BuildMockDbSet();

        var students = new List<ApplicationUser> { student1 }
            .AsQueryable()
            .BuildMockDbSet();

        var groups = new List<Group> { group }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Users).Returns(students.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);
        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);

        // Act
        var result = await _studentExamRepository.UpcomingExams(Guid.NewGuid().ToString());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<ExamDto>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task PublishExam_ShouldAddExamToContextAndReturnExamDto()
    {
        // Arrange
        var exam = new Exam(DateTime.Now, "Test Exam", "Description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 30, Guid.NewGuid());

        var exams = new List<Exam>()
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);

        // Act
        var result = await _studentExamRepository.PublishExam(exam);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ExamDto>(result);
        Assert.Equal(exam.Id, result.Id);
        _mockOralEhrContext.Verify(x => x.Exams.AddAsync(exam, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetStudentExamResults_ShouldReturnListOfStudentExamResultResponseDto()
    {
        // Arrange
        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, Guid.NewGuid());

        var group = new Group("teacherId", "groupName");

        var student = new ApplicationUser("test@test.com", "Test", "User", "123456789", "comment");
        var studentGroup = new StudentGroup(group.Id, student.Id);
        group.StudentGroups.Add(studentGroup);

        exam.Group = group;
        studentGroup.Group = group;

        exam.MarksAsGraded();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, student.Id);

        exam.PracticePatientExaminationCards.Add(practicePatientExaminationCard);

        practicePatientExaminationCard.SetStudentMark(90);
        practicePatientExaminationCard.SetStudent(student);

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard>
                { practicePatientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var students = new List<ApplicationUser> { student }
            .AsQueryable()
            .BuildMockDbSet();

        var groups = new List<Group> { group }
            .AsQueryable()
            .BuildMockDbSet();

        var studentGroups = new List<StudentGroup> { studentGroup }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(students.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);

        // Act
        var result = await _studentExamRepository.GetStudentExamResults(exam.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<StudentExamResultResponseDto>>(result);
        Assert.Single(result);
        Assert.Equal(practicePatientExaminationCard.StudentMark, result[0].StudentMark);
        Assert.Equal(practicePatientExaminationCard.Student.FirstName, result[0].FirstName);
    }

    [Fact]
    public async Task GetStudentExamResults_ShouldReturnEmptyList()
    {
        // Arrange
        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, Guid.NewGuid());

        var group = new Group("teacherId", "groupName");

        var student = new ApplicationUser("test@test.com", "Test", "User", "123456789", "comment");

        var studentGroup = new StudentGroup(group.Id, student.Id);
        group.StudentGroups.Add(studentGroup);

        exam.Group = group;
        studentGroup.Group = group;

        exam.MarksAsGraded();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, student.Id);

        exam.PracticePatientExaminationCards.Add(practicePatientExaminationCard);

        practicePatientExaminationCard.SetStudentMark(90);
        practicePatientExaminationCard.SetStudent(student);

        var practicePatientExaminationCards = new List<PracticePatientExaminationCard>
                { practicePatientExaminationCard }
            .AsQueryable()
            .BuildMockDbSet();

        var exams = new List<Exam> { exam }
            .AsQueryable()
            .BuildMockDbSet();

        var students = new List<ApplicationUser> { student }
            .AsQueryable()
            .BuildMockDbSet();

        var groups = new List<Group> { group }
            .AsQueryable()
            .BuildMockDbSet();

        var studentGroups = new List<StudentGroup> { studentGroup }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Exams).Returns(exams.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(students.Object);
        _mockOralEhrContext.Setup(x => x.PracticePatientExaminationCards)
            .Returns(practicePatientExaminationCards.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);

        // Act
        var result = await _studentExamRepository.GetStudentExamResults(Guid.NewGuid());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<StudentExamResultResponseDto>>(result);
        Assert.Empty(result);
    }
}