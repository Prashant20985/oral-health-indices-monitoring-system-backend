using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.StudentsSupervised;

/// <summary>
/// Fetches all students supervised by a doctor
/// </summary>
public record FetchStudentsSupervisedQuery(string DoctorId, string StudentName, string Email, int Page, int PageSize)
    : IRequest<OperationResult<PaginatedStudentResponseDto>>;
