using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AccountOperations.Command.ChangePassword;

/// <summary>
/// Class to handle the ChangePasswordCommand
/// </summary>
public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, OperationResult<Unit>>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    public ChangePasswordHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the change password command and performs the necessary actions to chnage the user's password
    /// </summary>
    /// <param name="request">The change password command.</param>
    /// <param name="cancellationToken">Cancellation token to abort the operation if needed.</param>
    /// <returns>An OperationResult of Unit indicating the result of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the user from the repository using the provided email or username
        var user = await _userRepository.GetUserByUserNameOrEmail(request.ChangePassword.Email, cancellationToken);

        // Check if the user is valid using the UserValidation helper class
        var userValidityResult = UserValidation.CheckUserValidity<Unit>(user);

        // If the user is invalid (e.g., not found or inactive), return the validation result
        if (userValidityResult is not null)
            return userValidityResult;

        // Check if the new password is the same as the old password
        if (request.ChangePassword.CurrentPassword.Equals(request.ChangePassword.NewPassword))
            return OperationResult<Unit>.Failure("New password cannot be the same as the old password");

        // Attempt to change the user's password using the UserRepository
        var result = await _userRepository.ChangePassword(user, request.ChangePassword.CurrentPassword, request.ChangePassword.NewPassword);

        // If the password change operation was not successful, return the failure result
        if (!result.Succeeded)
            return OperationResult<Unit>.Failure(result.Errors.FirstOrDefault().Description.ToString());

        // Password change was successful, return success result with Unit value
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
