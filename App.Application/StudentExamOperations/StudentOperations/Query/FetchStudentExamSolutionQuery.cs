using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Query;

/// <summary>
/// Fetches a practice patient examination card by student ID and exam ID.
/// </summary>
/// <param name="ExamId"></param>
/// <param name="StudentId"></param>
public record FetchStudentExamSolutionQuery(Guid ExamId, string StudentId)
    : IRequest<OperationResult<PracticePatientExaminationCardDto>>;

