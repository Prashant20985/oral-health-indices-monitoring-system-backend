using App.Application.AdminOperations.Command.UpdateRequestStatusToInProgress;
using App.Domain.Models.Enums;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.AdminOperations.Command.UpdateRequestStatusToInProgress;

public class UpdateRequestStatusToInProgressHandlerTests : TestHelper
{
    private readonly UpdateRequestStatusToInProgressCommand command;
    private readonly UpdateRequestStatusToInProgressHandler handler;

    public UpdateRequestStatusToInProgressHandlerTests()
    {
        command = new UpdateRequestStatusToInProgressCommand(Guid.NewGuid());
        handler = new UpdateRequestStatusToInProgressHandler(userRequestRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateRequestStatusToInProgress_Success_ReturnsSuccessResult()
    {
        // Arrange
        var userRequest = new UserRequest("test", "test", "test");

        userRequestRepositoryMock.Setup(u => u.GetUserRequestById(command.UserRequestId))
            .ReturnsAsync(userRequest);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(RequestStatus.In_Progress, userRequest.RequestStatus);
    }

    [Fact]
    public async Task UpdateRequestStatusToInProgress_UserRequestNotFound_ReturnsFailureResult()
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