using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentBleedingForm;

/// <summary>
/// Command for adding a comment to the bleeding form
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentBleedingFormCommand(Guid CardId, string DoctorComment)
    : IRequest<OperationResult<Unit>>;
