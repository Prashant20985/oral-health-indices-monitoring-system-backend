using App.Application.UserRequestOperations.Command.DeleteRequest;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.UserRequestOperations.Command.DeleteRequest;

public class DeleteRequestHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_WithValidCommand_ShouldDeleteUserRequestSuccessfully()
    {
        // Arrange
        var userRequest = new UserRequest("createdById", "Test Request", "Test Request Description");
        var handler = new DeleteRequestHandler(userRequestRepositoryMock.Object);

        var command = new DeleteRequestCommand(userRequest.Id);

        userRequestRepositoryMock.Setup(repo => repo.GetUserRequestById(userRequest.Id))
            .ReturnsAsync(userRequest);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        userRequestRepositoryMock.Verify(repo => repo.DeleteRequest(It.IsAny<UserRequest>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidCommand_ShouldReturnFailureResult()
    {
        // Arrange
        var handler = new DeleteRequestHandler(userRequestRepositoryMock.Object);
        var command = new DeleteRequestCommand(Guid.NewGuid());

        userRequestRepositoryMock.Setup(repo => repo.GetUserRequestById(command.UserRequestId)).ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Request not found", result.ErrorMessage);
        userRequestRepositoryMock.Verify(repo => repo.DeleteRequest(It.IsAny<UserRequest>()), Times.Never);
    }
}
