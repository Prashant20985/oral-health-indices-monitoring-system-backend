using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create Bleeding Regular Mode Request DTO
/// </summary>
public class CreateBleedingRequestDto
{
    /// <summary>
    /// Gets or initializes the comment
    /// </summary>
    public string Comment { get; init; }

    /// <summary>
    /// Gets or initializes Bledding Assessment model
    /// </summary>
    public APIBleedingAssessmentModel AssessmentModel { get; init; }
}
