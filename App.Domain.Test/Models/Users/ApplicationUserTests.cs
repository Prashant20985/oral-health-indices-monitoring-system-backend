using App.Domain.DTOs.ApplicationUserDtos.Request;
using App.Domain.Models.Users;

namespace App.Domain.Test.Models.Users;

public class ApplicationUserTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var email = "test@example.com";
        var firstName = "John";
        var lastName = "Doe";
        var phoneNumber = "1234567890";
        var guestUserComment = "Guest user comment";

        // Act
        var user = new ApplicationUser(email, firstName, lastName, phoneNumber, guestUserComment);

        // Assert
        Assert.Equal("test", user.UserName);
        Assert.Equal(email, user.Email);
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
        Assert.Equal(phoneNumber, user.PhoneNumber);
        Assert.Equal(guestUserComment, user.GuestUserComment);
        Assert.True(user.IsAccountActive);
        Assert.Equal(DateTime.UtcNow.Date, user.CreatedAt.Date);
        Assert.Null(user.DeletedAt);
        Assert.Null(user.DeleteUserComment);
    }

    [Fact]
    public void ActivationStatusToggle_ShouldToggleIsAccountActive()
    {
        // Arrange
        var user = new ApplicationUser("test@example.com", "John", "Doe", "1234567890", "Guest user comment");

        // Act
        user.ActivationStatusToggle();

        // Assert
        Assert.False(user.IsAccountActive);

        // Act
        user.ActivationStatusToggle();

        // Assert
        Assert.True(user.IsAccountActive);
    }

    [Fact]
    public void DeleteUser_ShouldSetDeletedAtAndDeleteUserComment()
    {
        // Arrange
        var user = new ApplicationUser("test@example.com", "John", "Doe", "1234567890", "Guest user comment");
        var deleteComment = "User deleted";

        // Act
        user.DeleteUser(deleteComment);

        // Assert
        Assert.Equal(DateTime.UtcNow.Date, user.DeletedAt?.Date);
        Assert.Equal(deleteComment, user.DeleteUserComment);
    }

    [Fact]
    public void UpdateUser_ShouldUpdateProperties()
    {
        // Arrange
        var user = new ApplicationUser("test@example.com", "John", "Doe", "1234567890", "Guest user comment");
        var updateDto = new UpdateApplicationUserRequestDto
        {
            FirstName = "Jane",
            LastName = "Smith",
            PhoneNumber = "0987654321",
            GuestUserComment = "Updated guest user comment"
        };

        // Act
        user.UpdateUser(updateDto);

        // Assert
        Assert.Equal(updateDto.FirstName, user.FirstName);
        Assert.Equal(updateDto.LastName, user.LastName);
        Assert.Equal(updateDto.PhoneNumber, user.PhoneNumber);
        Assert.Equal(updateDto.GuestUserComment, user.GuestUserComment);
    }
}
