using App.Application.AccountOperations.DTOs.Response;
using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.Application.AccountOperations;

/// <summary>
/// Represents the command to refresh the user authentication token.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Command to refresh the user authentication token.
    /// </summary>
    public class RefreshTokenCommand : IRequest<OperationResult<UserLoginResponseDto>>
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string UserName { get; set; }
    }

    /// <summary>
    /// Handles the refresh token command and returns the updated user authentication information.
    /// </summary>
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, OperationResult<UserLoginResponseDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessorService _httpContextAccessorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class with the required services.
        /// </summary>
        /// <param name="userManager">The user manager to handle user-related operations.</param>
        /// <param name="tokenService">The service responsible for generating tokens.</param>
        /// <param name="httpContextAccessorService">The service providing access to the current HTTP context.</param>
        public RefreshTokenHandler(UserManager<User> userManager,
            ITokenService tokenService,
            IHttpContextAccessorService httpContextAccessorService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _httpContextAccessorService = httpContextAccessorService;
        }

        /// <summary>
        /// Handles the refresh token command and generates a new authentication token for the user.
        /// </summary>
        /// <param name="request">The refresh token command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An operation result containing the updated user authentication information.</returns>
        public async Task<OperationResult<UserLoginResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var httpRequest = _httpContextAccessorService.GetRequest();
            var refreshToken = httpRequest.Cookies["refreshToken"];

            // Retrieve the user with associated refresh tokens from the database
            var user = await _userManager.Users
                .Include(r => r.RefreshTokens)
                .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken: cancellationToken);

            // Check if the user is valid and not null
            var checkUserValidity = UserValidation.CheckUserValidity<UserLoginResponseDto>(user);
            if (checkUserValidity is not null)
                return checkUserValidity;

            // Retrieve the old token associated with the current refresh token
            var oldToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

            // Check if the old token is not null and not active, return Unauthorized result
            if (oldToken is not null && !oldToken.IsActive)
                return OperationResult<UserLoginResponseDto>.Unauthorized("Unauthorized");

            // Mark the old token as revoked if it exists
            if (oldToken is not null)
                oldToken.Revoked = DateTime.UtcNow;

            // Fetches the current role of the user
            var roles = await _userManager.GetRolesAsync(user);

            // Create a user data transfer object (DTO) with necessary user information
            var userDto = new UserLoginResponseDto
            {
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Role = string.Join(", ", roles),
                UserName = user.UserName
            };

            // Generate the user token using the token service
            userDto.Token = await _tokenService.CreateToken(user);

            // Return the success result with the updated user authentication information
            return OperationResult<UserLoginResponseDto>.Success(userDto);
        }
    }
}
