using App.Application.AccountOperations.Command.Login;
using App.Application.AccountOperations.DTOs.Request;
using App.Application.AccountOperations.DTOs.Response;
using App.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AccountControllerTests;

public class UserLoginTest : TestableAccountController
{
    private readonly TestableAccountController _accountController;
    private readonly Mock<IMediator> _mediatorMock;

    public UserLoginTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _accountController = new TestableAccountController();
        _accountController.ExposeSetMediator(_mediatorMock.Object);
    }


    [Fact]
    public async Task Login_Returns_UserLoginResponseDto_OkResult()
    {
        // Arrange
        var creds = new UserCredentialsDto
        {
            Email = "test",
            Password = "test_pass"
        };

        var expectedResult = new UserLoginResponseDto("test User", "test", "test@test.com", "Admin", "token");
        _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), default))
            .ReturnsAsync(OperationResult<UserLoginResponseDto>.Success(expectedResult));

        // Act
        var result = await _accountController.UserLogin(creds);

        // Assert 
        Assert.IsType<ActionResult<UserLoginResponseDto>>(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsType<UserLoginResponseDto>(okResult.Value);
        Assert.Equal(expectedResult, model);
    }

    [Fact]
    public async Task Login_Returns_BadRequest()
    {
        // Arrange
        var creds = new UserCredentialsDto
        {
            Email = "test",
            Password = "test_pass"
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), default))
            .ReturnsAsync(OperationResult<UserLoginResponseDto>.Failure("Invalid Email or Username"));

        // Act
        var result = await _accountController.UserLogin(creds);

        // Assert 
        Assert.IsType<ActionResult<UserLoginResponseDto>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Invalid Email or Username", badRequestResult.Value);
    }

    [Fact]
    public async Task Login_User_Accout_Deleted_Returns_Unauthorized()
    {
        // Arrange
        var creds = new UserCredentialsDto
        {
            Email = "test",
            Password = "test_pass"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), default))
            .ReturnsAsync(OperationResult<UserLoginResponseDto>.Unauthorized("Your Account has been deleted"));

        // Act
        var result = await _accountController.UserLogin(creds);

        // Assert 
        Assert.IsType<ActionResult<UserLoginResponseDto>>(result);
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
        Assert.Equal("Your Account has been deleted", unauthorizedResult.Value);
    }

    [Fact]
    public async Task Login_User_Accout_Deactivated_Returns_Unauthorized()
    {
        // Arrange
        var creds = new UserCredentialsDto
        {
            Email = "test",
            Password = "test_pass"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<LoginCommand>(), default))
            .ReturnsAsync(OperationResult<UserLoginResponseDto>.Unauthorized("Your Account has been Deactivated"));

        // Act
        var result = await _accountController.UserLogin(creds);

        // Assert 
        Assert.IsType<ActionResult<UserLoginResponseDto>>(result);
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
        Assert.Equal("Your Account has been Deactivated", unauthorizedResult.Value);
    }
}

