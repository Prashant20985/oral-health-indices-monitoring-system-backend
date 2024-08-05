using App.Application.Core;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.PatientsNotInResearchGroups;

/// <summary>
/// Represents a query to fetch patients not in any research group.
/// </summary>
public record FetchPatientsNotInResearchGroupsQuery(string PatientName, string Email, int Page, int PageSize)
    : IRequest<OperationResult<PaginatedResearchGroupPatientResponseDto>>;
