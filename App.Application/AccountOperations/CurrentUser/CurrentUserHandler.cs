using App.Application.AccountOperations.DTOs.Response;
using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AccountOperations.CurrentUser;

/// <summary>
/// Handler class for handling the CurrentUserQuery and returning the user's information
/// </summary>
public class CurrentUserHandler : IRequestHandler<CurrentUserQuery, OperationResult<UserLoginResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUserHandler"/> class with the specified user repository and token service.
    /// </summary>
    /// <param name="userRepository">The user repository instance.</param>
    /// <param name="tokenService">The token service for creating authentication tokens.</param>
    public CurrentUserHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Handles the CurrentUserQuery and returns the user's information
    /// </summary>
    /// <param name="request">The current user quesry</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns></returns>
    public async Task<OperationResult<UserLoginResponseDto>> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUserNameOrEmail(request.UserName, cancellationToken);

        // Check user validity using the UserValidation helper
        var userValidationResult = UserValidation.CheckUserValidity<UserLoginResponseDto>(user);

        if (userValidationResult is not null)
            return userValidationResult;

        // Get the user's roles
        var role = await _userRepository.GetRoles(user);

        // Create a UserLoginResponseDto with the user's information
        var userDto = new UserLoginResponseDto(
            name: $"{user.FirstName} {user.LastName}",
            userName: user.UserName,
            email: user.Email,
            role: string.Join(", ", role),
            token: await _tokenService.CreateToken(user));

        // Set the refresh token for the current user
        await _tokenService.SetRefreshToken(user);

        // Return the user's information as a success result
        return OperationResult<UserLoginResponseDto>.Success(userDto);
    }
}