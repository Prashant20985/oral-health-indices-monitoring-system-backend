namespace App.Domain.DTOs.ResearchGroupDtos.Response;

/// <summary>
/// Repesents a paginated response of research group patients.
/// </summary>
public class PaginatedResearchGroupPatientResponseDto
{
    /// <summary>
    /// Gets or sets the total number of patients.
    /// </summary>
    public int TotalNumberOfPatients { get; set; }

    /// <summary>
    /// Gets or sets the patients.
    /// </summary>
    public List<ResearchGroupPatientResponseDto> Patients { get; set; }
}
