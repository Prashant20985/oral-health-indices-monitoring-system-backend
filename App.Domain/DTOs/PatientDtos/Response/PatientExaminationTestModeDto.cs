namespace App.Domain.DTOs.PatientDtos.Response;

/// <summary>
/// Data transfer object representing information about the test mode of a patient examination.
/// </summary>
public class PatientExaminationTestModeDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the test mode of patient examination.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the doctor overseeing the examination in test mode.
    /// </summary>
    public string DoctorName { get; init; }

    /// <summary>
    /// Gets or sets the name of the student undergoing the examination in test mode.
    /// </summary>
    public string StudentName { get; init; }

    /// <summary>
    /// Gets or sets the date of the patient examination in test mode.
    /// </summary>
    public DateTime DateOfExamination { get; init; }

    /// <summary>
    /// Gets or sets the marks obtained by the student in the test mode examination.
    /// </summary>
    public decimal StudentMarks { get; init; }
}

