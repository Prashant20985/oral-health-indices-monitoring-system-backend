namespace App.Domain.Models.Common.Tooth;

/// <summary>
///  Represents a model for BEWE (Basic Erosive Wear Examination) assessment.
/// </summary>
public class SixSurfaceTooth
{
    /// <summary>
    ///  Gets or sets the assessment data for the right surface of the tooth.
    /// </summary>
    public string R { get; set; }
    
    /// <summary>
    ///  Gets or sets the assessment data for the buccal surface of the tooth.
    /// </summary>
    public string B { get; set; }
    
    /// <summary>
    ///  Gets or sets the assessment data for the occlusal surface of the tooth.
    /// </summary>
    public string O { get; set; }
    
    /// <summary>
    ///  Gets or sets the assessment data for the left surface of the tooth.
    /// </summary>
    public string L { get; set; }
    
    /// <summary>
    ///  Gets or sets the assessment data for the distal surface of the tooth.
    /// </summary>
    public string D { get; set; }
    
    /// <summary>
    ///  Gets or sets the assessment data for the mesial surface of the tooth.
    /// </summary>
    public string M { get; set; }
}
