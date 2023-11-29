using App.Application.Behavior;
using Moq;
using static App.Application.Test.Behavior.SampleData;

namespace App.Application.Test.Behavior;

public class UnitOfWorkBehaviorPipelineTests : TestHelper
{

    [Fact]
    public async Task Handle_UnitOfWorkAttribute_AppliesTransaction()
    {
        // Arrange
        var behavior = new UnitOfWorkBehaviorPipeline<SampleRequestWithUnitOfWorkAttribute, SampleResponse>(userContextUnitOfWork.Object);

        var request = new SampleRequestWithUnitOfWorkAttribute();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await behavior.Handle(request, NextHandler, cancellationToken);

        // Assert
        userContextUnitOfWork.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task Handle_NoUnitOfWorkAttribute_NoTransactionApplied()
    {
        // Arrange
        var behavior = new UnitOfWorkBehaviorPipeline<SampleRequest, SampleResponse>(userContextUnitOfWork.Object);

        var request = new SampleRequest();
        var cancellationToken = CancellationToken.None;

        // Act
        var response = await behavior.Handle(request, NextHandler, cancellationToken);

        // Assert
        userContextUnitOfWork.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Never);
        Assert.NotNull(response);
    }

    private Task<SampleResponse> NextHandler()
    {
        return Task.FromResult(new SampleResponse());
    }
}
