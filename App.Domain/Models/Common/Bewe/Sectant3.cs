using App.Domain.Models.Common.Tooth;

namespace App.Domain.Models.Common.Bewe;

/// <summary>
/// Represents a section of teeth in the oral cavity.
/// </summary>
public class Sectant3
{
    /// <summary>
    /// Gets or sets the tooth at position 24.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_24 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 25.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_25 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 26.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_26 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 27.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_27 { get; set; }

    
    /// <summary>
    ///  Finds the maximum value of the teeth in the sectant.
    /// </summary>
    /// <returns></returns>
    public int FindMaxValue()
    {
        int maxVal = 0;
        var teeth = new[] { Tooth_24, Tooth_25, Tooth_26, Tooth_27 };

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

