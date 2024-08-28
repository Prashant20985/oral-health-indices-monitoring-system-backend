using App.Application.DentistTeacherOperations.Command.UnsuperviseStudent;
using App.Domain.Models.Users;
using MediatR;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.UnsuperviseStudent;

public class UnsuperviseStudentHandlerTests : TestHelper
{
    private readonly UnsuperviseStudentHandler handler;

    public UnsuperviseStudentHandlerTests()
    {
        handler = new UnsuperviseStudentHandler(superviseRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldRemoveSupervision()
    {
        // Arrange
        var command = new UnsuperviseStudentCommand("DoctorId", "StudentId");

        var supervise = new Supervise(command.DoctorId, command.StudentId);

        superviseRepositoryMock
            .Setup(repo => repo.GetSuperviseByDoctorIdAndStudentId(command.DoctorId, command.StudentId))
            .ReturnsAsync(supervise);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        superviseRepositoryMock.Verify(
            repo => repo.GetSuperviseByDoctorIdAndStudentId(command.DoctorId, command.StudentId), Times.Once);
        superviseRepositoryMock.Verify(repo => repo.RemoveSupervise(supervise), Times.Once);
    }

    [Fact]
    public async Task Handle_StudentNotInSupervision_ShouldReturnFailure()
    {
        // Arrange
        var command = new UnsuperviseStudentCommand("DoctorId", "StudentId");

        superviseRepositoryMock
            .Setup(repo => repo.GetSuperviseByDoctorIdAndStudentId(command.DoctorId, command.StudentId))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Student not in supervison", result.ErrorMessage);
        superviseRepositoryMock.Verify(
            repo => repo.GetSuperviseByDoctorIdAndStudentId(command.DoctorId, command.StudentId), Times.Once);
        superviseRepositoryMock.Verify(repo => repo.RemoveSupervise(It.IsAny<Supervise>()), Times.Never);
    }
}