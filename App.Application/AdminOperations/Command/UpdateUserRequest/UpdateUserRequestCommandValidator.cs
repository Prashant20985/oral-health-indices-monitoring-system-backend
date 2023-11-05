using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.AdminOperations.Command.UpdateUserRequest;

/// <summary>
/// Represents a validator for the UpdateUserRequestStatusCommand.
/// </summary>
public class UpdateUserRequestCommandValidator : AbstractValidator<UpdateUserRequestStatusCommand>
{
    public UpdateUserRequestCommandValidator()
    {
        RuleFor(x => x.AdminComment)
            .MaximumLength(70);

        RuleFor(x => x.RequestStatus)
            .Must(beAValidStatus)
            .WithMessage("Invalid status selection")
            .OverridePropertyName("RequestStatus");
    }

    /// <summary>
    /// Checks if the provided request status is a valid RequestStatus enum value.
    /// </summary>
    /// <param name="status">The status value to be checked.</param>
    /// <returns>True if the status is valid; otherwise, false.</returns>
    private bool beAValidStatus(string status)
    {
        if (Enum.TryParse(status, out RequestStatus result))
        {
            return Enum.IsDefined(typeof(RequestStatus), status);
        }
        return false;
    }
}
