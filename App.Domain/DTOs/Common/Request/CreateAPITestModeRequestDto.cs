using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create API Test Mode Request DTO
/// </summary>
public class CreateAPITestModeRequestDto
{
    /// <summary>
    /// Gets or initalizes the student comment.
    /// </summary>
    public string StudentComment { get; init; }

    /// <summary>
    /// Gets or initalizes the API result.
    /// </summary>
    public int APIResult { get; init; }

    /// <summary>
    /// Gets or initalizes the maxilla.
    /// </summary>
    public int Maxilla { get; init; }

    /// <summary>
    /// Gets or initalizes the mandible.
    /// </summary>
    public int Mandible { get; init; }

    /// <summary>
    /// Gets or initalizes the API assessment model.
    /// </summary>
    public APIBleedingAssessmentModel APIAssessmentModel { get; init; }
}
