namespace App.Domain.DTOs.Common.Response;

/// <summary>
/// Data transfer object representing the result of a patient examination.
/// </summary>
public class PatientExaminationResultDto
{
    /// <summary>
    /// Gets or sets the result of the BEWE (Basic Erosive Wear Examination) assessment.
    /// </summary>
    public BeweDto Bewe { get; init; }

    /// <summary>
    /// Gets or sets the result of the DMFT (Decayed, Missing, Filled Teeth) and DMFS (Decayed, Missing, Filled Surfaces) assessment.
    /// </summary>
    public DMFT_DMFSDto DMFT_DMFS { get; init; }

    /// <summary>
    /// Gets or sets the result of the API (Assessment of Periodontal Inflammation) and Bleeding assessment.
    /// </summary>
    public APIBleedingDto APIBleeding { get; init; }
}

