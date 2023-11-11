using FluentValidation;

namespace App.Application.AdminOperations.Command.UpdateRequestStatusToCompleted
{
    /// <summary>
    /// Validator for the UpdateRequestStatusToCompletedCommand.
    /// </summary>
    public class UpdateRequestStatusToCompletedCommandValidator : AbstractValidator<UpdateRequestStatusToCompletedCommand>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateRequestStatusToCompletedCommandValidator class.
        /// </summary>
        public UpdateRequestStatusToCompletedCommandValidator()
        {
            // Validate the maximum length of the AdminComment property.
            RuleFor(x => x.AdminComment)
                .MaximumLength(500);
        }
    }
}