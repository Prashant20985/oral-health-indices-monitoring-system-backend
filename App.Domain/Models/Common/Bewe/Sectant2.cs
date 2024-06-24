using App.Domain.Models.Common.Tooth;

namespace App.Domain.Models.Common.Bewe;

/// <summary>
/// Represents a section of teeth in the oral cavity.
/// </summary>
public class Sectant2
{
    /// <summary>
    /// Gets or sets the tooth at position 13.
    /// </summary>
    public FourSurfaceTooth Tooth_13 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 12.
    /// </summary>
    public FourSurfaceTooth Tooth_12 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 11.
    /// </summary>
    public FourSurfaceTooth Tooth_11 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 21.
    /// </summary>
    public FourSurfaceTooth Tooth_21 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 22.
    /// </summary>
    public FourSurfaceTooth Tooth_22 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 23.
    /// </summary>
    public FourSurfaceTooth Tooth_23 { get; set; }

    public int FindMaxValue()
    {
        int maxVal = 0;
        var teeth = new[] { Tooth_11, Tooth_12, Tooth_13, Tooth_21, Tooth_22, Tooth_23 };

        foreach (var tooth in teeth)
        {
            if (tooth != null)
            {
                maxVal = Math.Max(maxVal, GetIntValue(tooth.B));
                maxVal = Math.Max(maxVal, GetIntValue(tooth.L));
            }
        }

        return maxVal;
    }

    private static int GetIntValue(string value)
    {
        if (value.Equals("x") || string.IsNullOrEmpty(value))
            return 0;

        if (int.TryParse(value, out int result))
            return result;

        return 0;
    }
}

