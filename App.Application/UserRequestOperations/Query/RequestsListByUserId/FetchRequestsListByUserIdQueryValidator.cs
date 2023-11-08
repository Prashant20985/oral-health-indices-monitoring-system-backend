using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.UserRequestOperations.Query.RequestsListByUserId;

/// <summary>
/// Validator for the FetchRequestsListByUserIdQuery class.
/// </summary>
public class FetchRequestsListByUserIdQueryValidator : AbstractValidator<FetchRequestsListByUserIdQuery>
{
    public FetchRequestsListByUserIdQueryValidator()
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
