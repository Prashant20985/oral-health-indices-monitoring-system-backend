using App.Application.AccountOperations.DTOs.Request;
using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.Interfaces;
using App.Application.NotificationOperations;
using App.Application.NotificationOperations.DTOs;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace App.Application.AccountOperations;

/// <summary>
/// Class representing a password reset request.
/// </summary>
public class ResetPassword
{
    /// <summary>
    /// Class representing the command for resetting a user's password.
    /// </summary>
    public class ResetPasswordCommand : IRequest<OperationResult<Unit>>
    {
        /// <summary>
        /// Gets or sets the password reset data.
        /// </summary>
        public ResetPasswordDto ResetPassword { get; set; }
    }

    /// <summary>
    /// Class representing the handler for the password reset request.
    /// </summary>
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, OperationResult<Unit>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordHandler"/> class with the specified user manager.
        /// </summary>
        /// <param name="userManager">The user manager for working with user accounts.</param>
        /// <param name="mediator">The MediatR mediator for publishing notifications.</param>
        public ResetPasswordHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        /// <summary>
        /// Handles the password reset request.
        /// </summary>
        /// <param name="request">The command representing the password reset request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A result indicating the outcome of the password reset request.</returns>
        public async Task<OperationResult<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByEmailAsync(request.ResetPassword.Email);

            // Check the validity of the user
            var userValidityResult = UserValidation.CheckUserValidity<Unit>(user);

            if (userValidityResult is not null)
                return userValidityResult;

            // Reset the user's password
            var resetPasswordResult = await _userManager
                .ResetPasswordAsync(user, request.ResetPassword.Token, request.ResetPassword.Password);

            if (!resetPasswordResult.Succeeded)
            {
                var errorMessage = resetPasswordResult.Errors.Select(error => error.Description).FirstOrDefault();
                return OperationResult<Unit>.Failure(errorMessage);
            }

            // Create the email content
            var emailContent = new EmailContentDto
            {
                ReceiverEmail = user.Email,
                Subject = "Password Reset Confirmation",
                Message = $"{user.UserName} {request.ResetPassword.Password}",
                EmailType = EmailType.PasswordResetConfirmation
            };

            // Publish the email notification to be handled by the EmailNotificationHandler
            await _mediator.Publish(new EmailNotification.Email { EmailContent = emailContent }, cancellationToken);

            return OperationResult<Unit>.Success(Unit.Value);
        }
    }
}

