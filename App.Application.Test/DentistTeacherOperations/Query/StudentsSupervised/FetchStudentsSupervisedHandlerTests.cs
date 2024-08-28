using App.Application.DentistTeacherOperations.Query.StudentsSupervised;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MockQueryable.EntityFrameworkCore;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Query.StudentsSupervised;

public class FetchStudentsSupervisedHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnAllStudentsSupervised()
    {
        // Arrange
        var query = new FetchStudentsSupervisedQuery("doctorId", null, null, 0, 10);

        var students = new List<StudentResponseDto>
        {
            new() { Id = "studentId1", UserName = "Student1" },
            new() { Id = "studentId2", UserName = "Student2" }
        }.AsQueryable();

        var mockSet = students.BuildMock();
        superviseRepositoryMock.Setup(repo => repo.GetAllStudentsUnderSupervisionByDoctorId(It.IsAny<string>()))
            .Returns(mockSet);

        var handler = new FetchStudentsSupervisedHandler(superviseRepositoryMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.ResultValue.TotalStudents);
        Assert.Equal(2, result.ResultValue.Students.Count());
        superviseRepositoryMock.Verify(repo => repo.GetAllStudentsUnderSupervisionByDoctorId(query.DoctorId),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidDoctorId_ShouldReturnEmptyList()
    {
        // Arrange
        var query = new FetchStudentsSupervisedQuery("invalidDoctorId", null, null, 0, 10);

        var students = new List<StudentResponseDto>().AsQueryable();

        var mockSet = students.BuildMock();

        superviseRepositoryMock.Setup(repo => repo.GetAllStudentsUnderSupervisionByDoctorId(query.DoctorId))
            .Returns(mockSet);

        var handler = new FetchStudentsSupervisedHandler(superviseRepositoryMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.ResultValue.Students);
        Assert.Equal(0, result.ResultValue.TotalStudents);
    }
}