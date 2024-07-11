using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.StudentsNotSupervised;

/// <summary>
/// Fetches all students not supervised by a doctor
/// </summary>
public record FetchStudentsNotSupervisedQuery(string DoctorId)
    : IRequest<OperationResult<List<StudentResponseDto>>>;
