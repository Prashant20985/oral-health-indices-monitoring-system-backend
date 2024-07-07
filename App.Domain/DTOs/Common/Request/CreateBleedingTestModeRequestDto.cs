using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create Bleeding Test Mode Request DTO
/// </summary>
public class CreateBleedingTestModeRequestDto
{
    /// <summary>
    /// Gets or initializes the student comment
    /// </summary>
    public string StudentComment { get; set; }

    /// <summary>
    /// Gets or initializes the bleeding result
    /// </summary>
    public int BleedingResult { get; set; }

    /// <summary>
    /// Gets or initializes the Maxilla
    /// </summary>
    public int Maxilla { get; set; }

    /// <summary>
    /// Gets or initializes the Mandible
    /// </summary>
    public int Mandible { get; set; }

    /// <summary>
    /// Gets or initializes the Bleeding Assessment model
    /// </summary>
    public APIBleedingAssessmentModel BleedingAssessmentModel { get; set; }
}
