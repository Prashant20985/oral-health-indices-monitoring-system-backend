using App.Application.AdminOperations.Command.CreateApplicationUser;
using App.Application.Interfaces;
using App.Application.NotificationOperations;
using App.Domain.DTOs;
using App.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace App.Application.Test.AdminOperations.Command.CreateApplicationUser;

public class CreateApplicationUserHandlerTests : TestHelper
{
    private readonly CreateApplicationUserDto createApplicationUserDto;
    private readonly ApplicationUser applicationUser;
    private readonly Mock<IGeneratePassword> generatePasswordMock;
    private readonly CreateApplicationUserCommand createApplicationUserCommand;
    private readonly CreateApplicationUserHandler createApplicationUserHandler;

    public CreateApplicationUserHandlerTests()
    {
        createApplicationUserDto = new CreateApplicationUserDto
        {
            FirstName = "Jhon",
            LastName = "Doe",
            Email = "test@test.com",
            PhoneNumber = "1234567890",
            GuestUserComment = null
        };

        applicationUser = new ApplicationUser(
            email: "test@example.com",
            firstName: "John",
            lastName: "Doe",
            phoneNumber: "12345678",
            guestUserComment: "xyz");

        generatePasswordMock = new Mock<IGeneratePassword>();
        createApplicationUserCommand = new CreateApplicationUserCommand(createApplicationUserDto);
        createApplicationUserHandler = new CreateApplicationUserHandler(userRepositoryMock.Object, generatePasswordMock.Object, mediatorMock.Object);
    }

    [Fact]
    public async Task CreateApplicationUser_Success_ReturnsSuccessResult()
    {
        // Arrange
        var password = "Password";

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(value: null);

        generatePasswordMock.Setup(x => x.GenerateRandomPassword()).Returns(password);

        userRepositoryMock.Setup(u => u.CreateApplicationUserAsync(It.IsAny<ApplicationUser>(), password))
            .ReturnsAsync(IdentityResult.Success);

        userRepositoryMock.Setup(u => u.AddApplicationUserToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act 
        var result = await createApplicationUserHandler.Handle(createApplicationUserCommand, CancellationToken.None);

        // Assert 
        Assert.True(result.IsSuccessful);
        mediatorMock.Verify(m => m.Publish(It.IsAny<EmailNotification>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task CreateApplicationUser_Failure_UserExists_ReturnsFailureResult()
    {
        // Arrange
        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(applicationUser);

        // Act 
        var result = await createApplicationUserHandler.Handle(createApplicationUserCommand, CancellationToken.None);

        // Assert 
        Assert.False(result.IsSuccessful);
        Assert.Equal("Email already taken", result.ErrorMessage);
        mediatorMock.Verify(m => m.Publish(It.IsAny<EmailNotification>(), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task CreateApplicationUser_Failure_ReturnsFailureResult()
    {
        // Arrange

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(value: null);

        generatePasswordMock.Setup(x => x.GenerateRandomPassword()).Returns("Password");

        userRepositoryMock.Setup(u => u.CreateApplicationUserAsync(It.IsAny<ApplicationUser>(), "Password"))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error Creating User" }));

        // Act 
        var result = await createApplicationUserHandler.Handle(createApplicationUserCommand, CancellationToken.None);

        // Assert 
        Assert.False(result.IsSuccessful);
        Assert.NotNull(result.ErrorMessage);
        mediatorMock.Verify(m => m.Publish(It.IsAny<EmailNotification>(), CancellationToken.None), Times.Never);
    }
}

