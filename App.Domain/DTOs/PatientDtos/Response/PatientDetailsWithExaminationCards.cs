using App.Domain.DTOs.Common.Response;

namespace App.Domain.DTOs.PatientDtos.Response;

/// <summary>
/// Patient details with examination cards Data Transfer Object, inherits from PatientExaminationCardDto
/// </summary>
public class PatientDetailsWithExaminationCards : PatientExaminationCardDto
{
    /// <summary>
    /// Patient details
    /// </summary>
    public PatientResponseDto Patient { get; init; }
}
