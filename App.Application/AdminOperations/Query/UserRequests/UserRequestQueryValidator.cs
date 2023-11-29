using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.AdminOperations.Query.UserRequests;

/// <summary>
/// Validator for the UserRequestQuery class.
/// </summary>
public class UserRequestQueryValidator : AbstractValidator<UserRequestQuery>
{
    public UserRequestQueryValidator()
    {
        /// <summary>
        /// Validates the RequestStatus property of the UserRequestQuery.
        /// </summary>
        RuleFor(x => x.RequestStatus)
            .Must(r => string.IsNullOrWhiteSpace(r) || Enum.IsDefined(typeof(RequestStatus), r))
            .WithMessage("Invalid Status input")
            .OverridePropertyName("Request Status");
    }
}
