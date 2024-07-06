namespace App.Domain.DTOs.Common.Response;

/// <summary>
/// Data transfer object representing the result of a patient examination.
/// </summary>
public class PatientExaminationResultDto
{
    /// <summary>
    /// Gets or sets the result of the BEWE (Basic Erosive Wear Examination) assessment.
    /// </summary>
    public BeweResponseDto Bewe { get; set; }

    /// <summary>
    /// Gets or sets the result of the DMFT (Decayed, Missing, Filled Teeth) and DMFS (Decayed, Missing, Filled Surfaces) assessment.
    /// </summary>
    public DMFT_DMFSResponseDto DMFT_DMFS { get; set; }

    /// <summary>
    /// Gets or sets the result of the API (Assessment of Periodontal Inflammation) assessment.
    /// </summary>
    public APIResponseDto API { get; set; }

    /// <summary>
    /// Gets or sets the result of the bleeding assessment.
    /// </summary>
    public BleedingResponseDto Bleeding { get; set; }
}

