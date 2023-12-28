namespace App.Domain.DTOs.PatientDtos.Request;

/// <summary>
/// Data transfer object representing information required for creating a new patient.
/// </summary>
public class CreatePatientDto
{
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
}
