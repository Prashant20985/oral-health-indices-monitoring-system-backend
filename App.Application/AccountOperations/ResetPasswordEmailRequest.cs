using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.Interfaces;
using App.Application.NotificationOperations;
using App.Application.NotificationOperations.DTOs;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace App.Application.AccountOperations;

/// <summary>
/// Represents the class for sending a password reset email request.
/// </summary>
public class ResetPasswordEmailRequest
{
    /// <summary>
    /// Class representing the command for sending a password reset email request.
    /// </summary>
    public class Command : IRequest<OperationResult<Unit>>
    {
        /// <summary>
        /// Gets or sets the email address of the user requesting the password reset.
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// Class representing the handler for the password reset email request.
    /// </summary>
    public class Handler : IRequestHandler<Command, OperationResult<Unit>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessorService _httpContextAccessorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class with the specified dependencies.
        /// </summary>
        /// <param name="userManager">The user manager for working with user accounts.</param>
        /// <param name="mediator">The MediatR mediator for publishing notifications.</param>
        /// <param name="httpContextAccessorService">The service for accessing the current HttpContext.</param>
        public Handler(UserManager<User> userManager, IMediator mediator, IHttpContextAccessorService httpContextAccessorService)
        {
            _userManager = userManager;
            _mediator = mediator;
            _httpContextAccessorService = httpContextAccessorService;
        }

        /// <summary>
        /// Handles the password reset email request.
        /// </summary>
        /// <param name="request">The command representing the password reset email request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A result indicating the outcome of the password reset email request.</returns>
        public async Task<OperationResult<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Find the user by the provided email address
            var user = await _userManager.FindByEmailAsync(request.Email);

            // Check user validity using the UserValidation helper
            var userValidityResult = UserValidation.CheckUserValidity<Unit>(user);

            if (userValidityResult is not null)
                return OperationResult<Unit>.Failure(userValidityResult.ErrorMessage);

            // Generate the password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            var encodedEmail = WebUtility.UrlEncode(user.Email);

            // Construct the password reset callback URL with the token and email parameters
            var callbackUrl = $"{_httpContextAccessorService.GetOrigin()}/reset-password?token={encodedToken}&email={encodedEmail}";

            // Create the email content with the reset callback URL
            var emailContent = new EmailContentDto
            {
                ReceiverEmail = user.Email,
                Subject = "Reset Password",
                Message = callbackUrl,
                EmailType = EmailType.PasswordReset
            };

            // Publish the email notification to be handled by the EmailNotificationHandler
            await _mediator.Publish(new EmailNotification.Email { EmailContent = emailContent }, cancellationToken);

            return OperationResult<Unit>.Success(Unit.Value);
        }
    }
}
