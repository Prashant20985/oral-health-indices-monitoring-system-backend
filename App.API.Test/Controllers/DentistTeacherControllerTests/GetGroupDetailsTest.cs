using App.Application.Core;
using App.Application.DentistTeacherOperations.Query.StudentGroupDetails;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class GetGroupDetailsTest
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TestableDentistTeacherController _dentistTeacherController;

    public GetGroupDetailsTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetGroupDetails_Returns_OkResult()
    {
        // Arrange
        var teacher = new ApplicationUser("teacher@test.com", "john", "doe", "1234567890", "test");
        var student = new ApplicationUser("student@test.com", "jane", "doe", "987654321", "test123");

        var group = new Group(teacher.Id, "test");
        var studentGroup = new StudentGroup(group.Id, student.Id);
        group.StudentGroups.Add(studentGroup);

        var studentResponseDto = new StudentGroupResponseDto
        {
            Id = group.Id,
            GroupName = group.GroupName,
            Students = new List<StudentResponseDto>
            {
                new StudentResponseDto
                {
                    Id = student.Id,
                    Email = student.Email,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                }
            }
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchStudentGroupQuery>(), default))
            .ReturnsAsync(OperationResult<StudentGroupResponseDto>.Success(studentResponseDto));

        // Act
        var result = await _dentistTeacherController.GetGroupDetails(group.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<StudentGroupResponseDto>(okResult.Value);
        Assert.Equal(group.Id, model.Id);
        Assert.Equal(group.GroupName, model.GroupName);
        Assert.Equal(student.Id, model.Students.First().Id);
    }

    [Fact]
    public async Task GetGroupDetails_WithInvaliData_ReturnBadRequest()
    {
        // Arrange
        var group = new Group(Guid.NewGuid().ToString(), "test");

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchStudentGroupQuery>(), default))
            .ReturnsAsync(OperationResult<StudentGroupResponseDto>.Failure("Group not found"));

        // Act
        var result = await _dentistTeacherController.GetGroupDetails(Guid.NewGuid());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Group not found", badRequestResult.Value);
    }
}
