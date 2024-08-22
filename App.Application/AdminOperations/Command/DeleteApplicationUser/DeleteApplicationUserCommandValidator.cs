using FluentValidation;

namespace App.Application.AdminOperations.Command.DeleteApplicationUser;

/// <summary>
/// Represents a validator for the DeleteApplicationUserCommand.
/// </summary>
public class DeleteApplicationUserCommandValidator : AbstractValidator<DeleteApplicationUserCommand>
{  /// <summary>
    /// Initializes a new instance of the <see cref="DeleteApplicationUserCommandValidator"/> class.
    /// </summary>
    public DeleteApplicationUserCommandValidator()
    {
        // Rule to ensure the UserName property is not empty.
        RuleFor(x => x.UserName)
            .NotEmpty();

        // Rule to ensure the DeleteComment property is not empty and has a maximum length of 500 characters.
        RuleFor(x => x.DeleteComment)
            .NotEmpty()
            .MaximumLength(500);
    }
}
