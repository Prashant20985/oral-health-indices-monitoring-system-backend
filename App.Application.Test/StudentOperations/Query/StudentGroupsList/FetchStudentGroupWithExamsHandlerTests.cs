using App.Application.StudentOperations.Query.StudentGroupsList;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Models.Users;
using App.Domain.Repository;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Loader;

namespace App.Application.Test.StudentOperations.Query.StudentGroupsList;

public class FetchStudentGroupWithExamsHandlerTests : TestHelper
{
    private readonly FetchStudentGroupWithExamsHandler handler;
    private readonly FetchStudentGroupsWithExamsListQuery query;

    public FetchStudentGroupWithExamsHandlerTests()
    {
        var studentId = "studentId";
        handler = new FetchStudentGroupWithExamsHandler(groupRepositoryMock.Object);
        query = new FetchStudentGroupsWithExamsListQuery(studentId);
    }

    [Fact]
    public async Task Handle_WhenExamExists_ShouldReturnListOfGroupsWithExamsForStudent()
    {
        // Arrange
        var group = new Group("teacherId", "test");
        group.StudentGroups.Add(new StudentGroup(group.Id, "studentId"));

        var studentGroupWithExams = new List<StudentGroupWithExamsListResponseDto>
        {
            new StudentGroupWithExamsListResponseDto
            {
                Id = Guid.NewGuid(),
                GroupName = "test",
                Teacher = "teacher",
                Exams = new List<ExamDto>
                {
                    new ExamDto
                    {
                        Id = Guid.NewGuid(),
                        DateOfExamination = DateTime.Now
                    }
                }
            }
        };

        groupRepositoryMock.Setup(x => x.GetAllGroupsByStudentIdWithExamsList(It.IsAny<string>()))
            .ReturnsAsync(studentGroupWithExams);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(studentGroupWithExams.Count, result.ResultValue.Count);
        Assert.Equal(studentGroupWithExams[0].Id, result.ResultValue[0].Id);
        Assert.Equal(studentGroupWithExams[0].GroupName, result.ResultValue[0].GroupName);
        Assert.Equal(studentGroupWithExams[0].Exams.Count, result.ResultValue[0].Exams.Count);
        Assert.Equal(studentGroupWithExams[0].Exams[0].Id, result.ResultValue[0].Exams[0].Id);
    }

    [Fact]
    public async Task Handle_WhenExamDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        groupRepositoryMock.Setup(x => x.GetAllGroupsByStudentIdWithExamsList(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Null(result.ResultValue);
        Assert.Equal("No groups found for the student.", result.ErrorMessage);
    }
}
