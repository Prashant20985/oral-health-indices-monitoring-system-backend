using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Query.ExamResults;

/// <summary>
/// Query to fetch exam results.
/// </summary>
public record FetchExamResultsQuery(Guid ExamId)
    : IRequest<OperationResult<List<StudentExamResultResponseDto>>>;

