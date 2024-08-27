using App.Application.Core;
using App.Application.DentistTeacherOperations.Query.Groups;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.StudentGroupDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class GetAllGroupsTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetAllGroupsTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAllGroups_Returns_OkResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var expectedGroups = new List<StudentGroupResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Students = new List<StudentResponseDto>
                {
                    new()
                    {
                        Id = "1",
                        FirstName = "John",
                        LastName = "Doe",
                        UserName = "john.doe",
                        Email = ""
                    }
                }
            }
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchGroupsQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentGroupResponseDto>>.Success(expectedGroups));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.GetAllGroups();

        // Assert
        Assert.IsType<ActionResult<List<StudentGroupResponseDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedGroups = Assert.IsAssignableFrom<List<StudentGroupResponseDto>>(okResult.Value);
        Assert.Equal(expectedGroups, returnedGroups);

        _mediatorMock.Verify(x => x.Send(It.IsAny<FetchGroupsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllGroups_WhenFetchFails_Returns_BadRequestResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
        new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchGroupsQuery>(), default))
            .ReturnsAsync(OperationResult<List<StudentGroupResponseDto>>.Failure("Failed to fetch groups"));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.GetAllGroups();

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<StudentGroupResponseDto>>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Failed to fetch groups", badRequestResult.Value);

        _mediatorMock.Verify(x => x.Send(It.IsAny<FetchGroupsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }

}
