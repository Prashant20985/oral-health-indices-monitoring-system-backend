using App.Domain.Models.Enums;

namespace App.Domain.DTOs;

public class ResearchGroupPatientDto
{
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
    public DateTime CreatedAt { get; private set; }
    public bool IsArchived { get; private set; }
    public string ArchiveComment { get; private set; }
    public string AddedBy { get; set; }
}
