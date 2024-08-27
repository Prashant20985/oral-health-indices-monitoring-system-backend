using App.Application.AccountOperations.Command.ResetPassword;
using App.Application.AccountOperations.DTOs.Request;
using App.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AccountControllerTests;

public class ResetPasswordPostRequestTest
{
    private readonly TestableAccountController _accountController;
    private readonly Mock<IMediator> _mediatorMock;

    public ResetPasswordPostRequestTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _accountController = new TestableAccountController();
        _accountController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task ResetPassword_ReturnsOkResultWithResetPasswordDto()
    {
        // Arrange
        var resetPasswordDto = new ResetPasswordDto
        {
            Email = "test@test.com",
            Token = "token",
            Password = "Password",
            ConfirmPassword = "Password"
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ResetPasswordCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _accountController.ResetPassword(resetPasswordDto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(Unit.Value, okResult.Value);
    }

    [Theory]
    [InlineData("simplepass", "Password does not meet complexity requirements.")]
    [InlineData("Pass123", "Password does not meet complexity requirements.")]
    [InlineData("ComplexP@ss", "Password does not meet complexity requirements.")]
    public async Task ResetPassword_InvalidPassword_ReturnsFailureResultWithErrorMessage(string invalidPassword,
        string errorMessage)
    {
        // Arrange
        var invalidResetPasswordDto = new ResetPasswordDto
        {
            Token = "testToken",
            Email = "test@example.com",
            Password = invalidPassword,
            ConfirmPassword = invalidPassword
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<ResetPasswordCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure(errorMessage));

        // Act
        var result = await _accountController.ResetPassword(invalidResetPasswordDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(errorMessage, badRequestResult.Value);
    }
}