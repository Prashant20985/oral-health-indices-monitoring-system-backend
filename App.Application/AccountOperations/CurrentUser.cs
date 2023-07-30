using App.Application.AccountOperations.DTOs.Response;
using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace App.Application.AccountOperations;

/// <summary>
/// Class representing the "CurrentUser" query.
/// </summary>
public class CurrentUser
{
    /// <summary>
    /// Class representing the query for getting the current user information.
    /// </summary>
    public class CurrentUserQuery : IRequest<OperationResult<UserLoginResponseDto>>
    {
        /// <summary>
        /// Gets or sets the username of the current user.
        /// </summary>
        public string UserName { get; set; }
    }

    /// <summary>
    /// Class representing the handler for the "CurrentUser" query.
    /// </summary>
    public class CurrentUserHandler : IRequestHandler<CurrentUserQuery, OperationResult<UserLoginResponseDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class with the specified user manager and token service.
        /// </summary>
        /// <param name="userManager">The user manager for working with user accounts.</param>
        /// <param name="tokenService">The token service for creating authentication tokens.</param>
        public CurrentUserHandler(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Handles the "CurrentUser" query.
        /// </summary>
        /// <param name="request">The query representing the "CurrentUser" request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A result indicating the outcome of the "CurrentUser" query.</returns>
        public async Task<OperationResult<UserLoginResponseDto>> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Find the user by their username
            var user = await _userManager.FindByNameAsync(request.UserName);

            // Check user validity using the UserValidation helper
            var userValidityResult = UserValidation.CheckUserValidity<UserLoginResponseDto>(user);

            if (userValidityResult is not null)
                return userValidityResult;

            // Get the roles of the user
            var roles = await _userManager.GetRolesAsync(user);

            // Create a UserLoginResponseDto object with the user's information and token
            var userDto = new UserLoginResponseDto
            {
                Name = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Role = string.Join(", ", roles),
                UserName = user.UserName
            };

            // Generate the user token using the token service
            userDto.Token = await _tokenService.CreateToken(user);

            return OperationResult<UserLoginResponseDto>.Success(userDto);
        }
    }
}
