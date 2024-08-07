using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Query.UpcomingExams;

/// <summary>
/// Retrieves upcoming exams for a student.
/// </summary>
public record UpcominExamsQuery(string StudentId)
    : IRequest<OperationResult<List<ExamDto>>>;
