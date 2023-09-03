using App.Application.AccountOperations.Helpers;
using App.Application.Core;
using App.Application.NotificationOperations;
using App.Application.NotificationOperations.DTOs;
using App.Domain.Models.Enums;
using App.Domain.Repository;
using MediatR;

namespace App.Application.AccountOperations.Command.ResetPassword;

/// <summary>
/// Handles the ResetPasswordCommand to reset a user's password.
/// </summary>
internal sealed class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, OperationResult<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResetPasswordHandler"/> class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator instance.</param>
    /// <param name="userRepository">The user repository instance.</param>
    public ResetPasswordHandler(IMediator mediator, IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Handles the reset password command and performs the necessary actions to reset the user's password.
    /// </summary>
    /// <param name="request">The reset password command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An OperationResult of Unit indicating the result of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the user based on the provided email
        var user = await _userRepository.GetUserByUserNameOrEmail(request.ResetPassword.Email, cancellationToken);

        // Check the validity of the user using the UserValidation helper
        var userValidityResult = UserValidation.CheckUserValidity<Unit>(user);

        if (userValidityResult is not null)
            return userValidityResult;

        // Attempt to reset the user's password using the provided token and new password
        var resetPassResult = await _userRepository.ResetPassword(user,
            request.ResetPassword.Token,
            request.ResetPassword.Password);

        if (!resetPassResult.Succeeded)
            return OperationResult<Unit>.Failure(resetPassResult.Errors.FirstOrDefault().Description.ToString());

        // Send a password reset confirmation email to the user
        var emailContent = new EmailContentDto(
            receiverEmail: user.Email,
            subject: "Password Reset Confirmation",
            message: $"{user.UserName} {request.ResetPassword.Password}",
            emailType: EmailType.PasswordResetConfirmation);

        await _mediator.Publish(new EmailNotification(emailContent));

        // Return a success result with the Unit value to indicate successful password reset
        return OperationResult<Unit>.Success(Unit.Value);
    }
}