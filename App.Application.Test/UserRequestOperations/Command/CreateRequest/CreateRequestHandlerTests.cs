using App.Application.UserRequestOperations.Command.CreateRequest;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.UserRequestOperations.Command.CreateRequest;

public class CreateRequestHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateUserRequestSuccessfully()
    {
        // Arrange
        var handler = new CreateRequestHandler(userRequestRepositoryMock.Object);

        var command = new CreateRequestCommand("Request Title", "Request Description", "createdById");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        userRequestRepositoryMock.Verify(repo => repo.CreateRequest(It.IsAny<UserRequest>()), Times.Once);
    }
}
