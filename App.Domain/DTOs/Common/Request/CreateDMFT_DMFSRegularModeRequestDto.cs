using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.DTOs.Common.Request;

/// <summary>
/// Create DMFT-DMFS Regular Mode Request DTO
/// </summary>
public class CreateDMFT_DMFSRegularModeRequestDto
{
    /// <summary>
    /// Gets or initializes the comment
    /// </summary>
    public string Comment { get; init; }

    /// <summary>
    /// Gets or initializes the DMFT-DMFS assessment model
    /// </summary>
    public DMFT_DMFSAssessmentModel DMFT_DMFSAssessmentModel { get; init; }
}
