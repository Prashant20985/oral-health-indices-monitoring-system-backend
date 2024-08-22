using FluentValidation;

namespace App.Application.AccountOperations.Command.ChangePassword;

/// <summary>
/// Validator for the ChangePasswordCommand.
/// </summary>
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{ 
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordCommandValidator"/> class.
    /// Sets up validation rules for the ChangePasswordCommand.
    /// </summary>
    public ChangePasswordCommandValidator()
    {
        
        // Validates that the Email property of the ChangePasswordDto is a valid email address.
        RuleFor(x => x.ChangePassword.Email)
            .EmailAddress()
            .OverridePropertyName("Email");
      
        // Validates that the CurrentPassword property of the ChangePasswordDto is not empty and has a minimum length of 8 characters.
        RuleFor(x => x.ChangePassword.CurrentPassword)
            .NotEmpty()
            .MinimumLength(8)
            .OverridePropertyName("CurrentPassword");
     
        // Validates that the NewPassword property of the ChangePasswordDto is not empty and has a minimum length of 8 characters.
        RuleFor(x => x.ChangePassword.NewPassword)
            .NotEmpty()
            .MinimumLength(8)
            .OverridePropertyName("NewPassword");
    }
}
