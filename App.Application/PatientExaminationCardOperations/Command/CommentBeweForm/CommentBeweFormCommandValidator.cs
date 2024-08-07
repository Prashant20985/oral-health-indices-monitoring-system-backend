﻿using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CommentBeweForm;

/// <summary>
/// Validator for the CommentBeweFormCommand
/// </summary>
public class CommentBeweFormCommandValidator : AbstractValidator<CommentBeweFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentBeweFormCommandValidator"/> class.
    /// </summary>
    public CommentBeweFormCommandValidator()
    {
        RuleFor(x => x.CardId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Comment)
            .MaximumLength(500);
    }
}
