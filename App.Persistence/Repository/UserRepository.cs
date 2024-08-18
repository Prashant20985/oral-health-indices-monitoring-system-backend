using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Models.Users;
using App.Domain.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

/// <summary>
/// Implementation of the <see cref="IUserRepository"/> interface for user-related data access and operations.
/// </summary>

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class with the specified user manager.
    /// </summary>
    /// <param name="userManager">The ASP.NET Core Identity UserManager instance.</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UserRepository(UserManager<ApplicationUser> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Add an application user to a role asynchronously.
    /// </summary>
    /// <param name="applicationUser">The ApplicationUser instance to add to the role.</param>
    /// <param name="role">The role to which the user should be added.</param>
    /// <returns>An IdentityResult indicating the result of the role assignment operation.</returns>
    public async Task<IdentityResult> AddApplicationUserToRoleAsync(ApplicationUser applicationUser,
        string role) => await _userManager.AddToRoleAsync(applicationUser, role);


    /// <summary>
    /// Changes the password for the specified user.
    /// </summary>
    /// <param name="applicationUser">The user for which to change the password.</param>
    /// <param name="currentPassword">The current password of the user.</param>
    /// <param name="newPassword">The new password to set for the user.</param>
    /// <returns>An IdentityResult indicating whether the password change was successful.</returns>
    public async Task<IdentityResult> ChangePassword(
        ApplicationUser applicationUser,
        string currentPassword,
        string newPassword) => await _userManager
            .ChangePasswordAsync(applicationUser, currentPassword, newPassword);


    /// <summary>
    /// Checks if the provided password is valid for the specified user.
    /// </summary>
    /// <param name="applicationUser">The user to check the password against.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>True if the password is valid, false otherwise.</returns>
    public async Task<bool> CheckPassword(
        ApplicationUser applicationUser,
        string password) => await _userManager
            .CheckPasswordAsync(applicationUser, password);


    /// <summary>
    /// Create a new application user asynchronously.
    /// </summary>
    /// <param name="applicationUser">The ApplicationUser instance to create.</param>
    /// <param name="password">The password for the new user.</param>
    /// <returns>An IdentityResult indicating the result of the user creation operation.</returns>
    public async Task<IdentityResult> CreateApplicationUserAsync(ApplicationUser applicationUser,
        string password) => await _userManager.CreateAsync(applicationUser, password);


    /// <summary>
    /// Generates a reset password token for the specified user.
    /// </summary>
    /// <param name="user">The user for which to generate the reset password token.</param>
    /// <returns>The reset password token as a string.</returns>
    public async Task<string> GenerateResetPasswordToken(ApplicationUser applicationUser) => await _userManager
            .GeneratePasswordResetTokenAsync(applicationUser);


    /// <summary>
    /// Get an IQueryable of active application users.
    /// </summary>
    /// <returns>An IQueryable of ApplicationUserDto representing active application users.</returns>
    public IQueryable<ApplicationUserResponseDto> GetActiveApplicationUsersQuery(string currentUserId) => _userManager.Users
        .Where(x => x.IsAccountActive && x.DeletedAt == null && x.Id != currentUserId)
        .ProjectTo<ApplicationUserResponseDto>(_mapper.ConfigurationProvider)
        .OrderBy(c => c.CreatedAt)
        .AsQueryable();

    /// <summary>
    /// Get ApplicationUser by Id with roles
    /// </summary>
    /// <param name="userId">Unique identifier of the user</param>
    /// <returns>ApplicationUser with roles</returns>
    public async Task<ApplicationUser> GetApplicationUserWithRolesById(string userId) => await
        _userManager.Users
            .Include(x => x.ApplicationUserRoles)
            .ThenInclude(x => x.ApplicationRole)
            .FirstOrDefaultAsync(x => x.Id == userId);

    /// <summary>
    /// Get an IQueryable of deactivated application users.
    /// </summary>
    /// <returns>An IQueryable of ApplicationUserDto representing deactivated application users.</returns>
    public IQueryable<ApplicationUserResponseDto> GetDeactivatedApplicationUsersQuery() => _userManager.Users
        .ProjectTo<ApplicationUserResponseDto>(_mapper.ConfigurationProvider)
        .Where(x => !x.IsAccountActive && x.DeletedAt == null)
        .OrderBy(c => c.CreatedAt)
        .AsQueryable();


    /// <summary>
    /// Get an IQueryable of deleted application users.
    /// </summary>
    /// <returns>An IQueryable of ApplicationUserDto representing deleted application users.</returns>
    public IQueryable<ApplicationUserResponseDto> GetDeletedApplicationUsersQuery() => _userManager.Users
        .ProjectTo<ApplicationUserResponseDto>(_mapper.ConfigurationProvider)
        .Where(x => x.DeletedAt != null || !string.IsNullOrWhiteSpace(x.DeleteUserComment))
        .OrderBy(c => c.CreatedAt)
        .AsQueryable();


    /// <summary>
    /// Finds a user by their username or email address.
    /// </summary>
    /// <param name="value">The username or email address to search for.</param>
    /// <param name="cancellationToken">A cancellation token to observe cancellation requests.</param>
    /// <returns>The user if found, or null if not found.</returns>
    public async Task<ApplicationUser> GetUserByUserNameOrEmail(
        string value,
        CancellationToken cancellationToken) => await _userManager.Users
            .Include(x => x.ApplicationUserRoles)
            .ThenInclude(x => x.ApplicationRole)
            .FirstOrDefaultAsync(x => x.Email == value || x.UserName == value, cancellationToken);


    /// <summary>
    /// Finds a user by their username and eagerly loads their refresh tokens.
    /// </summary>
    /// <param name="userName">The username to search for.</param>
    /// <param name="cancellationToken">A cancellation token to observe cancellation requests.</param>
    /// <returns>The user if found, or null if not found.</returns>
    public async Task<ApplicationUser> GetUserByUserNameWithRefreshToken(
        string userName,
        CancellationToken cancellationToken) => await _userManager.Users
            .Include(r => r.RefreshTokens)
            .FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);


    /// <summary>
    /// Remove an application user from specified roles asynchronously.
    /// </summary>
    /// <param name="applicationUser">The ApplicationUser instance to remove from roles.</param>
    /// <param name="applicationUserRoles">A list of roles from which the user should be removed.</param>
    /// <returns>An IdentityResult indicating the result of the role removal operation.</returns>
    public async Task<IdentityResult> RemoveApplicationUserFromRolesAsync(
        ApplicationUser applicationUser,
        List<string> applicationUserRoles) => await _userManager
                .RemoveFromRolesAsync(applicationUser, applicationUserRoles);


    /// <summary>
    /// Resets a user's password using a provided reset password token.
    /// </summary>
    /// <param name="applicationUser">The user for which to reset the password.</param>
    /// <param name="token">The reset password token.</param>
    /// <param name="password">The new password to set for the user.</param>
    /// <returns>An IdentityResult indicating whether the password reset was successful.</returns>
    public async Task<IdentityResult> ResetPassword(
        ApplicationUser applicationUser,
        string token,
        string password) => await _userManager
            .ResetPasswordAsync(applicationUser, token, password);
}
