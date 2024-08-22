using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.Interfaces;
using App.Application.NotificationOperations;
using App.Application.NotificationOperations.DTOs;
using App.Domain.Models.Enums;
using App.Domain.Repository;
using MediatR;
using System.Net;

namespace App.Application.AccountOperations.Command.ResetPasswordUrlEmailRequest;

/// <summary>
/// Handler the ResetPasswordUrlEmailRequestCommand to request a password reset URL through email.
/// </summary>
internal sealed class ResetPasswordUrlEmailRequestHandler : IRequestHandler<ResetPasswordUrlEmailRequestCommand, OperationResult<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessorService _httpContextAccessorService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResetPasswordUrlEmailRequestHandler"/> class with the required dependencies.
    /// </summary>
    /// <param name="httpContextAccessorService">The HTTP context accessor service to access the request context.</param>
    /// <param name="mediator">The mediator instance for handling MediatR requests.</param>
    /// <param name="userRepository">The user repository instance for accessing user data.</param>
    public ResetPasswordUrlEmailRequestHandler(
        IHttpContextAccessorService httpContextAccessorService,
        IMediator mediator,
        IUserRepository userRepository)
    {
        _httpContextAccessorService = httpContextAccessorService;
        _mediator = mediator;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the user reset password url email request.
    /// </summary>
    /// <param name="request">The ResetPasswordUrlEmailRequestCommand containing user's user name.</param>
    /// <param name="cancellationToken">Cancellation token to abort the operation if needed.</param>
    /// <returns>An OperationResult of Unit indicating the result of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(ResetPasswordUrlEmailRequestCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the user based on the provided email
        var user = await _userRepository.GetUserByUserNameOrEmail(request.Email, cancellationToken);

        // Check user validity using the UserValidation helper
        var userValidityResult = UserValidation.CheckUserValidity<Unit>(user);

        if (userValidityResult is not null)
            return OperationResult<Unit>.Failure(userValidityResult.ErrorMessage);
       
        // Generate a password reset token for the user
        var token = await _userRepository.GenerateResetPasswordToken(user);
        var encodedToken = WebUtility.UrlEncode(token);
        var encodedEmail = WebUtility.UrlEncode(user.Email);

        // Construct the password reset callback URL with the token and email parameters
        var callbackUrl = $"{_httpContextAccessorService.GetOrigin()}/reset-password?token={encodedToken}&email={encodedEmail}";

        // Create the email content for the password reset request
        var emailContent = new EmailContentDto(
            receiverEmail: user.Email,
            subject: "Reset Password",
            message: callbackUrl,
            emailType: EmailType.PasswordReset);

        // Publish the email notification
        await _mediator.Publish(new EmailNotification(emailContent));

        // Return a success result with the Unit value to indicate successful password reset URL request
        return OperationResult<Unit>.Success(Unit.Value);
    }
}