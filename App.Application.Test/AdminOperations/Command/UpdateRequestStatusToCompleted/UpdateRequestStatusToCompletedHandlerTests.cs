using App.Application.AdminOperations.Command.UpdateRequestStatusToCompleted;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.AdminOperations.Command.UpdateRequestStatusToCompleted;

public class UpdateRequestStatusToCompletedHandlerTests : TestHelper
{
    private readonly UpdateRequestStatusToCompletedCommand command;
    private readonly UpdateRequestStatusToCompletedHandler handler;

    public UpdateRequestStatusToCompletedHandlerTests()
    {
        command = new UpdateRequestStatusToCompletedCommand(Guid.NewGuid(), "Test comment");
        handler = new UpdateRequestStatusToCompletedHandler(userRequestRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateRequestStatusToCompleted_Success_ReturnsSuccessResult()
    {
        // Arrange
        var userRequest = new UserRequest("test", "test", "test");

        userRequestRepositoryMock.Setup(u => u.GetUserRequestById(command.UserRequestId))
            .ReturnsAsync(userRequest);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Domain.Models.Enums.RequestStatus.Completed, userRequest.RequestStatus);
        Assert.Equal("Test comment", userRequest.AdminComment);
    }

    [Fact]
    public async Task UpdateRequestStatusToCompleted_UserRequestNotFound_ReturnsFailureResult()
    {
        // Arrange
        userRequestRepositoryMock.Setup(u => u.GetUserRequestById(command.UserRequestId))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("User request not found", result.ErrorMessage);
    }
}
