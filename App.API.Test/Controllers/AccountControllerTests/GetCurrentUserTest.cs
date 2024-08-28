using System.Security.Claims;
using App.Application.AccountOperations.DTOs.Response;
using App.Application.AccountOperations.Query.CurrentUser;
using App.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AccountControllerTests;

public class GetCurrentUserTest
{
    private readonly TestableAccountController _accountController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetCurrentUserTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _accountController = new TestableAccountController();
        _accountController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetCurrentUser_Returns_UserLoginResponseDto_OkResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "test")
        }));

        var expectedResult = new UserLoginResponseDto("test User", "test", "test@test.com", "Admin", "token");

        _mediatorMock.Setup(m => m.Send(It.IsAny<CurrentUserQuery>(), default))
            .ReturnsAsync(OperationResult<UserLoginResponseDto>.Success(expectedResult));

        _accountController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        var result = await _accountController.GetCurrentUser();

        // Assert
        Assert.IsType<ActionResult<UserLoginResponseDto>>(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<UserLoginResponseDto>(okResult.Value);
        Assert.Equal(expectedResult, model);
    }

    [Fact]
    public async Task GetCurrentUser_NoUserInClaims_ReturnsNotFound()
    {
        // Arrange
        _accountController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await _accountController.GetCurrentUser();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}