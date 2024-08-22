using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.AdminOperations.Command.CreateApplicationUser;

/// <summary>
/// Represents a validator for the CreateApplicationUserCommand.
/// </summary>
public class CreateApplicationUserCommandValidator : AbstractValidator<CreateApplicationUserCommand>
{  /// <summary>
    /// Initializes a new instance of the <see cref="CreateApplicationUserCommandValidator"/> class.
    /// </summary>
    public CreateApplicationUserCommandValidator()
    {
        // Validate the FirstName property: it must not be empty and must have a maximum length of 225 characters.
        RuleFor(x => x.CreateApplicationUser.FirstName)
            .NotEmpty()
            .MaximumLength(225)
            .OverridePropertyName("FirstName");

        // Validate the LastName property: it must have a maximum length of 225 characters.
        RuleFor(x => x.CreateApplicationUser.LastName)
            .MaximumLength(225)
            .OverridePropertyName("LastName");

        // Validate the Email property: it must be a valid email address.
        RuleFor(x => x.CreateApplicationUser.Email)
            .EmailAddress()
            .OverridePropertyName("Email");

        // Validate the PhoneNumber property: it must match the specified pattern if not empty.
        RuleFor(x => x.CreateApplicationUser.PhoneNumber)
            .Matches(@"^\d{6,11}$")
            .Unless(x => string.IsNullOrWhiteSpace(x.CreateApplicationUser.PhoneNumber))
            .OverridePropertyName("PhoneNumber");

        // Validate the GuestUserComment property: it must have a maximum length of 500 characters.
        RuleFor(x => x.CreateApplicationUser.GuestUserComment)
            .MaximumLength(500)
            .OverridePropertyName("GuestUserComment");

        // Validate the Role property: it must be a valid role if not empty.
        RuleFor(x => x.CreateApplicationUser.Role)
            .Must(r => string.IsNullOrWhiteSpace(r) || Enum.IsDefined(typeof(Role), r))
            .WithMessage("Invalid Role input")
            .OverridePropertyName("Role");
    }
}
