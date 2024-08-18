using App.Application.DentistTeacherOperations.Command.SuperviseStudent;
using App.Domain.Models.Users;
using MediatR;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.SuperviseStudent;

public class SuperviseStudentHandlerTests : TestHelper
{

    private readonly SuperviseStudentHandler handler;

    public SuperviseStudentHandlerTests()
    {
        handler = new SuperviseStudentHandler(superviseRepositoryMock.Object, userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldSuperviseStudent()
    {
        // Arrange
        var command = new SuperviseStudentCommand("DoctorId", "StudentId");

        var student = new ApplicationUser("test@test.com", "test", "test", "123456789", "test");
        student.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "Student" } });

        var teacher = new ApplicationUser("test1@test.com", "test1", "test1", "987654321", "test1");
        teacher.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "Dentist_Teacher_Examiner" } });

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.StudentId))
            .ReturnsAsync(student);

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.DoctorId))
            .ReturnsAsync(teacher);

        superviseRepositoryMock.Setup(repo => repo.CheckStudentAlreadyUnderDoctorSupervison(command.StudentId, command.DoctorId))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        superviseRepositoryMock.Verify(repo => repo.AddSupervise(It.IsAny<Supervise>()), Times.Once);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.StudentId), Times.Once);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.DoctorId), Times.Once);
        superviseRepositoryMock.Verify(repo => repo.CheckStudentAlreadyUnderDoctorSupervison(command.StudentId, command.DoctorId), Times.Once);
    }

    [Fact]
    public async Task Handle_StudentDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        var command = new SuperviseStudentCommand("nonexistentStudentId", "doctorId");

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.StudentId))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Student Not Found.", result.ErrorMessage);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.StudentId), Times.Once);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.DoctorId), Times.Never);
        superviseRepositoryMock.Verify(repo => repo.CheckStudentAlreadyUnderDoctorSupervison(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        superviseRepositoryMock.Verify(repo => repo.AddSupervise(It.IsAny<Supervise>()), Times.Never);
    }

    [Fact]
    public async Task Handle_UserIsNotAStudent_ShouldReturnFailure()
    {
        // Arrange
        var command = new SuperviseStudentCommand("DoctorId", "StudentId");

        var user = new ApplicationUser("test@test.com", "test", "test", "123456789", "test");
        // The user is not a student (no "Student" role).
        user.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "AnotherRole" } });

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.StudentId))
            .ReturnsAsync(user);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("User is not a student.", result.ErrorMessage);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.StudentId), Times.Once);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.DoctorId), Times.Never);
        superviseRepositoryMock.Verify(repo => repo.CheckStudentAlreadyUnderDoctorSupervison(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        superviseRepositoryMock.Verify(repo => repo.AddSupervise(It.IsAny<Supervise>()), Times.Never);
    }

    [Fact]
    public async Task Handle_DoctorDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        var command = new SuperviseStudentCommand("studentId", "nonexistentDoctorId");


        var student = new ApplicationUser("test@test.com", "test", "test", "123456789", "test");
        student.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "Student" } });

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.StudentId))
            .ReturnsAsync(student);

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.DoctorId))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Doctor Not Found.", result.ErrorMessage);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.StudentId), Times.Once);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.DoctorId), Times.Once);
        superviseRepositoryMock.Verify(repo => repo.CheckStudentAlreadyUnderDoctorSupervison(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        superviseRepositoryMock.Verify(repo => repo.AddSupervise(It.IsAny<Supervise>()), Times.Never);
    }

    [Fact]
    public async Task Handle_UserIsNotADoctor_ShouldReturnFailure()
    {
        // Arrange
        var command = new SuperviseStudentCommand("DoctorId", "StudentId");

        var student = new ApplicationUser("test@test.com", "test", "test", "123456789", "test");
        student.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "Student" } });

        var user = new ApplicationUser("test1@test.com", "test1", "test1", "987654321", "test1");

        user.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "AnotherRole" } });

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.StudentId))
            .ReturnsAsync(student);

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.DoctorId))
            .ReturnsAsync(user);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("User is not a Doctor.", result.ErrorMessage);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.StudentId), Times.Once);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.DoctorId), Times.Once);
        superviseRepositoryMock.Verify(repo => repo.CheckStudentAlreadyUnderDoctorSupervison(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        superviseRepositoryMock.Verify(repo => repo.AddSupervise(It.IsAny<Supervise>()), Times.Never);
    }

    [Fact]
    public async Task Handle_StudentAlreadyUnderSupervision_ShouldReturnFailure()
    {
        // Arrange
        var command = new SuperviseStudentCommand("DoctorId", "StudentId");

        var student = new ApplicationUser("test@test.com", "test", "test", "123456789", "test");
        student.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "Student" } });

        var teacher = new ApplicationUser("test1@test.com", "test1", "test1", "987654321", "test1");
        teacher.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "Dentist_Teacher_Examiner" } });

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.StudentId))
            .ReturnsAsync(student);

        userRepositoryMock.Setup(repo => repo.GetApplicationUserWithRolesById(command.DoctorId))
            .ReturnsAsync(teacher);

        superviseRepositoryMock.Setup(repo => repo.CheckStudentAlreadyUnderDoctorSupervison(command.StudentId, command.DoctorId))
            .ReturnsAsync(true);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Student is already under supervision.", result.ErrorMessage);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.StudentId), Times.Once);
        userRepositoryMock.Verify(repo => repo.GetApplicationUserWithRolesById(command.DoctorId), Times.Once);
        superviseRepositoryMock.Verify(repo => repo.CheckStudentAlreadyUnderDoctorSupervison(command.StudentId, command.DoctorId), Times.Once);
        superviseRepositoryMock.Verify(repo => repo.AddSupervise(It.IsAny<Supervise>()), Times.Never);
    }
}
