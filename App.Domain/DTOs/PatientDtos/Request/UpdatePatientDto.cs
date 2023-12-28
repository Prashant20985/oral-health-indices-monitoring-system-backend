namespace App.Domain.DTOs.PatientDtos.Request;

/// <summary>
/// Data transfer object representing information required for updating an existing patient.
/// </summary>
public class UpdatePatientDto
{
    /// <summary>
    /// Gets or sets the updated first name of the patient.
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Gets or sets the updated last name of the patient.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Gets or sets the updated gender of the patient.
    /// </summary>
    public string Gender { get; init; }

    /// <summary>
    /// Gets or sets the updated ethnic group of the patient.
    /// </summary>
    public string EthnicGroup { get; init; }

    /// <summary>
    /// Gets or sets the updated additional information about the patient's ethnic group.
    /// </summary>
    public string OtherGroup { get; init; }

    /// <summary>
    /// Gets or sets the updated number of years the patient has been in school.
    /// </summary>
    public int YearsInSchool { get; init; }

    /// <summary>
    /// Gets or sets the updated additional data related to the patient.
    /// </summary>
    public string OtherData { get; init; }

    /// <summary>
    /// Gets or sets the updated additional data related to the patient.
    /// </summary>
    public string OtherData2 { get; init; }

    /// <summary>
    /// Gets or sets the updated additional data related to the patient.
    /// </summary>
    public string OtherData3 { get; init; }

    /// <summary>
    /// Gets or sets the updated location of the patient.
    /// </summary>
    public string Location { get; init; }

    /// <summary>
    /// Gets or sets the updated age of the patient.
    /// </summary>
    public int Age { get; init; }
}
