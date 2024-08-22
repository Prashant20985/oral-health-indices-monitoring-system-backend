namespace App.Domain.Models.Common.Tooth;
/// <summary>
///  Represents a model for BEWE (Basic Erosive Wear Examination) assessment.
/// </summary>
public class FourSurfaceTooth
{
    /// <summary>
    ///  Gets or sets the assessment data for the occlusal surface of the tooth.
    /// </summary>
    public string B { get; set; }
    public string L { get; set; }
    public string D { get; set; }
    public string M { get; set; }
}
