using App.Domain.Models.Common.Bewe;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create Bewe Regular Mode Request Data Transfer Object.
/// </summary>
public class CreateBeweRequestDto
{
    /// <summary>
    /// Gets or initializes the comment.
    /// </summary>
    public string Comment { get; init; }

    /// <summary>
    /// Gets or initializes the Bewe Assessment model.
    /// </summary>
    public BeweAssessmentModel AssessmentModel { get; init; }
}
