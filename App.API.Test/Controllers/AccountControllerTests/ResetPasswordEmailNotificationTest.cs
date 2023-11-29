using App.Application.AccountOperations.Command.ResetPasswordUrlEmailRequest;
using App.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AccountControllerTests;

public class ResetPasswordEmailNotificationTest
{
    private readonly TestableAccountController _accountController;
    private readonly Mock<IMediator> _mediatorMock;

    public ResetPasswordEmailNotificationTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _accountController = new TestableAccountController();
        _accountController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task ResetPasswordEmailNotification_ValidEmail_ReturnsOkResult()
    {
        // Arrange
        const string validEmail = "test@example.com";

        _mediatorMock.Setup(m => m.Send(It.IsAny<ResetPasswordUrlEmailRequestCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _accountController.ResetPasswordEmailNotification(validEmail);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ResetPasswordEmailNotification_InvalidEmail_ReturnsBadRequest()
    {
        // Arrange
        const string invalidEmail = "invalidemail@test.com"; // An invalid email format
        _mediatorMock.Setup(m => m.Send(It.IsAny<ResetPasswordUrlEmailRequestCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Invalid Email or Username"));

        // Act
        var result = await _accountController.ResetPasswordEmailNotification(invalidEmail);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid Email or Username", badRequestResult.Value);
    }
}

