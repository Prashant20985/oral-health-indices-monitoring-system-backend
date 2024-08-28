using App.Application.Core;
using App.Application.DentistTeacherOperations.Query.ResearchGroups;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class GetAllResearchGroupsTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetAllResearchGroupsTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAllResearchGroups_Returns_OkResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var expectedGroups = new List<ResearchGroupResponseDto>
        {
            new ResearchGroupResponseDto
            {
                GroupName = "Group 1",
                Description = "Description 1",
                CreatedBy = "Dentist_Teacher_Researcher",
            }
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchResearchGroupsQuery>(), default))
            .ReturnsAsync(OperationResult<List<ResearchGroupResponseDto>>.Success(expectedGroups));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.GetAllResearchGroups("Group 1");

        // Assert
        Assert.IsType<ActionResult<List<ResearchGroupResponseDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedGroups = Assert.IsAssignableFrom<List<ResearchGroupResponseDto>>(okResult.Value);
        Assert.Equal(expectedGroups, returnedGroups);
    }

    [Fact]
    public async Task GetAllResearchGroups_WhenFetchFails_Returns_BadRequest()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchResearchGroupsQuery>(), default))
            .ReturnsAsync(OperationResult<List<ResearchGroupResponseDto>>.Failure("Fetch failed"));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.GetAllResearchGroups("Group 1");

        // Assert
        Assert.IsType<ActionResult<List<ResearchGroupResponseDto>>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Fetch failed", badRequestResult.Value);
    }
}
