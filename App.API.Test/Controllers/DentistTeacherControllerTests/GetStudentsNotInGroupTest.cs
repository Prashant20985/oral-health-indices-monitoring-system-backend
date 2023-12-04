using App.Application.Core;
using App.Application.DentistTeacherOperations.Query.StudentsNotInGroup;
using App.Domain.DTOs;
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
        var expectedStudents = new List<StudentDto>
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

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchStudentsNotInGroupListQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentDto>>.Success(expectedStudents));

        // Act
        var result = await _dentistTeacherController.GetStudentsNotInGroup(groupId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedStudents = Assert.IsAssignableFrom<List<StudentDto>>(okResult.Value);
        Assert.Equal(expectedStudents, returnedStudents);

        _mediatorMock.Verify(x => x.Send(It.Is<FetchStudentsNotInGroupListQuery>(q => q.GroupId == groupId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetStudentsNotInGroup_WhenFetchFails_Returns_BadRequestResult()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchStudentsNotInGroupListQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentDto>>.Failure("Failed to fetch students"));

        // Act
        var result = await _dentistTeacherController.GetStudentsNotInGroup(groupId);

        // Assert
        var badRequestresult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Failed to fetch students", badRequestresult.Value);

        _mediatorMock.Verify(x => x.Send(It.Is<FetchStudentsNotInGroupListQuery>(q => q.GroupId == groupId), It.IsAny<CancellationToken>()), Times.Once);
    }
}

