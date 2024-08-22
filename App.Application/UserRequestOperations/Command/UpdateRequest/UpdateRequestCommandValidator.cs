using FluentValidation;

namespace App.Application.UserRequestOperations.Command.UpdateRequest;

/// <summary>
/// Validator for the UpdateRequestCommand class.
/// </summary>
public class UpdateRequestCommandValidator : AbstractValidator<UpdateRequestCommand>
{
    public UpdateRequestCommandValidator()
    {
        // Validates the Title property of the UpdateRequestCommand.
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .MaximumLength(500);

        // Validates the Description property of the UpdateRequestCommand.
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .Unless(x => string.IsNullOrEmpty(x.Description));
    }
}
