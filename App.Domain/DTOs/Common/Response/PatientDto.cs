namespace App.Domain.DTOs.Common.Response;


/// <summary>
/// Data transfer object representing information about a patient.
/// </summary>
public class PatientDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the patient examination.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the first name of the patient.
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Gets or sets the last name of the patient.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Gets or sets the email address of the patient.
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Gets or sets the gender of the patient.
    /// </summary>
    public string Gender { get; init; }

    /// <summary>
    /// Gets or sets the ethnic group of the patient.
    /// </summary>
    public string EthnicGroup { get; init; }

    /// <summary>
    /// Gets or sets additional information about the patient's ethnic group.
    /// </summary>
    public string OtherGroup { get; init; }

    /// <summary>
    /// Gets or sets the number of years the patient has been in school.
    /// </summary>
    public int YearsInSchool { get; init; }

    /// <summary>
    /// Gets or sets additional data related to the patient.
    /// </summary>
    public string OtherData { get; init; }

    /// <summary>
    /// Gets or sets additional data related to the patient.
    /// </summary>
    public string OtherData2 { get; init; }

    /// <summary>
    /// Gets or sets additional data related to the patient.
    /// </summary>
    public string OtherData3 { get; init; }

    /// <summary>
    /// Gets or sets the location of the patient.
    /// </summary>
    public string Location { get; init; }

    /// <summary>
    /// Gets or sets the age of the patient.
    /// </summary>
    public int Age { get; init; }

    /// <summary>
    /// Gets or sets the date and time when the patient examination was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the patient examination is archived.
    /// </summary>
    public bool IsArchived { get; init; }

    /// <summary>
    /// Gets or sets a comment associated with archiving the patient examination.
    /// </summary>
    public string ArchiveComment { get; init; }

    /// <summary>
    /// Gets or sets the name of the doctor associated with the patient examination.
    /// </summary>
    public string DoctorName { get; init; }

    /// <summary>
    /// Gets or sets the name of the research group associated with the patient examination.
    /// </summary>
    public string ResearchGroupName { get; init; }
}
