using App.Domain.Models.Users;
using App.Domain.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

/// <summary>
/// Implementation of the <see cref="IUserRepository"/> interface for user-related data access and operations.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class with the specified user manager.
    /// </summary>
    /// <param name="userManager">The ASP.NET Core Identity UserManager instance.</param>
    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Changes the password for the specified user.
    /// </summary>
    /// <param name="user">The user for which to change the password.</param>
    /// <param name="currentPassword">The current password of the user.</param>
    /// <param name="newPassword">The new password to set for the user.</param>
    /// <returns>An IdentityResult indicating whether the password change was successful.</returns>
    public async Task<IdentityResult> ChangePassword(ApplicationUser user, string currentPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    /// <summary>
    /// Checks if the provided password is valid for the specified user.
    /// </summary>
    /// <param name="user">The user to check the password against.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>True if the password is valid, false otherwise.</returns>
    public async Task<bool> CheckPassword(ApplicationUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    /// <summary>
    /// Generates a reset password token for the specified user.
    /// </summary>
    /// <param name="user">The user for which to generate the reset password token.</param>
    /// <returns>The reset password token as a string.</returns>
    public async Task<string> GenerateResetPasswordToken(ApplicationUser user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    /// <summary>
    /// Gets the roles assigned to the specified user.
    /// </summary>
    /// <param name="user">The user for which to retrieve the roles.</param>
    /// <returns>A list of role names assigned to the user.</returns>
    public async Task<IList<string>> GetRoles(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    /// <summary>
    /// Finds a user by their username or email address.
    /// </summary>
    /// <param name="value">The username or email address to search for.</param>
    /// <param name="cancellationToken">A cancellation token to observe cancellation requests.</param>
    /// <returns>The user if found, or null if not found.</returns>
    public async Task<ApplicationUser> GetUserByUserNameOrEmail(string value, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _userManager.FindByNameAsync(value) ??
            await _userManager.FindByEmailAsync(value);
    }

    /// <summary>
    /// Finds a user by their username and eagerly loads their refresh tokens.
    /// </summary>
    /// <param name="userName">The username to search for.</param>
    /// <param name="cancellationToken">A cancellation token to observe cancellation requests.</param>
    /// <returns>The user if found, or null if not found.</returns>
    public async Task<ApplicationUser> GetUserByUserNameWithRefreshToken(string userName, CancellationToken cancellationToken)
    {
        return await _userManager.Users
            .Include(r => r.RefreshTokens)
            .FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
    }

    /// <summary>
    /// Resets a user's password using a provided reset password token.
    /// </summary>
    /// <param name="user">The user for which to reset the password.</param>
    /// <param name="token">The reset password token.</param>
    /// <param name="password">The new password to set for the user.</param>
    /// <returns>An IdentityResult indicating whether the password reset was successful.</returns>
    public async Task<IdentityResult> ResetPassword(ApplicationUser user, string token, string password)
    {
        return await _userManager.ResetPasswordAsync(user, token, password);
    }
}
