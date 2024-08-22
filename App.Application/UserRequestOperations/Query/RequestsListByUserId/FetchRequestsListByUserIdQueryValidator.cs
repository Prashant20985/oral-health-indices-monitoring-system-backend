using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.UserRequestOperations.Query.RequestsListByUserId;

/// <summary>
/// Validator for the FetchRequestsListByUserIdQuery class.
/// </summary>
public class FetchRequestsListByUserIdQueryValidator : AbstractValidator<FetchRequestsListByUserIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the FetchRequestsListByUserIdQueryValidator class.
    /// </summary>
    public FetchRequestsListByUserIdQueryValidator()
    {
        // Validates the RequestStatus property of the UserRequestQuery.
        RuleFor(x => x.RequestStatus)
            .Must(r => string.IsNullOrWhiteSpace(r) || Enum.IsDefined(typeof(RequestStatus), r))
            .WithMessage("Invalid Status input")
            .OverridePropertyName("Request Status");
    }
}
