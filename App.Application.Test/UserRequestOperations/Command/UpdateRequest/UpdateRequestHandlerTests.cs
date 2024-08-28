using App.Application.UserRequestOperations.Command.UpdateRequest;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.UserRequestOperations.Command.UpdateRequest;

public class UpdateRequestHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_WithValidCommand_ShouldUpdateUserRequestSuccessfully()
    {
        // Arrange
        var userRequest = new UserRequest("JohnDoe", "Test Request", "This is a test request");
        var handler = new UpdateRequestHandler(userRequestRepositoryMock.Object);

        var command = new UpdateRequestCommand(userRequest.Id, "Updated Request Title", "Updated request description.");


        userRequestRepositoryMock.Setup(repo => repo.GetUserRequestById(userRequest.Id)).ReturnsAsync(userRequest);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Updated Request Title", userRequest.RequestTitle);
        Assert.Equal("Updated request description.", userRequest.Description);
    }

    [Fact]
    public async Task Handle_WithInvalidCommand_ShouldReturnFailureResult()
    {
        // Arrange
        var handler = new UpdateRequestHandler(userRequestRepositoryMock.Object);
        var requestId = Guid.NewGuid();
        var command = new UpdateRequestCommand(requestId, "Updated Request Title", "Updated request description.");

        userRequestRepositoryMock.Setup(repo => repo.GetUserRequestById(requestId)).ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("User request not found", result.ErrorMessage);
    }
}