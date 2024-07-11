using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.SuperviseStudent;

/// <summary>
/// Command to supervise a student by a doctor.
/// </summary>
[OralEhrContextUnitOfWork]
public record SuperviseStudentCommand(string DoctorId, string StudentId)
    : IRequest<OperationResult<Unit>>;
