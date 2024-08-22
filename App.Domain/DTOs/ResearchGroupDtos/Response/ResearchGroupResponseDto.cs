namespace App.Domain.DTOs.ResearchGroupDtos.Response;
/// <summary>
///  Represents the response DTO for research group.
/// </summary>
public class ResearchGroupResponseDto
{
    /// <summary>
    ///  Gets or sets the id of the research group.
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    ///  Gets or sets the name of the research group.
    /// </summary>
    public string GroupName { get; set; }
    
    /// <summary>
    ///  Gets or sets the description of the research group.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Gets or sets the created by of the research group.
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    ///  Gets or sets the created at of the research group.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///  Gets or sets the list of patients.
    /// </summary>
    public List<ResearchGroupPatientResponseDto> Patients { get; set; }
}
