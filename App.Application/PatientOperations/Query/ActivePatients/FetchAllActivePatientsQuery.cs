using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using MediatR;

namespace App.Application.PatientOperations.Query.ActivePatients;

/// <summary>
/// Represents a query to fetch all active patients based on optional name and email filters.
/// </summary>
public record FetchAllActivePatientsQuery(string Name, string Email)
    : IRequest<OperationResult<List<PatientDto>>>;
