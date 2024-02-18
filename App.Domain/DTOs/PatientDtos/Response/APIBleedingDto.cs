using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.PatientDtos.Response;

/// <summary>
/// Data transfer object representing information about API (Aproximal Plaque Index) and Bleeding.
/// </summary>
public class APIBleedingDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the API and Bleeding assessment.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the API (Assessment of Periodontal Inflammation) result.
    /// </summary>
    public decimal APIResult { get; init; }

    /// <summary>
    /// Gets or sets the Bleeding result associated with the assessment.
    /// </summary>
    public decimal BleedingResult { get; init; }

    /// <summary>
    /// Gets or sets comments related to the API and Bleeding assessment.
    /// </summary>
    public string Comments { get; init; }

    /// <summary>
    /// Gets or sets the assessment model used for the API and Bleeding assessment.
    /// </summary>
    public APIBleedingAssessmentModel AssessmentModel { get; init; }
}
