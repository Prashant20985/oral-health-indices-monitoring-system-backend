using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.AdminOperations.Command.UpdateApplicationUser;

/// <summary>
/// Represents a validator for the UpdateApplicationUserCommand.
/// </summary>
public class UpdateApplicationUserCommandValidator : AbstractValidator<UpdateApplicationUserCommand>
{
    public UpdateApplicationUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty();

        RuleFor(x => x.UpdateApplicationUser.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .OverridePropertyName("FirstName");

        RuleFor(x => x.UpdateApplicationUser.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .OverridePropertyName("LastName");

        RuleFor(x => x.UpdateApplicationUser.PhoneNumber)
            .Matches(@"^\d{6,11}$")
            .Unless(x => string.IsNullOrWhiteSpace(x.UpdateApplicationUser.PhoneNumber))
            .OverridePropertyName("PhoneNumber");

        RuleFor(x => x.UpdateApplicationUser.GuestUserComment)
            .MaximumLength(200)
            .OverridePropertyName("GuestUserComment");

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
