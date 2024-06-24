using App.Domain.Models.Common.Tooth;

namespace App.Domain.Models.Common.Bewe;

/// <summary>
/// Represents a section of teeth in the oral cavity.
/// </summary>
public class Sectant4
{
    /// <summary>
    /// Gets or sets the tooth at position 34.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_34 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 35.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_35 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 36.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_36 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 37.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_37 { get; set; }

    public int FindMaxValue()
    {
        int maxVal = 0;
        var teeth = new[] { Tooth_34, Tooth_35, Tooth_36, Tooth_37 };

        foreach (var tooth in teeth)
        {
            if (tooth != null)
            {
                maxVal = Math.Max(maxVal, GetIntValue(tooth.B));
                maxVal = Math.Max(maxVal, GetIntValue(tooth.O));
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

