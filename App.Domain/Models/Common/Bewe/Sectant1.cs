using App.Domain.Models.Common.Tooth;

namespace App.Domain.Models.Common.Bewe;

/// <summary>
/// Represents a section of teeth in the oral cavity.
/// </summary>
public class Sectant1
{
    /// <summary>
    /// Gets or sets the tooth at position 17.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_17 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 16.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_16 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 15.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_15 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 14.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_14 { get; set; }

    /// <summary>
    ///  Finds the maximum value of the teeth in the sectant.
    /// </summary>
    /// <returns></returns>
    public int FindMaxValue()
    {
        int maxVal = 0;
        var teeth = new[] { Tooth_17, Tooth_16, Tooth_15, Tooth_14 };

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

    /// <summary>
    ///  Finds the minimum value of the teeth in the sectant.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static int GetIntValue(string value)
    {
        if (value.Equals("x") || string.IsNullOrEmpty(value))
            return 0;

        if (int.TryParse(value, out int result))
            return result;

        return 0;
    }
}

