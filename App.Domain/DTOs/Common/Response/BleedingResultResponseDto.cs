namespace App.Domain.DTOs.Common.Response;

/// <summary>
/// Represents the Bleeding result response DTO.
/// </summary>
public class BleedingResultResponseDto
{
    /// <summary>
    /// Gets or sets the Bleeding result.
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
}
