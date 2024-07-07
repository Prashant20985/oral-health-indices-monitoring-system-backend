using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create API Regular Mode Request DTO
/// </summary>
public class CreateAPIRegularModeRequestDto
{
    /// <summary>
    /// Gets or initializes the comment.
    /// </summary>
    public string Comment { get; init; }

    /// <summary>
    /// Gets or initializes the API Assessment model.
    /// </summary>
    public APIBleedingAssessmentModel APIAssessmentModel { get; init; }
}
