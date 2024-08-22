using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.DTOs.ExamDtos.Request;

/// <summary>
///  Represents the Practice DMFT and DMFS DTO.
/// </summary>
public class PracticeDMFT_DMFSDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the DMFT and DMFS assessment.
    /// </summary>
    public string ProstheticStatus { get; set; }
    
    /// <summary>
    ///  Gets or sets the DMFT (Decayed, Missing, Filled Teeth) result.
    /// </summary>
    public decimal DMFTResult { get; set; }
    
    /// <summary>
    ///   Gets or sets the DMFS (Decayed, Missing, Filled Surfaces) result.
    /// </summary>
    public decimal DMFSResult { get; set; }
    
    /// <summary>
    ///  Gets or sets comments related to the DMFT and DMFS assessment.
    /// </summary>
    public DMFT_DMFSAssessmentModel AssessmentModel { get; set; }
}
