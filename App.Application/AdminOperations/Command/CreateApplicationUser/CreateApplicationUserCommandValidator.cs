using FluentValidation;

namespace App.Application.AdminOperations.Command.CreateApplicationUser;

/// <summary>
/// Represents a validator for the CreateApplicationUserCommand.
/// </summary>
public class CreateApplicationUserCommandValidator : AbstractValidator<CreateApplicationUserCommand>
{
    public CreateApplicationUserCommandValidator()
    {
        RuleFor(x => x.CreateApplicationUser.FirstName)
            .NotEmpty()
            .MaximumLength(225)
            .OverridePropertyName("FirstName");

        RuleFor(x => x.CreateApplicationUser.LastName)
            .NotEmpty()
            .MaximumLength(225)
            .OverridePropertyName("LastName");

        RuleFor(x => x.CreateApplicationUser.Email)
            .EmailAddress()
            .OverridePropertyName("Email");

        RuleFor(x => x.CreateApplicationUser.PhoneNumber)
            .Matches(@"^\d{6,11}$")
            .Unless(x => string.IsNullOrWhiteSpace(x.CreateApplicationUser.PhoneNumber))
            .OverridePropertyName("PhoneNumber");

        RuleFor(x => x.CreateApplicationUser.GuestUserComment)
            .MaximumLength(500)
            .OverridePropertyName("GuestUserComment");
    }
}
