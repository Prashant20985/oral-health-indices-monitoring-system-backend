namespace App.Domain.Models.Common.Tooth;
/// <summary>
///  Represents a model for BEWE (Basic Erosive Wear Examination) assessment.
/// </summary>
public class FiveSurfaceToothBEWE
{
    /// <summary>
    ///  Gets or sets the assessment data for the occlusal surface of the tooth.
    /// </summary>
    public string O { get; set; }
    
    /// <summary>
    ///  Gets or sets the assessment data for the buccal surface of the tooth.
    /// </summary>
    public string B { get; set; }
    
    /// <summary>
    ///  Gets or sets the assessment data for the lingual surface of the tooth.
    /// </summary>
    public string L { get; set; }
    
    /// <summary>
    ///  Gets or sets the assessment data for the disal surface of the tooth.
    /// </summary>
    public string D { get; set; }
    
    /// <summary>
    /// Gets or sets the assessment data for the mesial surface of the tooth.
    /// </summary>
    public string M { get; set; }
}
