using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.SuperviseDtos.Response;
using App.Domain.Models.Users;
using App.Persistence.Contexts;
using App.Persistence.Repository;
using AutoMapper;
using MockQueryable.Moq;
using Moq;

namespace App.Persistence.Test.Repository;

public class SuperviseRepositoryTests
{
    private readonly Mock<OralEhrContext> _mockOralEhrContext;
    private readonly SuperviseRepository _superviseRepository;

    public SuperviseRepositoryTests()
    {
        _mockOralEhrContext = new Mock<OralEhrContext>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, StudentResponseDto>()
            .ForMember(x => x.Groups, o => o.MapFrom(s => s.StudentGroups
            .Select(x => x.Group.GroupName).ToList()));
            cfg.CreateMap<ApplicationUser, SupervisingDoctorResponseDto>()
            .ForMember(x => x.Id, o => o.MapFrom(s => (s.Id)))
            .ForMember(x => x.DoctorName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"));
        });
        var mapper = mapperConfig.CreateMapper();
        _superviseRepository = new SuperviseRepository(_mockOralEhrContext.Object, mapper);
    }

    [Fact]
    public async Task AddSupervise_ShouldAddSupervise()
    {
        // Arrange
        var doctorId = "doctorId";
        var studentId = "studentId";
        var supervise = new Supervise(doctorId, studentId);

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);

        // Act
        await _superviseRepository.AddSupervise(supervise);

        // Assert
        _mockOralEhrContext.Verify(x => x.Supervises.AddAsync(supervise, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task CheckStudentAlreadyUnderDoctorSupervision_ShouldReturnTrue_WhenStudentAlreadyUnderDoctorSupervision()
    {
        // Arrange
        var doctorId = "doctorId";
        var studentId = "studentId";
        var supervise = new Supervise(doctorId, studentId);

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);

        // Act
        var result = await _superviseRepository.CheckStudentAlreadyUnderDoctorSupervison(studentId, doctorId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckStudentAlreadyUnderDoctorSupervision_ShouldReturnFalse_WhenStudentNotUnderDoctorSupervision()
    {
        // Arrange
        var doctorId = "doctorId";
        var studentId = "studentId";
        var supervise = new Supervise(doctorId, studentId);

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);

        // Act
        var result = await _superviseRepository.CheckStudentAlreadyUnderDoctorSupervison("studentId2", "doctorId2");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetAllStudentsUnderSupervisionByDoctorId_ShouldReturnListOfStudentResponseDto()
    {
        // Arrange
        var applicationUserRoles = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole =  new ApplicationRole { Name = "Doctor"}}
        };

        var doctor = new ApplicationUser("test1@test.com", "Test1", "User1", "74185521", "comment")
        { ApplicationUserRoles = applicationUserRoles };

        var applicationUserRoles1 = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole =  new ApplicationRole { Name = "Student"}}
        };

        var student = new ApplicationUser("test@test.com", "Test", "User", "7418552", "comment")
        { ApplicationUserRoles = applicationUserRoles1 };


        var group = new Group(doctor.Id, "test");
        var studentGroup = new StudentGroup(group.Id, student.Id);
        group.StudentGroups.Add(studentGroup);

        var supervise = new Supervise(doctor.Id, student.Id);


        supervise.Doctor = doctor;
        supervise.Student = student;

        var students = new List<ApplicationUser> { student, doctor }
        .AsQueryable()
        .BuildMockDbSet();

        var mockApplicationUserRoles = applicationUserRoles.AsQueryable().BuildMockDbSet();

        var mockApplicationUserRoles1 = applicationUserRoles1.AsQueryable().BuildMockDbSet();

        var groups = new List<Group> { group }
        .AsQueryable()
        .BuildMockDbSet();

        var studentGroups = new List<StudentGroup> { studentGroup }
        .AsQueryable()
        .BuildMockDbSet();

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(students.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(mockApplicationUserRoles.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(mockApplicationUserRoles1.Object);

        // Act
        var result = _superviseRepository.GetAllStudentsUnderSupervisionByDoctorId(doctor.Id);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsAssignableFrom<IQueryable<StudentResponseDto>>(result);
        Assert.Single(result);
        Assert.Equal(student.UserName, result.First().UserName);
    }

    [Fact]
    public void GetAllStudentsUnderSupervisionByDoctorId_ShouldReturnEmptyList_WhenNoStudentsUnderSupervision()
    {
        // Arrange
        var applicationUserRoles = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "Doctor" } }
        };

        var doctor = new ApplicationUser("test1@test.com", "Test1", "User1", "74185521", "comment")
        { ApplicationUserRoles = applicationUserRoles };

        var emptyStudentList = new List<ApplicationUser>().AsQueryable().BuildMockDbSet();

        var emptyGroups = new List<Group>().AsQueryable().BuildMockDbSet();

        var emptyStudentGroups = new List<StudentGroup>().AsQueryable().BuildMockDbSet();

        var emptySupervises = new List<Supervise>().AsQueryable().BuildMockDbSet();

        var emptyApplicationUserRoles = new List<ApplicationUserRole>().AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(emptySupervises.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(emptyStudentList.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(emptyGroups.Object);
        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(emptyStudentGroups.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(emptyApplicationUserRoles.Object);

        // Act
        var result = _superviseRepository.GetAllStudentsUnderSupervisionByDoctorId(doctor.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllSupervisingDoctorsByStudentId_ShouldReturnListOfSupervisingDoctorResponseDto()
    {
        // Arrange
        var applicationUserRoles = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole =  new ApplicationRole { Name = "Doctor"}}
        };

        var doctor = new ApplicationUser("test1@test.com", "Test1", "User1", "74185521", "comment")
        { ApplicationUserRoles = applicationUserRoles };

        var applicationUserRoles1 = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole =  new ApplicationRole { Name = "Student"}}
        };

        var student = new ApplicationUser("test@test.com", "Test", "User", "7418552", "comment")
        { ApplicationUserRoles = applicationUserRoles1 };


        var group = new Group(doctor.Id, "test");
        var studentGroup = new StudentGroup(group.Id, student.Id);
        group.StudentGroups.Add(studentGroup);

        var supervise = new Supervise(doctor.Id, student.Id);


        supervise.Doctor = doctor;
        supervise.Student = student;

        var students = new List<ApplicationUser> { student, doctor }
        .AsQueryable()
        .BuildMockDbSet();

        var mockApplicationUserRoles = applicationUserRoles.AsQueryable().BuildMockDbSet();

        var mockApplicationUserRoles1 = applicationUserRoles1.AsQueryable().BuildMockDbSet();

        var groups = new List<Group> { group }
        .AsQueryable()
        .BuildMockDbSet();

        var studentGroups = new List<StudentGroup> { studentGroup }
        .AsQueryable()
        .BuildMockDbSet();

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(students.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);
        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(mockApplicationUserRoles.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(mockApplicationUserRoles1.Object);

        // Act
        var result = await _superviseRepository.GetAllSupervisingDoctorsByStudentId(student.Id);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<SupervisingDoctorResponseDto>>(result);
        Assert.Single(result);
        var doctorName = $"{doctor.FirstName} {doctor.LastName}";
        Assert.Equal(doctorName, result.First().DoctorName);
    }

    [Fact]
    public async Task GetAllSupervisingDoctorsByStudentId_ShouldReturnEmptyList_WhenNoSupervisingDoctors()
    {
        // Arrange
        var applicationUserRoles = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "Doctor" } }
        };

        var doctor = new ApplicationUser("test1@test.com", "Test1", "User1", "74185521", "comment")
        { ApplicationUserRoles = applicationUserRoles };

        var emptyStudentList = new List<ApplicationUser>().AsQueryable().BuildMockDbSet();

        var emptyGroups = new List<Group>().AsQueryable().BuildMockDbSet();

        var emptyStudentGroups = new List<StudentGroup>().AsQueryable().BuildMockDbSet();

        var emptySupervises = new List<Supervise>().AsQueryable().BuildMockDbSet();

        var emptyApplicationUserRoles = new List<ApplicationUserRole>().AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(emptySupervises.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(emptyStudentList.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(emptyGroups.Object);
        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(emptyStudentGroups.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(emptyApplicationUserRoles.Object);

        // Act
        var result = await _superviseRepository.GetAllSupervisingDoctorsByStudentId(doctor.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void RemoveSupervise_ShouldRemoveSupervise()
    {
        // Arrange
        var doctorId = "doctorId";
        var studentId = "studentId";
        var supervise = new Supervise(doctorId, studentId);

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);

        // Act
        _superviseRepository.RemoveSupervise(supervise);

        // Assert
        _mockOralEhrContext.Verify(x => x.Supervises.Remove(supervise), Times.Once);
    }

    [Fact]
    public async Task GetSuperviseByDoctorIdAndStudentId_ShouldReturnSupervise()
    {
        // Arrange
        var doctorId = "doctorId";
        var studentId = "studentId";
        var supervise = new Supervise(doctorId, studentId);

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);

        // Act
        var result = await _superviseRepository.GetSuperviseByDoctorIdAndStudentId(doctorId, studentId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Supervise>(result);
        Assert.Equal(doctorId, result.DoctorId);
        Assert.Equal(studentId, result.StudentId);
    }

    [Fact]
    public async Task GetSuperviseByDoctorIdAndStudentId_ShouldReturnNull_WhenSuperviseNotFound()
    {
        // Arrange
        var doctorId = "doctorId";
        var studentId = "studentId";
        var supervise = new Supervise(doctorId, studentId);

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);

        // Act
        var result = await _superviseRepository.GetSuperviseByDoctorIdAndStudentId("doctorId2", "studentId2");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllStudentsNotUnderSupervisionByDoctorId_ShouldReturnListOfStudentResponseDto()
    {
        // Arrange
        var applicationUserRoles = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole =  new ApplicationRole { Name = "Doctor"}}
        };

        var doctor = new ApplicationUser("test1@test.com", "Test1", "User1", "74185521", "comment")
        { ApplicationUserRoles = applicationUserRoles };

        var doctor2 = new ApplicationUser("test4@test.com", "Test4", "User4", "74185521", "comment")
        { ApplicationUserRoles = applicationUserRoles };

        var applicationUserRoles1 = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole =  new ApplicationRole { Name = "Student"}}
        };

        var student = new ApplicationUser("test@test.com", "Test", "User", "7418552", "comment")
        { ApplicationUserRoles = applicationUserRoles1 };

        var student2 = new ApplicationUser("test2@test.com", "Test2", "User2", "7418552", "comment")
        { ApplicationUserRoles = applicationUserRoles1 };

        var student3 = new ApplicationUser("test3@test.com", "Tes3t", "User3", "7418552", "comment")
        { ApplicationUserRoles = applicationUserRoles1 };

        var supervise = new Supervise(doctor.Id, student.Id);

        supervise.Doctor = doctor;
        supervise.Student = student;

        var supervise2 = new Supervise(doctor.Id, student2.Id);
        supervise.Student = student2;
        supervise.Doctor = doctor;

        student.SuperviseStudentNavigation = new List<Supervise> { supervise };
        student2.SuperviseStudentNavigation = new List<Supervise> { supervise2 };

        var users = new List<ApplicationUser> { student, doctor, student2, student3, doctor2 }
        .AsQueryable()
        .BuildMockDbSet();

        var mockApplicationUserRoles = applicationUserRoles.AsQueryable().BuildMockDbSet();

        var mockApplicationUserRoles1 = applicationUserRoles1.AsQueryable().BuildMockDbSet();

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(users.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(mockApplicationUserRoles.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(mockApplicationUserRoles1.Object);

        // Act
        var result = _superviseRepository.GetAllStudentsNotUnderSupervisionByDoctorId(doctor2.Id);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsAssignableFrom<IQueryable<StudentResponseDto>>(result);
        Assert.Equal(3, result.Count());
        Assert.Equal(student.UserName, result.ToList()[0].UserName);
        Assert.Equal(student2.UserName, result.ToList()[1].UserName);
        Assert.Equal(student3.UserName, result.ToList()[2].UserName);
    }

    [Fact]
    public void GetAllStudentsNotUnderSupervisionByDoctorId_ShouldReturnEmpty_WhenAllStudentsAreUnderSupervisionOfDoctor()
    {
        // Arrange
        var applicationUserRoles = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole =  new ApplicationRole { Name = "Doctor"}}
        };

        var doctor = new ApplicationUser("test1@test.com", "Test1", "User1", "74185521", "comment")
        { ApplicationUserRoles = applicationUserRoles };

        var applicationUserRoles1 = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole =  new ApplicationRole { Name = "Student"}}
        };

        var student = new ApplicationUser("test@test.com", "Test", "User", "7418552", "comment")
        { ApplicationUserRoles = applicationUserRoles1 };

        var student2 = new ApplicationUser("test2@test.com", "Test2", "User2", "7418552", "comment")
        { ApplicationUserRoles = applicationUserRoles1 };

        var supervise = new Supervise(doctor.Id, student.Id);

        supervise.Doctor = doctor;
        supervise.Student = student;

        var supervise2 = new Supervise(doctor.Id, student2.Id);
        supervise.Student = student2;
        supervise.Doctor = doctor;

        student.SuperviseStudentNavigation = new List<Supervise> { supervise };
        student2.SuperviseStudentNavigation = new List<Supervise> { supervise2 };

        var users = new List<ApplicationUser> { student, doctor, student2 }
        .AsQueryable()
        .BuildMockDbSet();

        var mockApplicationUserRoles = applicationUserRoles.AsQueryable().BuildMockDbSet();

        var mockApplicationUserRoles1 = applicationUserRoles1.AsQueryable().BuildMockDbSet();

        var supervises = new List<Supervise> { supervise }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Supervises).Returns(supervises.Object);
        _mockOralEhrContext.Setup(x => x.Users).Returns(users.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(mockApplicationUserRoles.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(mockApplicationUserRoles1.Object);

        // Act
        var result = _superviseRepository.GetAllStudentsNotUnderSupervisionByDoctorId(doctor.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
