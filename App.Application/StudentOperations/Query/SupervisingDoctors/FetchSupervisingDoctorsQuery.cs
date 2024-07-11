using App.Application.Core;
using App.Domain.DTOs.SuperviseDtos.Response;
using MediatR;

namespace App.Application.StudentOperations.Query.SupervisingDoctors;

/// <summary>
/// Queries the supervising doctors of a student.
/// </summary>
/// <param name="StudentId"></param>
public record FetchSupervisingDoctorsQuery(string StudentId)
    : IRequest<OperationResult<List<SupervisingDoctorResponseDto>>>;
