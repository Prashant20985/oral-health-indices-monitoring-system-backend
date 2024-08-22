namespace App.Domain.DTOs.ResearchGroupDtos.Request;
/// <summary>
///  Represents the create update research group request DTO.
/// </summary>
public class CreateUpdateResearchGroupRequestDto
{
    /// <summary>
    ///     Gets or sets the name of the research group.
    /// </summary>
    public string GroupName { get; set; }
    
    /// <summary>
    ///    Gets or sets the description of the research group.
    /// </summary>
    public string Description { get; set; }
}
