namespace App.Domain.Models.Common.Bewe;

/// <summary>
/// Represents a model for BEWE (Basic Erosive Wear Examination) assessment with six sectants.
/// </summary>
public class BeweAssessmentModel
{
    /// <summary>
    /// Gets or sets the teeth in the first sectant.
    /// </summary>
    public Sectant1 Sectant1 { get; set; }

    /// <summary>
    /// Gets or sets the teeth in the second sectant.
    /// </summary>
    public Sectant2 Sectant2 { get; set; }

    /// <summary>
    /// Gets or sets the teeth in the third sectant.
    /// </summary>
    public Sectant3 Sectant3 { get; set; }

    /// <summary>
    /// Gets or sets the teeth in the fourth sectant.
    /// </summary>
    public Sectant4 Sectant4 { get; set; }

    /// <summary>
    /// Gets or sets the teeth in the fifth sectant.
    /// </summary>
    public Sectant5 Sectant5 { get; set; }

    /// <summary>
    /// Gets or sets the teeth in the sixth sectant.
    /// </summary>
    public Sectant6 Sectant6 { get; set; }
}

