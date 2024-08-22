using FluentValidation;

namespace App.Application.AccountOperations.Command.ResetPassword;

/// <summary>
/// Validator for the ResetPasswordCommand.
/// </summary>
/// <remarks>
/// This validator ensures that the ResetPasswordCommand contains valid data:
/// - The email must be a valid email address.
/// - The password must be at least 8 characters long and not empty.
/// - The confirm password must match the password.
/// - The token must not be empty.
/// </remarks>
public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{  /// <summary>
    /// Initializes a new instance of the <see cref="ResetPasswordCommandValidator"/> class.
    /// </summary>
    public ResetPasswordCommandValidator()
    {
        // Validate email
        RuleFor(x => x.ResetPassword.Email)
            .EmailAddress()
            .OverridePropertyName("Email");
        
        // Validate password
        RuleFor(x => x.ResetPassword.Password)
            .NotEmpty()
            .MinimumLength(8)
            .OverridePropertyName("Password");

        // Validate confirm password
        RuleFor(x => x.ResetPassword.ConfirmPassword)
            .Equal(x => x.ResetPassword.Password)
            .WithMessage("Password and Confirm Password do not match");

        // Validate token
        RuleFor(x => x.ResetPassword.Token)
            .NotEmpty()
            .OverridePropertyName("Token");
    }
}
