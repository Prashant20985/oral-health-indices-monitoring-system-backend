using App.Application.DentistTeacherOperations.Query.StudentsNotSupervised;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MockQueryable.EntityFrameworkCore;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Query.StudentsNotSupervised;

public class FetchStudentsNotSupervisedHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnAllStudentsNotSupervised()
    {
        // Arrange
        var query = new FetchStudentsNotSupervisedQuery("doctorId", null, null, 0, 10);

        var students = new List<StudentResponseDto>
        {
            new() { Id = "studentId1", UserName = "Student1" },
            new() { Id = "studentId2", UserName = "Student2" }
        }.AsQueryable();

        var mockSet = students.BuildMock();
        superviseRepositoryMock.Setup(repo => repo.GetAllStudentsNotUnderSupervisionByDoctorId(It.IsAny<string>()))
            .Returns(mockSet);

        var handler = new FetchStudentsNotSupervisedHandler(superviseRepositoryMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.ResultValue.TotalStudents);
        Assert.Equal(2, result.ResultValue.Students.Count());
        superviseRepositoryMock.Verify(repo => repo.GetAllStudentsNotUnderSupervisionByDoctorId(query.DoctorId),
            Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidDoctorId_ShouldReturnEmptyList()
    {
        // Arrange
        var query = new FetchStudentsNotSupervisedQuery("invalidDoctorId", null, null, 0, 10);

        var students = new List<StudentResponseDto>().AsQueryable();

        var mockSet = students.BuildMock();
        superviseRepositoryMock.Setup(repo => repo.GetAllStudentsNotUnderSupervisionByDoctorId(query.DoctorId))
            .Returns(mockSet);

        var handler = new FetchStudentsNotSupervisedHandler(superviseRepositoryMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.ResultValue.Students);
        Assert.Equal(0, result.ResultValue.TotalStudents);
        superviseRepositoryMock.Verify(repo => repo.GetAllStudentsNotUnderSupervisionByDoctorId(query.DoctorId),
            Times.Once);
    }
}