using App.Application.AccountOperations.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Test.Controllers.AccountControllerTests;

public class ResetPasswordGetRequestTest
{
    private readonly TestableAccountController _accountController;

    public ResetPasswordGetRequestTest()
    {
        _accountController = new TestableAccountController();
    }

    [Fact]
    public void ResetPassword_ReturnsOkResultWithResetPasswordDto()
    {
        // Arrange
        const string expectedToken = "testToken";
        const string expectedEmail = "test@example.com";

        // Act
        var result = _accountController.ResetPassword(expectedToken, expectedEmail);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var resetPasswordDto = Assert.IsType<ResetPasswordDto>(okResult.Value);
        Assert.Equal(expectedToken, resetPasswordDto.Token);
        Assert.Equal(expectedEmail, resetPasswordDto.Email);
    }
}
