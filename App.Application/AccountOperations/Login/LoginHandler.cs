using App.Application.AccountOperations.DTOs.Response;
using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AccountOperations.Login;

/// <summary>
/// Represents the handler for the LoginCommand, responsible for handling user login operations.
/// </summary>
public class LoginHandler : IRequestHandler<LoginCommand, OperationResult<UserLoginResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginHandler"/> class with the specified user repository and token service.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    /// <param name="tokenService">The token service for creating authentication tokens.</param>
    public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Handles the user login request.
    /// </summary>
    /// <param name="request">The LoginCommand containing user credentials.</param>
    /// <param name="cancellationToken">Cancellation token to abort the operation if needed.</param>
    /// <returns>An OperationResult containing the response for the user login operation.</returns>
    public async Task<OperationResult<UserLoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the user using the provided email or username
        var user = await _userRepository.GetUserByUserNameOrEmail(request.UserCredentials.Email, cancellationToken);

        // Check user validity using the UserValidation helper
        var userValidationResult = UserValidation.CheckUserValidity<UserLoginResponseDto>(user);

        if (userValidationResult is not null)
            return userValidationResult;

        // Check if the provided password is valid for the user
        var validCreds = await _userRepository.CheckPassword(user, request.UserCredentials.Password);

        if (!validCreds)
            return OperationResult<UserLoginResponseDto>.Unauthorized("Invalid Credentials");

        // Retrieve the roles of the user
        var role = await _userRepository.GetRoles(user);

        // Create a UserLoginResponseDto with user information and authentication token
        var userDto = new UserLoginResponseDto(
            name: $"{user.FirstName} {user.LastName}",
            userName: user.UserName,
            email: user.Email,
            role: string.Join(", ", role),
            token: await _tokenService.CreateToken(user));

        // Sets the refresh token for the current user
        await _tokenService.SetRefreshToken(user);

        // Return the successful response containing the UserLoginResponseDto
        return OperationResult<UserLoginResponseDto>.Success(userDto);
    }
}
