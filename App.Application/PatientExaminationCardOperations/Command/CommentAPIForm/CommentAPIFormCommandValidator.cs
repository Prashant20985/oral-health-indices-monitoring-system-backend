﻿using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CommentAPIForm;

/// <summary>
/// Validator for CommentAPIFormCommand
/// </summary>
public class CommentAPIFormCommandValidator : AbstractValidator<CommentAPIFormCommnand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentAPIFormCommandValidator"/> class.
    /// </summary>
    public CommentAPIFormCommandValidator()
    {
        RuleFor(x => x.CardId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Comment)
            .MaximumLength(500);
    }
}
