﻿using App.Application.DentistTeacherOperations.Query.StudentsNotInGroup;
using App.Domain.DTOs;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Query.StudentsNotInGroup;

public class FetchStudentsNotInGroupListHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnListOfStudentDtos()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetAllStudentsNotInGroup(It.IsAny<Guid>()))
            .ReturnsAsync(new List<StudentDto>
            {
                new StudentDto { Id = "studentId1", UserName = "Student1" },
                new StudentDto { Id = "studentId2", UserName = "Student2" }
            });

        var handler = new FetchStudentsNotInGroupListHandler(groupRepositoryMock.Object);

        var query = new FetchStudentsNotInGroupListQuery(Guid.NewGuid());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.ResultValue.Count);
        Assert.Equal("Student1", result.ResultValue[0].UserName);
        Assert.Equal("Student2", result.ResultValue[1].UserName);
        groupRepositoryMock.Verify(repo => repo.GetAllStudentsNotInGroup(query.GroupId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNoStudentsFound_ShouldReturnFailureResult()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetAllStudentsNotInGroup(It.IsAny<Guid>()))
            .ReturnsAsync(new List<StudentDto>());

        var handler = new FetchStudentsNotInGroupListHandler(groupRepositoryMock.Object);

        var query = new FetchStudentsNotInGroupListQuery(Guid.NewGuid());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Students not found", result.ErrorMessage);
        groupRepositoryMock.Verify(repo => repo.GetAllStudentsNotInGroup(query.GroupId), Times.Once);
    }
}
