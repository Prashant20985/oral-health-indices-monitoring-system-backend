using App.Application.Core;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace App.Application.Behavior;

/// <summary>
/// Pipeline behavior for validation of requests using FluentValidation.
/// </summary>
/// <typeparam name="TRequest">The type of the request being processed.</typeparam>
/// <typeparam name="TResponse">The type of the response produced by the pipeline.</typeparam>
public class ValidationBehaviorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IValidator<TRequest> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehaviorPipeline{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validator">The validator for the request.</param>
    public ValidationBehaviorPipeline(IValidator<TRequest> validator = null)
    {
        _validator = validator;
    }

    /// <summary>
    /// Handles the request by performing validation and passing it to the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The request being processed.</param>
    /// <param name="next">The delegate representing the next handler in the pipeline.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The response produced by the pipeline.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // If no validator is provided, proceed to the next handler
        if (_validator is null)
            return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        // Create error messages based on validation failures
        var errors = validationResult.Errors
            .Select(e => $"{e.PropertyName}: {e.ErrorMessage}\n")
            .ToList();

        // Check if the response type is of the OperationResult<> type
        Type responseType = typeof(TResponse);
        if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(OperationResult<>))
        {
            Type resultValueType = responseType.GetGenericArguments()[0];

            // Create an instance of OperationResult with error details
            object operationResult = Activator.CreateInstance(responseType);
            PropertyInfo isSuccessfulProperty = responseType.GetProperty("IsSuccessful");
            PropertyInfo errorMessageProperty = responseType.GetProperty("ErrorMessage");
            isSuccessfulProperty.SetValue(operationResult, false);
            errorMessageProperty.SetValue(operationResult, string.Join("", errors));

            return (TResponse)operationResult;
        }

        // If the response type is not OperationResult<>, return default value
        return default;
    }
}
