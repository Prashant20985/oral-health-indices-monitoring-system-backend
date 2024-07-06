namespace App.Domain.DTOs.ResearchGroupDtos.Response;

public class ResearchGroupResponseDto
{
    public Guid Id { get; private set; }
    public string GroupName { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public List<ResearchGroupPatientResponseDto> Patients { get; set; }
}
