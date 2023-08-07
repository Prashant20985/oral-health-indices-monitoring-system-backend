using App.Application.Core;
using App.Domain.Repository;
using App.Persistence.Contexts;
using MediatR;

namespace App.Application.AdminOperations.Command.ChangeActivationStatus;

/// <summary>
/// Handles the command to change the activation status of a user.
/// </summary>
internal class ChangeActivationStatusHandler 
    : IRequestHandler<ChangeActivationStatusCommand, OperationResult<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly UserContext _userContext;

    /// <summary>
    /// Initializes a new instance of the ChangeActivationStatusHandler class.
    /// </summary>
    /// <param name="userRepository">The repository for user-related operations.</param>
    /// <param name="userContext">The database context for user-related data.</param>
    public ChangeActivationStatusHandler(IUserRepository userRepository, UserContext userContext)
    {
        _userRepository = userRepository;
        _userContext = userContext;
    }

    /// <summary>
    /// Handles the command to change the activation status of a user.
    /// </summary>
    /// <param name="request">The command containing the username of the user.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(ChangeActivationStatusCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the user based on the provided username.
        var user = await _userRepository.GetUserByUserNameOrEmail(request.UserName, cancellationToken);

        if (user is null)
            return OperationResult<Unit>.Failure("User not found");

        // Toggle the activation status of the user.
        user.ActivationStatusToggle();

        // Save the changes to the user context.
        var result = await _userContext.SaveChangesAsync(cancellationToken);

        if (result <= 0)
            return OperationResult<Unit>.Failure("Error changing activation status");

        return OperationResult<Unit>.Success(Unit.Value);
    }
}

