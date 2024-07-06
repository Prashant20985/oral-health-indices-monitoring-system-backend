namespace App.Domain.DTOs.Common.Response;

/// <summary>
/// Data transfer object representing the result of a patient examination.
/// </summary>
public class PracticePatientExaminationResultResponseDto
{
    /// <summary>
    /// Gets or sets the result of the BEWE (Basic Erosive Wear Examination) assessment.
    /// </summary>
    public PracticeBeweResponseDto Bewe { get; init; }

    /// <summary>
    /// Gets or sets the result of the DMFT (Decayed, Missing, Filled Teeth) and DMFS (Decayed, Missing, Filled Surfaces) assessment.
    /// </summary>
    public PracticeDMFT_DMFSRespnseDto DMFT_DMFS { get; init; }

    /// <summary>
    /// Gets or sets the result of the API (Assessment of Periodontal Inflammation) assessment.
    /// </summary>
    public PracticeAPIResponseDto API { get; init; }

    /// <summary>
    /// Gets or sets the result of the bleeding assessment.
    /// </summary>
    public PracticeBleedingResponseDto Bleeding { get; init; }
}

