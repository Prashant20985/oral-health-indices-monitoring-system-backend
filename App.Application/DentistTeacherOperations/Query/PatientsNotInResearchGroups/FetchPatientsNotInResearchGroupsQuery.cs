using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.PatientsNotInResearchGroups;

/// <summary>
/// Represents a query to fetch patients not in any research group.
/// </summary>
public record FetchPatientsNotInResearchGroupsQuery(string PatientName, string Email)
    : IRequest<OperationResult<List<ResearchGroupPatientDto>>>;
