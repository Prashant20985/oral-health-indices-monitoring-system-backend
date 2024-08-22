using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.AdminOperations.Command.UpdateApplicationUser;

/// <summary>
/// Represents a validator for the UpdateApplicationUserCommand.
/// </summary>
public class UpdateApplicationUserCommandValidator : AbstractValidator<UpdateApplicationUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateApplicationUserCommandValidator"/> class.
    /// </summary>
    public UpdateApplicationUserCommandValidator()
    {
        
        // Rule to ensure the UserName property is not empty.
        RuleFor(x => x.UserName)
            .NotEmpty();

        // Rule to ensure the FirstName property is not empty and has a maximum length of 50 characters.
        RuleFor(x => x.UpdateApplicationUser.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .OverridePropertyName("FirstName");

        // Rule to ensure the LastName property has a maximum length of 50 characters.
        RuleFor(x => x.UpdateApplicationUser.LastName)
            .MaximumLength(50)
            .OverridePropertyName("LastName");

        // Rule to ensure the PhoneNumber property matches the specified pattern or is null/whitespace.
        RuleFor(x => x.UpdateApplicationUser.PhoneNumber)
            .Matches(@"^\d{6,11}$")
            .Unless(x => string.IsNullOrWhiteSpace(x.UpdateApplicationUser.PhoneNumber))
            .OverridePropertyName("PhoneNumber");

        // Rule to ensure the GuestUserComment property has a maximum length of 200 characters.
        RuleFor(x => x.UpdateApplicationUser.GuestUserComment)
            .MaximumLength(200)
            .OverridePropertyName("GuestUserComment");

        // Rule to ensure the Role property is a valid Role enum value.
        RuleFor(x => x.UpdateApplicationUser.Role)
            .Must(BeAVAlidRole)
            .WithMessage("Invalid Role Selection")
            .OverridePropertyName("Role");
    }

    /// <summary>
    /// Checks if the provided role is a valid Role enum value.
    /// </summary>
    /// <param name="role">The role value to be checked.</param>
    /// <returns>True if the role is valid; otherwise, false.</returns>
    private bool BeAVAlidRole(string role)
    {
        if (Enum.TryParse(role, out Role result))
        {
            return Enum.IsDefined(typeof(Role), result);
        }

        return false;
    }
}
