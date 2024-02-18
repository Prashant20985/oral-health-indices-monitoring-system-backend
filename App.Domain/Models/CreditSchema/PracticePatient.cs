using App.Domain.Models.Enums;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents a practice patient.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PracticePatient"/> class.
/// </remarks>
/// <param name="firstName">The first name of the patient.</param>
/// <param name="lastName">The last name of the patient.</param>
/// <param name="email">The email of the patient.</param>
/// <param name="gender">The gender of the patient.</param>
/// <param name="ethnicGroup">The ethnic group of the patient.</param>
/// <param name="location">The location of the patient.</param>
/// <param name="age">The age of the patient.</param>
/// <param name="otherGroup">Another group related to the patient.</param>
/// <param name="otherData">Additional data related to the patient.</param>
/// <param name="otherData2">Additional data related to the patient.</param>
/// <param name="otherData3">Additional data related to the patient.</param>
/// <param name="yearsInSchool">The number of years the patient has been in school.</param>
public class PracticePatient(string firstName,
    string lastName,
    string email,
    Gender gender,
    string ethnicGroup,
    string location,
    int age,
    string otherGroup,
    string otherData,
    string otherData2,
    string otherData3,
    int yearsInSchool)
{

    /// <summary>
    /// Gets the unique identifier of the practice patient.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets the first name of the patient.
    /// </summary>
    public string FirstName { get; private set; } = firstName;

    /// <summary>
    /// Gets the last name of the patient.
    /// </summary>
    public string LastName { get; private set; } = lastName;

    /// <summary>
    /// Gets the email of the patient.
    /// </summary>
    public string Email { get; private set; } = email;

    /// <summary>
    /// Gets the gender of the patient.
    /// </summary>
    public Gender Gender { get; private set; } = gender;

    /// <summary>
    /// Gets the ethnic group of the patient.
    /// </summary>
    public string EthnicGroup { get; private set; } = ethnicGroup;

    /// <summary>
    /// Gets another group related to the patient.
    /// </summary>
    public string OtherGroup { get; private set; } = otherGroup;

    /// <summary>
    /// Gets the number of years the patient has been in school.
    /// </summary>
    public int YearsInSchool { get; private set; } = yearsInSchool;

    /// <summary>
    /// Gets additional data related to the patient.
    /// </summary>
    public string OtherData { get; private set; } = otherData;

    /// <summary>
    /// Gets additional data related to the patient.
    /// </summary>
    public string OtherData2 { get; private set; } = otherData2;

    /// <summary>
    /// Gets additional data related to the patient.
    /// </summary>
    public string OtherData3 { get; private set; } = otherData3;

    /// <summary>
    /// Gets the location of the patient.
    /// </summary>
    public string Location { get; private set; } = location;

    /// <summary>
    /// Gets the age of the patient.
    /// </summary>
    public int Age { get; private set; } = age;

    /// <summary>
    /// Gets or sets the practice patient examination card associated with this patient.
    /// </summary>
    public virtual PracticePatientExaminationCard PracticePatientExaminationCard { get; set; }
}

