using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;

namespace App.Application.StudentExamOperations.CommonOperations.Query.ExamDetails;

/// <summary>
/// Query for fetching exam details.
/// </summary>
public record FetchExamDetailsQuery(Guid ExamId) : IRequest<OperationResult<ExamDto>>;

