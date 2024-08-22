using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Query.UpcomingExams;
/// <summary>
/// Record for the upcoming exams query.
/// </summary>
/// <param name="StudentId">The ID of the student.</param>
public record UpcominExamsQuery(string StudentId)
    : IRequest<OperationResult<List<ExamDto>>>;
