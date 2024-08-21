using App.Domain.Models.Enums;
using App.Domain.Models.Users;

namespace App.Domain.Test.Models.Users;

public class UserRequestTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var createdBy = "user123";
        var requestTitle = "Request Title";
        var description = "Request Description";

        // Act
        var userRequest = new UserRequest(createdBy, requestTitle, description);

        // Assert
        Assert.Equal(createdBy, userRequest.CreatedBy);
        Assert.Equal(requestTitle, userRequest.RequestTitle);
        Assert.Equal(description, userRequest.Description);
        Assert.NotEqual(Guid.Empty, userRequest.Id);
        Assert.Equal(DateTime.UtcNow.Date, userRequest.DateSubmitted.Date);
        Assert.Equal(RequestStatus.Submitted, userRequest.RequestStatus);
        Assert.Null(userRequest.AdminComment);
        Assert.Null(userRequest.ApplicationUser);
    }

    [Fact]
    public void UpdateRequestTitleAndDescription_ShouldUpdateProperties()
    {
        // Arrange
        var userRequest = new UserRequest("user123", "Old Title", "Old Description");
        var newTitle = "New Title";
        var newDescription = "New Description";

        // Act
        userRequest.UpdateRequestTitleAndDescription(newTitle, newDescription);

        // Assert
        Assert.Equal(newTitle, userRequest.RequestTitle);
        Assert.Equal(newDescription, userRequest.Description);
    }

    [Fact]
    public void SetRequestToCompleted_ShouldUpdateStatusAndProperties()
    {
        // Arrange
        var userRequest = new UserRequest("user123", "Request Title", "Request Description");
        var adminComment = "Completed by admin";

        // Act
        userRequest.SetRequestToCompleted(adminComment);

        // Assert
        Assert.Equal(RequestStatus.Completed, userRequest.RequestStatus);
        Assert.Equal(DateTime.UtcNow.Date, userRequest.DateCompleted.Date);
        Assert.Equal(adminComment, userRequest.AdminComment);
    }

    [Fact]
    public void SetRequestToInProgress_ShouldUpdateStatus()
    {
        // Arrange
        var userRequest = new UserRequest("user123", "Request Title", "Request Description");

        // Act
        userRequest.SetRequestToInProgress();

        // Assert
        Assert.Equal(RequestStatus.In_Progress, userRequest.RequestStatus);
    }

    [Fact]
    public void AddAdminComment_ShouldUpdateAdminComment()
    {
        // Arrange
        var userRequest = new UserRequest("user123", "Request Title", "Request Description");
        var adminComment = "Admin comment";

        // Act
        userRequest.AddAdminComment(adminComment);

        // Assert
        Assert.Equal(adminComment, userRequest.AdminComment);
    }
}
