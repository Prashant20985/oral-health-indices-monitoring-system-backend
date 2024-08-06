using App.Application.Core;
using App.Application.DentistTeacherOperations.Query.StudentsNotInGroup;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.StudentGroupDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class GetStudentsNoInGroupTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetStudentsNoInGroupTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetStudentsNotInGroup_Returns_OkResult()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var expectedStudents = new List<StudentResponseDto>
        {
            new()
            {
                Id = "1",
                FirstName = "John",
                LastName = "Doe",
                UserName = "john.doe",
                Email = "john.doe@example.com",
                Groups = new List<string> { "Group1" }
            },
            new()
            {
                Id = "2",
                FirstName = "Jane",
                LastName = "Doe",
                UserName = "jane.doe",
                Email = "jane.doe@example.com",
                Groups = new List<string> { "Group2" }
            }
        };

        var studentsNotInGroup = new PaginatedStudentResponseDto
        {
            Students = expectedStudents,
            TotalStudents = expectedStudents.Count
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchStudentsNotInGroupListQuery>(), default))
            .ReturnsAsync(OperationResult<PaginatedStudentResponseDto>.Success(studentsNotInGroup));

        // Act
        var result = await _dentistTeacherController.GetStudentsNotInGroup(groupId, null, null);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PaginatedStudentResponseDto>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedStudents = Assert.IsAssignableFrom<PaginatedStudentResponseDto>(okObjectResult.Value);
        Assert.Equal(expectedStudents, returnedStudents.Students);

        _mediatorMock.Verify(x => x.Send(It.Is<FetchStudentsNotInGroupListQuery>(q => q.GroupId == groupId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetStudentsNotInGroup_WhenFetchFails_Returns_BadRequestResult()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchStudentsNotInGroupListQuery>(), default))
            .ReturnsAsync(OperationResult<PaginatedStudentResponseDto>.Failure("Failed to fetch students"));

        // Act
        var result = await _dentistTeacherController.GetStudentsNotInGroup(groupId, null, null);

        // Assert
        var badRequestresult = Assert.IsType<ActionResult<PaginatedStudentResponseDto>>(result);
        var error = Assert.IsType<BadRequestObjectResult>(badRequestresult.Result);
        Assert.Equal("Failed to fetch students", error.Value);

        _mediatorMock.Verify(x => x.Send(It.Is<FetchStudentsNotInGroupListQuery>(q => q.GroupId == groupId), It.IsAny<CancellationToken>()), Times.Once);
    }
}

