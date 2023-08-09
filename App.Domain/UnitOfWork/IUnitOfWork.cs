namespace App.Domain.UnitOfWork;

/// <summary>
/// Represents a unit of work interface for managing transactions and saving changes.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Asynchronously saves the changes made in the unit of work.
    /// </summary>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
