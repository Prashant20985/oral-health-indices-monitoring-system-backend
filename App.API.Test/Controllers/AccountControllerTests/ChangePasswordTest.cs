using App.Application.AccountOperations.Command.ChangePassword;
using App.Application.AccountOperations.DTOs.Request;
using App.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AccountControllerTests;

public class ChangePasswordTest
{
    private readonly TestableAccountController _accountController;
    private readonly Mock<IMediator> _mediatorMock;

    public ChangePasswordTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _accountController = new TestableAccountController();
        _accountController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task ChangePassword_Returns_OkResult()
    {
        // Arrange
        var changePasswordDto = new ChangePasswordDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _accountController.ChangePassword(changePasswordDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(Unit.Value, okResult.Value);
    }

    [Fact]
    public async Task ChangePassword_Returns_BadRequest()
    {
        // Arrange
        var changePasswordDto = new ChangePasswordDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Invalid Email or Username"));

        // Act
        var result = await _accountController.ChangePassword(changePasswordDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid Email or Username", badRequestResult.Value);
    }

    [Fact]
    public async Task ChangePassword_Same_CuurentAndNewPassword_Returns_BadRequest()
    {
        // Arrange
        var changePasswordDto = new ChangePasswordDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("New password cannot be the same as the old password"));

        // Act
        var result = await _accountController.ChangePassword(changePasswordDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("New password cannot be the same as the old password", badRequestResult.Value);
    }
}
