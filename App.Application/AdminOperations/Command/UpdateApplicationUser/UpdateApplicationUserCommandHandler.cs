using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AdminOperations.Command.UpdateApplicationUser;

/// <summary>
/// Represents a handler for updating an application user.
/// </summary>
internal sealed class UpdateApplicationUserCommandHandler
    : IRequestHandler<UpdateApplicationUserCommand,
    OperationResult<Unit>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateApplicationUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The repository for user-related operations.</param>
    public UpdateApplicationUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the command to update an application user.
    /// </summary>
    /// <param name="request">The command containing the username and updated user data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(UpdateApplicationUserCommand request,
        CancellationToken cancellationToken)
    {
        // Retrieve the application user based on the provided username.
        var applicationUser = await _userRepository.GetUserByUserNameOrEmail(request.UserName, cancellationToken);

        // If the user is not found, return a failure result with the message "User not found".
        if (applicationUser is null)
            return OperationResult<Unit>.Failure("User not found");

        // If the user is marked as deleted, return a failure result with the message "Deleted user".
        if (applicationUser.DeletedAt is not null)
            return OperationResult<Unit>.Failure("Deleted user");

        // Check if the phone number and guest user comment are null or whitespace and update them accordingly.
        request.UpdateApplicationUser.PhoneNumber = CheckNullOrWhiteSpace(request.UpdateApplicationUser.PhoneNumber);
        request.UpdateApplicationUser.GuestUserComment = CheckNullOrWhiteSpace(request.UpdateApplicationUser.GuestUserComment);
        request.UpdateApplicationUser.LastName = CheckNullOrWhiteSpace(request.UpdateApplicationUser.LastName);

        // Update the user with the provided updated user data.
        applicationUser.UpdateUser(request.UpdateApplicationUser);

        // Retrieve the current roles of the application user.
        var applicationUserRoles = applicationUser.ApplicationUserRoles
            .Select(x => x.ApplicationRole.Name)
            .ToList();

        // Check if the new role is different from existing roles and update accordingly.
        if (!applicationUserRoles.Contains(request.UpdateApplicationUser.Role))
        {
            // If the user has existing roles, remove them.
            if (applicationUserRoles.Any())
            {
                var removeFromRole = await _userRepository
                    .RemoveApplicationUserFromRolesAsync(applicationUser, applicationUserRoles);
               
                // If removing existing roles fails, return a failure result.
                if (!removeFromRole.Succeeded)
                    return OperationResult<Unit>.Failure("Failed to remove existing role");
            }
            // Add the user to the new role.
            await _userRepository
                .AddApplicationUserToRoleAsync(applicationUser, request.UpdateApplicationUser.Role);
        }

        // Return a success result.
        return OperationResult<Unit>.Success(Unit.Value);
    }

    /// <summary>
    /// Checks if the value is null or whitespace and returns null if true; otherwise, returns the value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>The value or null if it's whitespace.</returns>
    private static string CheckNullOrWhiteSpace(string value) => string.IsNullOrWhiteSpace(value) ? null : value;
}
