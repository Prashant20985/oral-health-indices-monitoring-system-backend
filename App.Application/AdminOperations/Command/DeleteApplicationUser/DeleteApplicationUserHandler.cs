using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Command.DeleteApplicationUser;

/// <summary>
/// Represents a handler for deleting an application user.
/// </summary>
internal class DeleteApplicationUserHandler
    : IRequestHandler<DeleteApplicationUserCommand,
    OperationResult<Unit>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteApplicationUserHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for user-related operations.</param>
    public DeleteApplicationUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the command to delete an application user.
    /// </summary>
    /// <param name="request">The command containing the username and deletion comment.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the user based on the provided username.
        var user = await _userRepository.GetUserByUserNameOrEmail(request.UserName, cancellationToken);

        if (user is null)
            return OperationResult<Unit>.Failure("User not found");

        // Delete the user with the provided deletion comment.
        user.DeleteUser(request.DeleteComment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
