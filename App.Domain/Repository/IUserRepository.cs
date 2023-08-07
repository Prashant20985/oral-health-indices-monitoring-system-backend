using App.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Repository;

/// <summary>
/// Represents a contract for interacting with user-related data and operations in the application.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets a user based on their username or email.
    /// </summary>
    /// <param name="value">The username or email to search for.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A <see cref="ApplicationUser"/> object representing the found user, if any.</returns>
    Task<ApplicationUser> GetUserByUserNameOrEmail(string value, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a user based on their username with their refresh token.
    /// </summary>
    /// <param name="userName">The username to search for.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A <see cref="ApplicationUser"/> object representing the found user, if any.</returns>
    Task<ApplicationUser> GetUserByUserNameWithRefreshToken(string userName, CancellationToken cancellationToken);

    /// <summary>
    /// Changes the password of a user.
    /// </summary>
    /// <param name="user">The user for whom to change the password.</param>
    /// <param name="currentPassword">The current password of the user.</param>
    /// <param name="newPassword">The new password to set for the user.</param>
    /// <returns>An <see cref="IdentityResult"/> representing the result of the password change operation.</returns>
    Task<IdentityResult> ChangePassword(ApplicationUser user, string currentPassword, string newPassword);

    /// <summary>
    /// Resets the password of a user using a reset token.
    /// </summary>
    /// <param name="user">The user for whom to reset the password.</param>
    /// <param name="token">The reset token to use for resetting the password.</param>
    /// <param name="password">The new password to set for the user.</param>
    /// <returns>An <see cref="IdentityResult"/> representing the result of the password reset operation.</returns>
    Task<IdentityResult> ResetPassword(ApplicationUser user, string token, string password);

    /// <summary>
    /// Checks if the provided password is valid for a user.
    /// </summary>
    /// <param name="user">The user for whom to check the password.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>True if the password is valid, false otherwise.</returns>
    Task<bool> CheckPassword(ApplicationUser user, string password);

    /// <summary>
    /// Gets the roles assigned to a user.
    /// </summary>
    /// <param name="user">The user for whom to get the roles.</param>
    /// <returns>A list of role names assigned to the user.</returns>
    Task<IList<string>> GetRoles(ApplicationUser user);

    /// <summary>
    /// Generates a reset password token for a user.
    /// </summary>
    /// <param name="user">The user for whom to generate the reset password token.</param>
    /// <returns>The generated reset password token.</returns>
    Task<string> GenerateResetPasswordToken(ApplicationUser user);
}
