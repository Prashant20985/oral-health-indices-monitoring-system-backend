using App.Application.AccountOperations.DTOs.Request;
using App.Application.AccountOperations.DTOs.Response;
using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace App.Application.AccountOperations
{
    /// <summary>
    /// Class representing the user login operation.
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Class representing the command for user login.
        /// </summary>
        public class Command : IRequest<OperationResult<UserLoginResponseDto>>
        {
            /// <summary>
            /// Gets or sets the login data.
            /// </summary>
            public UserCredentialsDto Credentials { get; set; }
        }

        /// <summary>
        /// Class representing the handler for the user login request.
        /// </summary>
        public class Handler : IRequestHandler<Command, OperationResult<UserLoginResponseDto>>
        {
            private readonly UserManager<User> _userManager;
            private readonly ITokenService _tokenService;
            private readonly ILogger<Handler> _logger;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class with the specified user manager, token service, and logger factory.
            /// </summary>
            /// <param name="userManager">The user manager for working with user accounts.</param>
            /// <param name="tokenService">The token service for creating authentication tokens.</param>
            /// <param name="loggerFactory">The logger factory to create the logger.</param>
            public Handler(UserManager<User> userManager, ITokenService tokenService, ILoggerFactory loggerFactory)
            {
                _userManager = userManager;
                _tokenService = tokenService;
                _logger = loggerFactory.CreateLogger<Handler>();
            }

            /// <summary>
            /// Handles the user login request.
            /// </summary>
            /// <param name="request">The command representing the user login request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>A result indicating the outcome of the user login request.</returns>
            public async Task<OperationResult<UserLoginResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var user = await _userManager.FindByNameAsync(request.Credentials.Email)
                    ?? await _userManager.FindByEmailAsync(request.Credentials.Email);

                // Check user validity using the UserValidation helper
                var userValidationResult = UserValidation.CheckUserValidity<UserLoginResponseDto>(user);

                if (userValidationResult is not null)
                    return userValidationResult;

                // Check if the credentials are valid
                var validCreds = await _userManager.CheckPasswordAsync(user, request.Credentials.Password);

                if (!validCreds)
                    return OperationResult<UserLoginResponseDto>.Unauthorized("Invalid Credentials");

                var roles = await _userManager.GetRolesAsync(user);

                var userDto = new UserLoginResponseDto
                {
                    Name = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    Role = string.Join(", ", roles),
                    UserName = user.UserName
                };

                // Generate the user token using the token service
                userDto.Token = await _tokenService.CreateToken(user);

                // Sets the refresh token for current user
                await _tokenService.SetRefreshToken(user);

                // Log successful user login
                _logger.LogInformation("User Logged in");

                return OperationResult<UserLoginResponseDto>.Success(userDto);
            }
        }
    }
}
