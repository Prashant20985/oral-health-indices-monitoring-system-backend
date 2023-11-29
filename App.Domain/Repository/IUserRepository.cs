using App.Domain.DTOs;
using App.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Repository;

/// <summary>
/// Represents an interface for interacting with user-related data and operations in the application.
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
    /// <param name="applicationUser">The user for whom to change the password.</param>
    /// <param name="currentPassword">The current password of the user.</param>
    /// <param name="newPassword">The new password to set for the user.</param>
    /// <returns>An <see cref="IdentityResult"/> representing the result of the password change operation.</returns>
    Task<IdentityResult> ChangePassword(ApplicationUser applicationUser, string currentPassword, string newPassword);

    /// <summary>
    /// Resets the password of a user using a reset token.
    /// </summary>
    /// <param name="applicationUser">The user for whom to reset the password.</param>
    /// <param name="token">The reset token to use for resetting the password.</param>
    /// <param name="password">The new password to set for the user.</param>
    /// <returns>An <see cref="IdentityResult"/> representing the result of the password reset operation.</returns>
    Task<IdentityResult> ResetPassword(ApplicationUser applicationUser, string token, string password);

    /// <summary>
    /// Checks if the provided password is valid for a user.
    /// </summary>
    /// <param name="applicationUser">The user for whom to check the password.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>True if the password is valid, false otherwise.</returns>
    Task<bool> CheckPassword(ApplicationUser applicationUser, string password);

    /// <summary>
    /// Generates a reset password token for a user.
    /// </summary>
    /// <param name="applicationUser">The user for whom to generate the reset password token.</param>
    /// <returns>The generated reset password token.</returns>
    Task<string> GenerateResetPasswordToken(ApplicationUser applicationUser);

    /// <summary>
    /// Get an IQueryable of active application users.
    /// </summary>
    /// <returns>An IQueryable of ApplicationUserDto representing active application users.</returns>
    IQueryable<ApplicationUserDto> GetActiveApplicationUsersQuery();

    /// <summary>
    /// Get an IQueryable of deactivated application users.
    /// </summary>
    /// <returns>An IQueryable of ApplicationUserDto representing deactivated application users.</returns>
    IQueryable<ApplicationUserDto> GetDeactivatedApplicationUsersQuery();

    /// <summary>
    /// Get an IQueryable of deleted application users.
    /// </summary>
    /// <returns>An IQueryable of ApplicationUserDto representing deleted application users.</returns>
    IQueryable<ApplicationUserDto> GetDeletedApplicationUsersQuery();

    /// <summary>
    /// Create a new application user asynchronously.
    /// </summary>
    /// <param name="applicationUser">The ApplicationUser instance to create.</param>
    /// <param name="password">The password for the new user.</param>
    /// <returns>An IdentityResult indicating the result of the user creation operation.</returns>
    Task<IdentityResult> CreateApplicationUserAsync(ApplicationUser applicationUser, string password);

    /// <summary>
    /// Add an application user to a role asynchronously.
    /// </summary>
    /// <param name="applicationUser">The ApplicationUser instance to add to the role.</param>
    /// <param name="role">The role to which the user should be added.</param>
    /// <returns>An IdentityResult indicating the result of the role assignment operation.</returns>
    Task<IdentityResult> AddApplicationUserToRoleAsync(ApplicationUser applicationUser, string role);

    /// <summary>
    /// Remove an application user from specified roles asynchronously.
    /// </summary>
    /// <param name="applicationUser">The ApplicationUser instance to remove from roles.</param>
    /// <param name="applicationUserRoles">A list of roles from which the user should be removed.</param>
    /// <returns>An IdentityResult indicating the result of the role removal operation.</returns>
    Task<IdentityResult> RemoveApplicationUserFromRolesAsync(ApplicationUser applicationUser, List<string> applicationUserRoles);
}
