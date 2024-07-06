namespace App.Domain.DTOs.Common.Response;

/// <summary>
/// Represents the API result response DTO.
/// </summary>
public class APIResultResponseDto
{
    /// <summary>
    /// Gets or sets the API result.
    /// </summary>
    public int APIResult { get; init; }

    /// <summary>
    /// Gets or sets the maxilla result.
    /// </summary>
    public int Maxilla { get; init; }

    /// <summary>
    /// Gets or sets the mandible result.
    /// </summary>
    public int Mandible { get; init; }
}
