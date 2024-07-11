using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.UnsuperviseStudent;

/// <summary>
/// Command to unsupervise a student
/// </summary>
[OralEhrContextUnitOfWork]
public record UnsuperviseStudentCommand(string DoctorId, string StudentId)
    : IRequest<OperationResult<Unit>>;
