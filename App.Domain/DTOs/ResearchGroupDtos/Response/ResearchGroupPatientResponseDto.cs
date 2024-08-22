namespace App.Domain.DTOs.ResearchGroupDtos.Response;
/// <summary>
///  Represents the response DTO for research group patients.
/// </summary>
public class ResearchGroupPatientResponseDto
{
    /// <summary>
    ///   Gets or sets the id of the research group patient.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    ///  Gets or sets the first name of the research group patient.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    ///  Gets or sets the last name of the research group patient.
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    ///  Gets or sets the email of the research group patient.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    ///  Gets or sets the phone number of the research group patient.
    /// </summary>
    public string Gender { get; set; }
    
    /// <summary>
    ///  Gets or sets EthnicGroup of the research group patient.
    /// </summary>
    public string EthnicGroup { get; set; }
    
    /// <summary>
    ///  Gets or sets OtherGroup of the research group patient.
    /// </summary>
    public string OtherGroup { get; set; }
    
    /// <summary>
    ///  Gets or sets YearsInSchool of the research group patient.
    /// </summary>
    public int YearsInSchool { get; set; }
    
    /// <summary>
    ///  Gets or sets OtherData of the research group patient.
    /// </summary>
    public string OtherData { get; set; }
    
    /// <summary>
    ///  Gets or sets OtherData2 of the research group patient.
    /// </summary>
    public string OtherData2 { get; set; }
    
    
    /// <summary>
    ///  Gets or sets OtherData3 of the research group patient.
    /// </summary>
    public string OtherData3 { get; set; }
    
    /// <summary>
    ///  Gets or sets Location of the research group patient.
    /// </summary>
    public string Location { get; set; }
    
    /// <summary>
    ///  Gets or sets Age of the research group patient.
    ///  </summary>
    public int Age { get; set; }
    
    /// <summary>
    ///  Gets or sets the date the research group patient was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    ///  Gets or sets if the research group patient is Archived.
    /// </summary>
    public bool IsArchived { get; set; }
    
    /// <summary>
    ///  Gets or sets the archive comment of the research group patient.
    /// </summary>
    public string ArchiveComment { get; set; }
    
    /// <summary>
    ///  Gets or sets by whom the research group patient was added.
    /// </summary>
    public string AddedBy { get; set; }
}
