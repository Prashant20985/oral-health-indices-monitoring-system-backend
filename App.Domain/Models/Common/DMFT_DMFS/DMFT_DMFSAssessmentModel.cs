namespace App.Domain.Models.Common.DMFT_DMFS;

/// <summary>
/// Represents a model for DMFT (Decayed, Missing, Filled Teeth) and DMFS (Decayed, Missing, Filled Surfaces) assessment.
/// </summary>
public class DMFT_DMFSAssessmentModel
{
    /// <summary>
    /// Gets or sets the assessment data for the upper section of the oral cavity.
    /// </summary>
    public UpperMouth UpperMouth { get; set; }

    /// <summary>
    /// Gets or sets the assessment data for the lower section of the oral cavity.
    /// </summary>
    public LowerMouth LowerMouth { get; set; }
}

