using App.Application.Behavior;
using App.Application.Core;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using static App.Application.Test.Behavior.SampleData;

namespace App.Application.Test.Behavior;

public class ValidationBehaviorTests
{
    private readonly SampleRequest request;
    private readonly ValidationBehaviorPipeline<SampleRequest, OperationResult<SampleResponse>> validationBehavior;
    private readonly Mock<IValidator<SampleRequest>> validatorMock;

    public ValidationBehaviorTests()
    {
        validatorMock = new Mock<IValidator<SampleRequest>>();
        validationBehavior =
            new ValidationBehaviorPipeline<SampleRequest, OperationResult<SampleResponse>>(validatorMock.Object);
        request = new SampleRequest();
    }

    [Fact]
    public async Task Handle_ValidRequest_PassesToNextHandler()
    {
        // Arrange
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<SampleRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        // Act
        var response = await validationBehavior.Handle(request, NextHandler, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        validatorMock.Verify(v => v.ValidateAsync(request, CancellationToken.None), Times.Once);
        Assert.True(response.IsSuccessful);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnsErrorResult()
    {
        // Arrange
        var validationErrors = new List<ValidationFailure>
        {
            new("PropertyName", "Error message 1"),
            new("AnotherProperty", "Error message 2")
        };

        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<SampleRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationErrors));

        // Act
        var response = await validationBehavior.Handle(request, NextHandler, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.IsSuccessful);
        Assert.Equal("PropertyName: Error message 1\nAnotherProperty: Error message 2\n", response.ErrorMessage);
        validatorMock.Verify(v => v.ValidateAsync(request, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_NoValidator_PassesToNextHandler()
    {
        // Arrange
        var behavior = new ValidationBehaviorPipeline<SampleRequest, OperationResult<SampleResponse>>();

        // Act
        var response = await behavior.Handle(request, NextHandler, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    private Task<OperationResult<SampleResponse>> NextHandler()
    {
        return Task.FromResult(OperationResult<SampleResponse>.Success(new SampleResponse()));
    }
}