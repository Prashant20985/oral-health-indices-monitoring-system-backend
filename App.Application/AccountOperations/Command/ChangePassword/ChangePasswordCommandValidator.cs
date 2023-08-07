using FluentValidation;

namespace App.Application.AccountOperations.Command.ChangePassword;

/// <summary>
/// Validator for the ChangePasswordCommand.
/// </summary>
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.ChangePassword.Email)
            .EmailAddress()
            .OverridePropertyName("Email");

        RuleFor(x => x.ChangePassword.CurrentPassword)
            .NotEmpty()
            .MinimumLength(8)
            .OverridePropertyName("CurrentPassword");

        RuleFor(x => x.ChangePassword.NewPassword)
            .NotEmpty()
            .MinimumLength(8)
            .OverridePropertyName("NewPassword");
    }
}
