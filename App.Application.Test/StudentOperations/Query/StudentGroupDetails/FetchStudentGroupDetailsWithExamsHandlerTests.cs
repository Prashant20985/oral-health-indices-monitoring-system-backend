using App.Application.StudentOperations.Query.StudentGroupDetails;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.StudentOperations.Query.StudentGroupDetails;

public class FetchStudentGroupDetailsWithExamsHandlerTests : TestHelper
{
    private readonly FetchStudentGroupDetailsWithExamsHandler handler;
    private readonly FetchStudentGroupDetailsWithExamsQuery query;

    public FetchStudentGroupDetailsWithExamsHandlerTests()
    {
        var studentId = "studentId";
        var group = new Group("teacherId", "test");
        group.StudentGroups.Add(new StudentGroup(group.Id, "studentId"));
        handler = new FetchStudentGroupDetailsWithExamsHandler(groupRepositoryMock.Object);
        query = new FetchStudentGroupDetailsWithExamsQuery(studentId, group.Id);
    }

    [Fact]
    public async Task Handle_WhenStudentGroupExists_ShouldReturnStudentGroupDetails()
    {
        // Arrange
        var studentGroupWithExams = new StudentGroupWithExamsListResponseDto
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
        };

        groupRepositoryMock.Setup(x => x.GetGroupDetailsWithExamsListByGroupIdAndStudentId(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(studentGroupWithExams);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(studentGroupWithExams.Id, result.ResultValue.Id);
        Assert.Equal(studentGroupWithExams.GroupName, result.ResultValue.GroupName);
        Assert.Equal(studentGroupWithExams.Exams.Count, result.ResultValue.Exams.Count);
        Assert.Equal(studentGroupWithExams.Exams[0].Id, result.ResultValue.Exams[0].Id);
        Assert.Equal(studentGroupWithExams.Exams[0].DateOfExamination, result.ResultValue.Exams[0].DateOfExamination);
    }

    [Fact]
    public async Task Handle_WhenStudentGroupDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        groupRepositoryMock.Setup(x => x.GetGroupDetailsWithExamsListByGroupIdAndStudentId(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Null(result.ResultValue);
        Assert.Equal("Group Not Found", result.ErrorMessage);
    }
}
