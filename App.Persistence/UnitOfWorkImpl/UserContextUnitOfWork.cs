using App.Domain.UnitOfWork;
using App.Persistence.Contexts;

namespace App.Persistence.UnitOfWorkImpl;

/// <summary>
/// Implementation of the <see cref="IUserContextUnitOfWork"/> interface.
/// This class is responsible for managing transactions and saving changes within the user context.
/// </summary>
public class UserContextUnitOfWork : IUserContextUnitOfWork
{
    private readonly UserContext _userContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserContextUnitOfWork"/> class.
    /// </summary>
    /// <param name="userContext">The user context instance.</param>
    public UserContextUnitOfWork(UserContext userContext)
    {
        _userContext = userContext;
    }

    /// <inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _userContext.SaveChangesAsync(cancellationToken);
}
