using App.Domain.UnitOfWork;
using App.Persistence.Contexts;

namespace App.Persistence.UnitOfWorkImpl;

/// <summary>
/// Implementation of the <see cref="IUserContextUnitOfWork"/> interface.
/// This class is responsible for managing transactions and saving changes within the user context.
/// </summary>
public class OralEhrContextUnitOfWork : IUnitOfWork
{
    private readonly OralEhrContext _oralEhrContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="OralEhrContextUnitOfWork"/> class.
    /// </summary>
    /// <param name="oralEhrContext">The user context instance.</param>
    public OralEhrContextUnitOfWork(OralEhrContext oralEhrContext)
    {
        _oralEhrContext = oralEhrContext;
    }

    /// <inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _oralEhrContext.SaveChangesAsync(cancellationToken);
}
