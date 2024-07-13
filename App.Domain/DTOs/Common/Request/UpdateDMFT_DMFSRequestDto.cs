using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Request to update DMFT/DMFS form
/// </summary>
public class UpdateDMFT_DMFSRequestDto
{
    /// <summary>
    /// Gets or initializes the DMFT/DMFS Assessment Model
    /// </summary>
    public DMFT_DMFSAssessmentModel AssessmentModel { get; init; }
    
    /// <summary>
    /// Gets or initializes the DMFT/DMFS Prosthetic Status
    /// </summary>
    public string ProstheticStatus { get; init; }

    /// <summary>
    /// Gets or initializes the DMFT Result
    /// </summary>
    public decimal DMFTResult { get; init; }

    /// <summary>
    /// Gets or initializes the DMFS Result
    /// </summary>
    public decimal DMFSResult { get; init; }
}
