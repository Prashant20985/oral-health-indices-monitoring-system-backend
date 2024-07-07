using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create DMFT-DMFS Test Mode Request DTO
/// </summary>
public class CreateDMFT_DMFSTestModeRequestDto
{
    /// <summary>
    /// Gets or initializes the student comment
    /// </summary>
    public string StudentComment { get; init; }

    /// <summary>
    /// Gets or initializes the DMFT result
    /// </summary>
    public decimal DMFTResult { get; init; }

    /// <summary>
    /// Gets or initializes the DMFS result
    /// </summary>
    public decimal DMFSResult { get; init; }

    /// <summary>
    /// Gets or initializes the DMFT-DMFS assessment model
    /// </summary>
    public DMFT_DMFSAssessmentModel DMFT_DMFSAssessmentModel { get; init; }
}
