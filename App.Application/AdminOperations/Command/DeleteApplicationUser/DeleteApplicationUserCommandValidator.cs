using FluentValidation;

namespace App.Application.AdminOperations.Command.DeleteApplicationUser;

/// <summary>
/// Represents a validator for the DeleteApplicationUserCommand.
/// </summary>
public class DeleteApplicationUserCommandValidator : AbstractValidator<DeleteApplicationUserCommand>
{
    public DeleteApplicationUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty();

        RuleFor(x => x.DeleteComment)
            .NotEmpty()
            .MaximumLength(500);
    }
}
