namespace App.Domain.Models.Common.Tooth;
/// <summary>
/// Represents a model for DMFT/DMFS (Decayed, Missing, Filled Teeth/Surfaces) assessment.
/// </summary>
public class FiveSurfaceToothDMFT_DMFS
{
    /// <summary>
    ///  Gets or sets the DMFT/DMFS data for the right surface of the tooth.
    /// </summary>
    public string R { get; set; }
    
    /// <summary>
    ///  Gets or sets the DMFT/DMFS data for the buccal surface of the tooth.
    /// </summary>
    public string B { get; set; }
    
    /// <summary>
    ///  Gets or sets the DMFT/DMFS data for the left surface of the tooth.
    /// </summary>
    public string L { get; set; }
    
    /// <summary>
    ///  Gets or sets the DMFT/DMFS data for the distal surface of the tooth.
    /// </summary>
    public string D { get; set; }
    
    /// <summary>
    ///  Gets or sets the DMFT/DMFS data for the mesial surface of the tooth.
    /// </summary>
    public string M { get; set; }
}
