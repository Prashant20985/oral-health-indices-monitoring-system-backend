using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.DTOs.Common.Response;

/// <summary>
/// Data transfer object representing information about DMFT (Decayed, Missing, Filled Teeth) and DMFS (Decayed, Missing, Filled Surfaces) assessment.
/// </summary>
public class DMFT_DMFSResponseDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the DMFT and DMFS assessment.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the DMFT (Decayed, Missing, Filled Teeth) result.
    /// </summary>
    public decimal DMFTResult { get; init; }

    /// <summary>
    /// Gets or sets the DMFS (Decayed, Missing, Filled Surfaces) result.
    /// </summary>
    public decimal DMFSResult { get; init; }

    /// <summary>
    /// Gets Doctor comments related to the DMFT and DMFS assessment.
    /// </summary>
    public string DoctorComment { get; init; }

    /// <summary>
    /// Gets Student Comment related to the DMFT and DMFS assessment.
    /// </summary>
    public string StudentComment { get; init; }

    /// <summary>
    /// Gets or sets the assessment model used for the DMFT and DMFS assessment.
    /// </summary>
    public DMFT_DMFSAssessmentModel AssessmentModel { get; init; }
}
