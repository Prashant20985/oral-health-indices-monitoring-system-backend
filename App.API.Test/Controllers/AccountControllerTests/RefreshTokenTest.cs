using App.Application.AccountOperations.Command.RefreshToken;
using App.Application.AccountOperations.DTOs.Response;
using App.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.AccountControllerTests;

public class RefreshTokenTest
{
    private readonly TestableAccountController _accountController;
    private readonly Mock<IMediator> _mediatorMock;

    public RefreshTokenTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _accountController = new TestableAccountController();
        _accountController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task RefreshToken_Returns_UserLoginResponseDto_OkResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "test")
        }));

        var expectedResult = new UserLoginResponseDto("test User", "test", "test@test.com", "Admin", "token");

        _mediatorMock.Setup(m => m.Send(It.IsAny<RefreshTokenCommand>(), default))
            .ReturnsAsync(OperationResult<UserLoginResponseDto>.Success(expectedResult));

        _accountController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _accountController.RefreshToken();

        // Assert
        Assert.IsType<ActionResult<UserLoginResponseDto>>(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<UserLoginResponseDto>(okResult.Value);
        Assert.Equal(expectedResult, model);
    }

    [Fact]
    public async Task RefreshToken_NoUserInClaims_ReturnsNotFound()
    {
        // Arrange
        _accountController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await _accountController.RefreshToken();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}

