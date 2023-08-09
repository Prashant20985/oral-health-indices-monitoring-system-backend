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
    private readonly IUserContextUnitOfWork _userContextUnitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWorkBehaviorPipeline{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="userContextUnitOfWork">The user context unit of work instance.</param>
    public UnitOfWorkBehaviorPipeline(IUserContextUnitOfWork userContextUnitOfWork)
    {
        _userContextUnitOfWork = userContextUnitOfWork;
    }

    /// <inheritdoc/>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.GetType().GetCustomAttribute<UserContextUnitOfWorkAttribute>() is not null)
        {
            var response = await next();
            await _userContextUnitOfWork.SaveChangesAsync(cancellationToken);
            return response;
        }

        return await next();
    }
}
