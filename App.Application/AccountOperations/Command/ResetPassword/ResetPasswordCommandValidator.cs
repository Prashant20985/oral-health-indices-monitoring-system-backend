using FluentValidation;

namespace App.Application.AccountOperations.Command.ResetPassword;

/// <summary>
/// Validator for the ResetPasswordCommand.
/// </summary>
public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.ResetPassword.Email)
            .EmailAddress()
            .OverridePropertyName("Email");

        RuleFor(x => x.ResetPassword.Password)
            .NotEmpty()
            .MinimumLength(8)
            .OverridePropertyName("Password");

        RuleFor(x => x.ResetPassword.ConfirmPassword)
            .Equal(x => x.ResetPassword.Password)
            .WithMessage("Password and Confirm Password do not match");

        RuleFor(x => x.ResetPassword.Token)
            .NotEmpty()
            .OverridePropertyName("Token");
    }
}
