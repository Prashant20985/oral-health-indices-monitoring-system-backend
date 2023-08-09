using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Command.ChangeActivationStatus;

/// <summary>
/// Handles the command to change the activation status of a user.
/// </summary>
internal class ChangeActivationStatusHandler
    : IRequestHandler<ChangeActivationStatusCommand, OperationResult<Unit>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the ChangeActivationStatusHandler class.
    /// </summary>
    /// <param name="userRepository">The repository for user-related operations.</param>
    /// <param name="userContext">The database context for user-related data.</param>
    public ChangeActivationStatusHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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

        return OperationResult<Unit>.Success(Unit.Value);
    }
}

