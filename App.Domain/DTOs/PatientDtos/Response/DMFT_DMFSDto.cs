using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.DTOs.PatientDtos.Response;

/// <summary>
/// Data transfer object representing information about DMFT (Decayed, Missing, Filled Teeth) and DMFS (Decayed, Missing, Filled Surfaces) assessment.
/// </summary>
public class DMFT_DMFSDto
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
    /// Gets or sets comments related to the DMFT and DMFS assessment.
    /// </summary>
    public string Comment { get; init; }

    /// <summary>
    /// Gets or sets the assessment model used for the DMFT and DMFS assessment.
    /// </summary>
    public DMFT_DMFSAssessmentModel AssessmentModel { get; init; }
}
