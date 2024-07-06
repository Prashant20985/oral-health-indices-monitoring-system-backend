namespace App.Domain.DTOs.Common.Response;

/// <summary>
/// Represents the DMFT and DMFS result response DTO.
/// </summary>
public class DMFT_DMFSResultResponseDto
{
    /// <summary>
    /// Gets or sets the DMFT result.
    /// </summary>
    public decimal DMFTResult { get; init; }

    /// <summary>
    /// Gets or sets the DMFS result.
    /// </summary>
    public decimal DMFSResult { get; init; }
}
