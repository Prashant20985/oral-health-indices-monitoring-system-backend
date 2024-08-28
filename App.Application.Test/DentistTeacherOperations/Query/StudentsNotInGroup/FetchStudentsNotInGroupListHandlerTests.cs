using App.Application.DentistTeacherOperations.Query.StudentsNotInGroup;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MockQueryable.EntityFrameworkCore;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Query.StudentsNotInGroup;

public class FetchStudentsNotInGroupListHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnListOfStudentDtos()
    {
        // Arrange

        var studentNotInGroupIQueryable = new List<StudentResponseDto>
        {
            new() { Id = "studentId1", UserName = "Student1" },
            new() { Id = "studentId2", UserName = "Student2" }
        }.AsQueryable().BuildMock();

        groupRepositoryMock.Setup(repo => repo.GetAllStudentsNotInGroup(It.IsAny<Guid>()))
            .Returns(studentNotInGroupIQueryable);

        var handler = new FetchStudentsNotInGroupListHandler(groupRepositoryMock.Object);

        var query = new FetchStudentsNotInGroupListQuery(Guid.NewGuid(), null, null, 0, 10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.ResultValue.TotalStudents);
        Assert.Equal("Student1", result.ResultValue.Students[0].UserName);
        Assert.Equal("Student2", result.ResultValue.Students[1].UserName);
        groupRepositoryMock.Verify(repo => repo.GetAllStudentsNotInGroup(query.GroupId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNoStudentsFound_ShouldReturnFailureResult()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetAllStudentsNotInGroup(It.IsAny<Guid>()))
            .Returns(new List<StudentResponseDto>().AsQueryable().BuildMock());

        var handler = new FetchStudentsNotInGroupListHandler(groupRepositoryMock.Object);

        var query = new FetchStudentsNotInGroupListQuery(Guid.NewGuid(), null, null, 0, 1);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result.ResultValue.Students);
    }
}