using App.Domain.UnitOfWork;
using App.Persistence.Attributes;
using MediatR;
using System.Reflection;

namespace App.Application.Behavior;

/// <summary>
/// Pipeline behavior for handling unit of work transactions based on attributes.
/// This behavior detects command types marked with the any context unit of work,
/// and uses the provided user context unit of work to manage transactions.
/// </summary>
public class UnitOfWorkBehaviorPipeline<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWorkBehaviorPipeline{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="unitOfWork">The user context unit of work instance.</param>
    public UnitOfWorkBehaviorPipeline(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the request by checking for the presence of the OralEhrContextUnitOfWorkAttribute and managing transactions accordingly.
    /// </summary>
    /// <param name="request">The request being processed.</param>
    /// <param name="next">The delegate representing the next handler in the pipeline.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The response produced by the pipeline.</returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Check if the request type has the OralEhrContextUnitOfWorkAttribute
        if (request.GetType().GetCustomAttribute<OralEhrContextUnitOfWorkAttribute>() is not null)
        {
            // If the attribute is present, proceed with the next handler in the pipeline
            var response = await next();
            // Save changes to the unit of work
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            // Return the response
            return response;
        }

        // If the attribute is not present, simply proceed with the next handler
        return await next();
    }
}
