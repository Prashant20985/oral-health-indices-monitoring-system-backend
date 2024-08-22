using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.Common.Response;

/// <summary>
///  Represents the Bleeding response DTO.
/// </summary>
public class BleedingResponseDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the API and Bleeding assessment.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the API (Assessment of Periodontal Inflammation) result.
    /// </summary>
    public int BleedingResult { get; init; }

    /// <summary>
    /// Gets or sets the Maxilla result.
    /// </summary>
    public int Maxilla { get; init; }

    /// <summary>
    /// Gets or sets the Mandible result.
    /// </summary>
    public int Mandible { get; init; }

    /// <summary>
    /// Gets the Doctor comment related to the Bleeding assessment.
    /// </summary>
    public string DoctorComment { get; init; }

    /// <summary>
    /// Gets Student Comment related to the Bleeding assessment.
    /// </summary>
    public string StudentComment { get; init; }

    /// <summary>
    /// Gets or sets the assessment model used for the Bleeding assessment.
    /// </summary>
    public APIBleedingAssessmentModel AssessmentModel { get; init; }
}
