using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;

namespace App.Application.StudentExamOperations.CommonOperations.Query.ExamsList;

/// <summary>
/// Query for fetching a list of exams by group ID.
/// </summary>
public record FetchExamsListByGroupIdQuery(Guid GroupId)
    : IRequest<OperationResult<List<ExamDto>>>;

