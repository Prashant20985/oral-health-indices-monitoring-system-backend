using App.Application.AccountOperations.DTOs.Response;
using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AccountOperations.Command.RefreshToken;

/// <summary>
/// Request handler for the RefreshTokenCommand.
/// </summary>
internal sealed class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, OperationResult<UserLoginResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessorService _httpContextAccessorService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenHandler"/> class with the required dependencies.
    /// </summary>
    /// <param name="httpContextAccessorService">The service for accessing the HTTP context.</param>
    /// <param name="tokenService">The service for creating authentication tokens.</param>
    /// <param name="userRepository">The user repository instance.</param>
    public RefreshTokenHandler(IHttpContextAccessorService httpContextAccessorService,
        ITokenService tokenService,
        IUserRepository userRepository)
    {
        _httpContextAccessorService = httpContextAccessorService;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the refresh token request.
    /// </summary>
    /// <param name="request">The refresh token command, containg the user name for whom the authentication token needs to be refreshed.</param>
    /// <param name="cancellationToken">Cancellation token to abort the operation if needed.</param>
    /// <returns>An OperationResult containing the response for the refresh token operation.</returns>
    public async Task<OperationResult<UserLoginResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = _httpContextAccessorService.GetRefreshTokenCookie();

        var user = await _userRepository.GetUserByUserNameWithRefreshToken(request.UserName, cancellationToken);

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

        // Create a user data transfer object (DTO) with necessary user information
        var userDto = new UserLoginResponseDto(
            name: $"{user.FirstName} {user.LastName}",
            userName: user.UserName,
            email: user.Email,
            role: user.ApplicationUserRoles.Select(x => x.ApplicationRole.Name).FirstOrDefault(),
            token: await _tokenService.CreateToken(user));

        // Return the success result with the updated user authentication information
        return OperationResult<UserLoginResponseDto>.Success(userDto);
    }
}
