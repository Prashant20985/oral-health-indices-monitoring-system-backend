using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create DMFT-DMFS Regular Mode Request DTO
/// </summary>
public class CreateDMFT_DMFSRequestDto
{
    /// <summary>
    /// Gets or initializes the comment
    /// </summary>
    public string Comment { get; init; }

    /// <summary>
    /// Gets or initializes the prosthetic status
    /// </summary>
    public string ProstheticStatus { get; init; }

    /// <summary>
    /// Gets or initializes the DMFT result
    /// </summary>
    public decimal DMFTResult { get; set; }

    /// <summary>
    /// Gets or initializes the DMFS result
    /// </summary>
    public decimal DMFSResult { get; set; }

    /// <summary>
    /// Gets or initializes the DMFT-DMFS assessment model
    /// </summary>
    public DMFT_DMFSAssessmentModel AssessmentModel { get; init; }
}
