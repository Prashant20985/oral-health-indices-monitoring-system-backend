using App.Domain.Models.Enums;

namespace App.Domain.DTOs.ResearchGroupDtos.Response;

public class ResearchGroupPatientResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public string EthnicGroup { get; set; }
    public string OtherGroup { get; set; }
    public int YearsInSchool { get; set; }
    public string OtherData { get; set; }
    public string OtherData2 { get; set; }
    public string OtherData3 { get; set; }
    public string Location { get; set; }
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsArchived { get; set; }
    public string ArchiveComment { get; set; }
    public string AddedBy { get; set; }
}
