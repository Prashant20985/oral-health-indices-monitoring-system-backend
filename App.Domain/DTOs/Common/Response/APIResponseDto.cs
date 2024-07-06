using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.Common.Response;

/// <summary>
/// Class representing the API DTO.
/// </summary>
public class APIResponseDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the API and Bleeding assessment.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the API (Assessment of Periodontal Inflammation) result.
    /// </summary>
    public int APIResult { get; init; }

    /// <summary>
    /// Gets or sets the Maxilla result.
    /// </summary>
    public int Maxilla { get; init; }

    /// <summary>
    /// Gets or sets the Mandible result.
    /// </summary>
    public int Mandible { get; init; }

    /// <summary>
    /// Gets Doctor Comment related to the API assessment.
    /// </summary>
    public string DoctorComment { get; init; }

    /// <summary>
    /// Gets Student Comment related to the API assessment.
    /// </summary>
    public string StudentComment { get; init; }

    /// <summary>
    /// Gets or sets the assessment model used for the Bleeding assessment.
    /// </summary>
    public APIBleedingAssessmentModel AssessmentModel { get; init; }
}
