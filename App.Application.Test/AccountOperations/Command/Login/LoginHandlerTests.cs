using App.Application.AccountOperations.Command.Login;
using App.Application.AccountOperations.DTOs.Request;
using App.Application.Interfaces;
using App.Domain.Models.Users;
using App.Domain.Repository;
using Moq;

namespace App.Application.Test.AccountOperations.Command.Login;

public class LoginHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidLogin_ReturnsSuccessResultWithUserLoginResponseDto()
    {
        var credentials = new UserCredentialsDto
        {
            Email = "test@example.com",
            Password = "password"
        };

        var userRepositoryMock = new Mock<IUserRepository>();
        var tokenServiceMock = new Mock<ITokenService>();

        var applicationRole = new ApplicationRole { Name = "Admin" };

        var applicationUser = new ApplicationUser(
            email: "test@example.com",
            firstName: "John",
            lastName: "Doe",
            phoneNumber: "12345678",
            guestUserComment: "xyz")
        {
            ApplicationUserRoles = new List<ApplicationUserRole> { new ApplicationUserRole { ApplicationRole = applicationRole } }
        };

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(credentials.Email, CancellationToken.None))
            .ReturnsAsync(applicationUser);

        userRepositoryMock.Setup(u => u.CheckPassword(applicationUser, credentials.Password))
            .ReturnsAsync(true);

        tokenServiceMock.Setup(t => t.CreateToken(applicationUser))
            .ReturnsAsync("token");

        var loginCommand = new LoginCommand(credentials);
        var loginHandler = new LoginHandler(userRepositoryMock.Object, tokenServiceMock.Object);

        // Act
        var result = await loginHandler.Handle(loginCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("John Doe", result.ResultValue.Name);
        Assert.Equal("test@example.com", result.ResultValue.Email);
        Assert.Equal("Admin", result.ResultValue.Role);
        Assert.Equal("token", result.ResultValue.Token);
    }

    [Fact]
    public async Task Handle_InvalidCredentials_FailureResult()
    {
        // Arrange
        var credentials = new UserCredentialsDto
        {
            Email = "test@example.com",
            Password = "wrongpassword"
        };

        var applicationUser = new ApplicationUser("test@example.com",
             "John",
             "Doe",
             "12345678",
             "xyz");

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(credentials.Email, CancellationToken.None))
            .ReturnsAsync(applicationUser);

        userRepositoryMock.Setup(u => u.CheckPassword(applicationUser, credentials.Password))
            .ReturnsAsync(false);

        var loginCommand = new LoginCommand(credentials);
        var loginHandler = new LoginHandler(userRepositoryMock.Object, tokenServiceMock.Object);

        // Act
        var result = await loginHandler.Handle(loginCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Credentials", result.ErrorMessage);
        Assert.Null(result.ResultValue);
    }

    [Fact]
    public async Task Handle_InvalidUsernameOrEmail_FailureResult()
    {
        // Arrange
        var credentials = new UserCredentialsDto
        {
            Email = "test@example.com",
            Password = "wrongpassword"
        };

        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz");

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(credentials.Email, CancellationToken.None))
            .ReturnsAsync(value: null);

        var loginCommand = new LoginCommand(credentials);
        var loginHandler = new LoginHandler(userRepositoryMock.Object, tokenServiceMock.Object);

        // Act
        var result = await loginHandler.Handle(loginCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Email or Username", result.ErrorMessage);
        Assert.Null(result.ResultValue);
    }

    [Fact]
    public async Task Handle_Account_Deactivated_FailureResult()
    {
        // Arrange
        var credentials = new UserCredentialsDto
        {
            Email = "test@example.com",
            Password = "wrongpassword"
        };

        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz");

        user.ActivationStatusToggle();

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(credentials.Email, CancellationToken.None))
            .ReturnsAsync(user);

        var loginCommand = new LoginCommand(credentials);
        var loginHandler = new LoginHandler(userRepositoryMock.Object, tokenServiceMock.Object);

        // Act
        var result = await loginHandler.Handle(loginCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Your Account has been Deactivated", result.ErrorMessage);
        Assert.Null(result.ResultValue);
    }

    [Fact]
    public async Task Handle_Account_Deleted_FailureResult()
    {
        // Arrange
        var credentials = new UserCredentialsDto
        {
            Email = "test@example.com",
            Password = "wrongpassword"
        };

        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz");
        user.DeleteUser("Test Delete");

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(credentials.Email, CancellationToken.None))
            .ReturnsAsync(user);

        var loginCommand = new LoginCommand(credentials);
        var loginHandler = new LoginHandler(userRepositoryMock.Object, tokenServiceMock.Object);

        // Act
        var result = await loginHandler.Handle(loginCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Your Account has been deleted", result.ErrorMessage);
        Assert.Null(result.ResultValue);
    }
}
