using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using MediatR;

namespace App.Application.PatientOperations.Query.ActivePatientsByDoctorId;

/// <summary>
/// Represents a query to fetch all active patients by doctor ID, with optional name and email filters.
/// </summary>
public record FetchAllActivePatientsByDoctorIdQuery(string DoctorId, string Name, string Email)
    : IRequest<OperationResult<List<PatientResponseDto>>>;
