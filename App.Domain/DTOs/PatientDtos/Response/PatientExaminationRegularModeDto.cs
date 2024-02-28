namespace App.Domain.DTOs.PatientDtos.Response;

/// <summary>
/// Data transfer object representing information about the regular mode of a patient examination.
/// </summary>
public class PatientExaminationRegularModeDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the regular mode of patient examination.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the doctor conducting the examination in regular mode.
    /// </summary>
    public string DoctorName { get; init; }

    /// <summary>
    /// Gets or sets the date of the patient examination in regular mode.
    /// </summary>
    public DateTime DateOfExamination { get; init; }
}
