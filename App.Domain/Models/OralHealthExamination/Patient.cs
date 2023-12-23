using App.Domain.Models.Enums;
using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;

public class Patient
{
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

    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public Gender Gender { get; private set; }
    public string EthnicGroup { get; private set; }
    public string OtherGroup { get; private set; }
    public int YearsInSchool { get; private set; }
    public string OtherData { get; private set; }
    public string OtherData2 { get; private set; }
    public string OtherData3 { get; private set; }
    public string Location { get; private set; }
    public int Age { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public bool IsArchived { get; private set; } = false;
    public string ArchiveComment { get; private set; } = null;

    public string DoctorId { get; set; }
    public virtual ApplicationUser Doctor { get; set; }

    public Guid? ResearchGroupId { get; set; }
    public virtual ResearchGroup ResearchGroup { get; set; } = null;

    public void Archive(string comment)
    {
        IsArchived = true;
        ArchiveComment = comment;
    }

    public void AddResearchGroup(Guid researchGroupId) => ResearchGroupId = researchGroupId;

    public void RemoveResearchGroup() => ResearchGroupId = null;

    public ICollection<PatientExaminationCard> PatientExaminationCards { get; set; } = new List<PatientExaminationCard>();
}
