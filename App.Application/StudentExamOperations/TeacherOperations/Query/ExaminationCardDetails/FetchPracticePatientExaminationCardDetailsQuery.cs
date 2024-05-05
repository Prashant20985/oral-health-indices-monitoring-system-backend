using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardDetails;

/// <summary>
/// Command to fetch the details of a practice patient examination card
/// </summary>
public record FetchPracticePatientExaminationCardDetailsQuery(Guid PracticePatientExaminationCardId)
    : IRequest<OperationResult<PracticePatientExaminationCardDto>>;
