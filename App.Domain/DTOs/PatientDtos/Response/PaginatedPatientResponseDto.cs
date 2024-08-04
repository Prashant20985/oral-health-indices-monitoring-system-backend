using App.Domain.DTOs.Common.Response;
namespace App.Domain.DTOs.PatientDtos.Response;

/// <summary>
/// Represents a paginated response DTO for patients.
/// </summary>
public class PaginatedPatientResponseDto
{
    /// <summary>
    /// Gets or sets the total count of patients.
    /// </summary>
    public int TotalPatientsCount { get; set; }

    /// <summary>
    /// Gets or sets the list of patients.
    /// </summary>
    public List<PatientResponseDto> Patients { get; set; }
}
