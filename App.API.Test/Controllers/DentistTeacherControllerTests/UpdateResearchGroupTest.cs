using App.Application.Core;
using App.Application.DentistTeacherOperations.Command.UpdateResearchGroup;
using App.Domain.DTOs.ResearchGroupDtos.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class UpdateResearchGroupTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public UpdateResearchGroupTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task UpdateResearchGroup_Returns_OkResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var expectedGroup = new CreateUpdateResearchGroupRequestDto
        {
            GroupName = "Group 1",
            Description = "Description 1",
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateResearchGroupCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.UpdateResearchGroup(Guid.NewGuid(), expectedGroup);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateResearchGroup_Returns_BadRequestResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var expectedGroup = new CreateUpdateResearchGroupRequestDto
        {
            GroupName = "Group 1",
            Description = "Description 1",
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<UpdateResearchGroupCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.UpdateResearchGroup(Guid.NewGuid(), expectedGroup);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
