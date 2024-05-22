using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.DTOs.ExamDtos.Response;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.PublishExam;

/// <summary>
/// Command to publish an exam.
/// </summary>
[OralEhrContextUnitOfWork]
public record PublishExamCommand(PublishExamDto PublishExam)
    : IRequest<OperationResult<ExamDto>>;

