namespace App.Domain.Models.Common.APIBleeding;

/// <summary>
/// Represents a model for API Bleeding Assessment with four quadrants.
/// </summary>
public class APIBleedingAssessmentModel
{
    /// <summary>
    /// Gets or sets the first quadrant.
    /// </summary>
    public Quadrant Quadrant1 { get; set; }

    /// <summary>
    /// Gets or sets the second quadrant.
    /// </summary>
    public Quadrant Quadrant2 { get; set; }

    /// <summary>
    /// Gets or sets the third quadrant.
    /// </summary>
    public Quadrant Quadrant3 { get; set; }

    /// <summary>
    /// Gets or sets the fourth quadrant.
    /// </summary>
    public Quadrant Quadrant4 { get; set; }
}

