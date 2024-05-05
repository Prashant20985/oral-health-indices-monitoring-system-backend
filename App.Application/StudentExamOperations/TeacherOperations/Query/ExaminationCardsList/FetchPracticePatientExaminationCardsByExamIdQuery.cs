using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardsList;

/// <summary>
/// Query to fetch the practice patient examination cards by exam id.
/// </summary>
public record FetchPracticePatientExaminationCardsByExamIdQuery(Guid ExamId)
    : IRequest<OperationResult<List<PracticePatientExaminationCardDto>>>;
