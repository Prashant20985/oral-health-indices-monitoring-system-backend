using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using MediatR;

namespace App.Application.PatientOperations.Query.ArchivedPatients;

/// <summary>
/// Represents a query to fetch all archived patients with optional name and email filters.
/// </summary>
public record FetchAllArchivedPatientsQuery(string Name, string Email)
    : IRequest<OperationResult<List<PatientDto>>>;

