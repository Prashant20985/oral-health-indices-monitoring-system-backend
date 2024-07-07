using App.Domain.Models.Common.Bewe;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create Bewe Test Mode Request Data Transfer Object.
/// </summary>
public class CreateBeweTestModeRequestDto
{
    /// <summary>
    /// Gets or initializes the student comment.
    /// </summary>
    public string StudentComment { get; set; }

    /// <summary>
    /// Gets or initializes the Bewe result.
    /// </summary>
    public decimal BeweResult { get; set; }

    /// <summary>
    /// Gets or initializes the Bewe assessment model.
    /// </summary>
    public BeweAssessmentModel BeweAssessmentModel { get; set; }
}
