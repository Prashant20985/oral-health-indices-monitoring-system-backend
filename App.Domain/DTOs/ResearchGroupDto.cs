namespace App.Domain.DTOs;

public class ResearchGroupDto
{
    public Guid Id { get; private set; }
    public string GroupName { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public List<ResearchGroupPatientDto> Patients { get; set; }
}
