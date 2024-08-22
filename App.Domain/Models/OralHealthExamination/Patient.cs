using App.Domain.Models.Enums;
using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;

/// <summary>
/// Represents a patient entity with information about personal details, examinations, and archival status.
/// </summary>
public class Patient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Patient"/> class.
    /// </summary>
    /// <param name="firstName">The first name of the patient.</param>
    /// <param name="lastName">The last name of the patient.</param>
    /// <param name="email">The email of the patient.</param>
    /// <param name="gender">The gender of the patient.</param>
    /// <param name="ethnicGroup">The ethnic group of the patient.</param>
    /// <param name="location">The location of the patient.</param>
    /// <param name="age">The age of the patient.</param>
    /// <param name="otherGroup">Other group information of the patient.</param>
    /// <param name="otherData">Other data related to the patient.</param>
    /// <param name="otherData2">Additional data related to the patient.</param>
    /// <param name="otherData3">More additional data related to the patient.</param>
    /// <param name="yearsInSchool">The number of years the patient has been in school.</param>
    /// <param name="doctorId">The ID of the doctor associated with the patient.</param>
    public Patient(string firstName, string lastName, string email, Gender gender, string ethnicGroup, string location,
        int age, string otherGroup, string otherData, string otherData2, string otherData3, int yearsInSchool, string doctorId)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Gender = gender;
        EthnicGroup = ethnicGroup;
        OtherGroup = otherGroup;
        YearsInSchool = yearsInSchool;
        OtherData = otherData;
        OtherData2 = otherData2;
        OtherData3 = otherData3;
        Location = location;
        Age = age;
        DoctorId = doctorId;
    }

    /// <summary>
    /// Gets the unique identifier for the patient.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the first name of the patient.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Gets the last name of the patient.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Gets the email of the patient.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the gender of the patient.
    /// </summary>
    public Gender Gender { get; private set; }

    /// <summary>
    /// Gets the ethnic group of the patient.
    /// </summary>
    public string EthnicGroup { get; private set; }

    /// <summary>
    /// Gets other group information of the patient.
    /// </summary>
    public string OtherGroup { get; private set; }

    /// <summary>
    /// Gets the number of years the patient has been in school.
    /// </summary>
    public int YearsInSchool { get; private set; }

    /// <summary>
    /// Gets other data related to the patient.
    /// </summary>
    public string OtherData { get; private set; }

    /// <summary>
    /// Gets additional data related to the patient.
    /// </summary>
    public string OtherData2 { get; private set; }

    /// <summary>
    /// Gets more additional data related to the patient.
    /// </summary>
    public string OtherData3 { get; private set; }

    /// <summary>
    /// Gets the location of the patient.
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    /// Gets the age of the patient.
    /// </summary>
    public int Age { get; private set; }

    /// <summary>
    /// Gets the date and time when the patient was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets a value indicating whether the patient is archived.
    /// </summary>
    public bool IsArchived { get; private set; } = false;

    /// <summary>
    /// Gets the comment associated with the patient's archival status.
    /// </summary>
    public string ArchiveComment { get; private set; } = null;

    /// <summary>
    /// Gets or sets the ID of the doctor associated with the patient.
    /// </summary>
    public string DoctorId { get; set; }

    /// <summary>
    /// Gets or sets the doctor associated with the patient.
    /// </summary>
    public virtual ApplicationUser Doctor { get; set; }

    /// <summary>
    /// Gets or sets the ID of the research group associated with the patient.
    /// </summary>
    public Guid? ResearchGroupId { get; set; }

    /// <summary>
    /// Gets or sets the research group associated with the patient.
    /// </summary>
    public virtual ResearchGroup ResearchGroup { get; set; } = null;

    /// <summary>
    /// Archives the patient with a comment.
    /// </summary>
    /// <param name="comment">The comment to add when archiving the patient.</param>
    public void ArchivePatient(string comment)
    {
        IsArchived = true;
        ArchiveComment = comment;
    }

    /// <summary>
    /// Unarchives the patient.
    /// </summary>
    public void UnarchivePatient()
    {
        IsArchived = false;
        ArchiveComment = null;
    }

    /// <summary>
    /// Updates the patient's information.
    /// </summary>
    /// <param name="firstName">The first name of the patient.</param>
    /// <param name="lastName">The last name of the patient.</param>
    /// <param name="gender">The gender of the patient.</param>
    /// <param name="ethnicGroup">The ethnic group of the patient.</param>
    /// <param name="location">The location of the patient.</param>
    /// <param name="age">The age of the patient.</param>
    /// <param name="otherGroup">Other group information of the patient.</param>
    /// <param name="otherData">Other data related to the patient.</param>
    /// <param name="otherData2">Additional data related to the patient.</param>
    /// <param name="otherData3">More additional data related to the patient.</param>
    /// <param name="yearsInSchool">The number of years the patient has been in school.</param>
    public void UpdatePatient(string firstName, string lastName, string gender, string ethnicGroup, string location,
        int age, string otherGroup, string otherData, string otherData2, string otherData3, int yearsInSchool)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = Enum.Parse<Gender>(gender);
        EthnicGroup = ethnicGroup;
        OtherGroup = otherGroup;
        YearsInSchool = yearsInSchool;
        OtherData = otherData;
        OtherData2 = otherData2;
        OtherData3 = otherData3;
        Location = location;
        Age = age;
    }

    /// <summary>
    /// Adds the patient to a research group.
    /// </summary>
    /// <param name="researchGroupId">The ID of the research group to add the patient to.</param>
    public void AddResearchGroup(Guid researchGroupId) => ResearchGroupId = researchGroupId;

    /// <summary>
    /// Removes the patient from the research group.
    /// </summary>
    public void RemoveResearchGroup() => ResearchGroupId = null;

    /// <summary>
    /// Gets or sets the collection of patient examination cards.
    /// </summary>
    public ICollection<PatientExaminationCard> PatientExaminationCards { get; set; } = new List<PatientExaminationCard>();
}
