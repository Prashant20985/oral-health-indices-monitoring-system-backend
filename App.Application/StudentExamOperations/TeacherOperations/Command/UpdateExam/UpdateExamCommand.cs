using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.DTOs.ExamDtos.Response;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.UpdateExam;

/// <summary>
/// Command to update an exam.
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdateExamCommand(Guid ExamId, UpdateExamDto UpdateExam)
    : IRequest<OperationResult<ExamDto>>;