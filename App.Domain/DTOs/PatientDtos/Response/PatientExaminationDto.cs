using App.Domain.DTOs.Common.Response;

namespace App.Domain.DTOs.PatientDtos.Response;

/// <summary>
/// Data transfer object representing information about a patient examination.
/// </summary>
public class PatientExaminationDto : PatientDto
{
    /// <summary>
    /// Gets or sets a list of examination cards associated with the patient examination.
    /// </summary>
    public List<PatientExaminationCardDto> ExaminationCards { get; set; }
}
