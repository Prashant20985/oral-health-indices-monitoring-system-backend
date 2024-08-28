using System.Security.Claims;
using App.Application.Core;
using App.Application.DentistTeacherOperations.Query.ResearchGroupDetailsById;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class GetResearchGroupDetailsByIdTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetResearchGroupDetailsByIdTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetResearchGroupDetailsById_Returns_OkResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var researchGroup = new ResearchGroupResponseDto
        {
            GroupName = "Test Group",
            Description = "Test Description"
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchResearchGroupDetailsByIdQuery>(), default))
            .ReturnsAsync(OperationResult<ResearchGroupResponseDto>.Success(researchGroup));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.GetResearchGroupDetailsById(researchGroup.Id);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, ((OkObjectResult)result).StatusCode);
        Assert.Equal(researchGroup, ((OkObjectResult)result).Value);
    }

    [Fact]
    public async Task GetResearchGroupDetailsById_Returns_BadRequestResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchResearchGroupDetailsByIdQuery>(), default))
            .ReturnsAsync(OperationResult<ResearchGroupResponseDto>.Failure("test"));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.GetResearchGroupDetailsById(Guid.NewGuid());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, ((BadRequestObjectResult)result).StatusCode);
    }
}