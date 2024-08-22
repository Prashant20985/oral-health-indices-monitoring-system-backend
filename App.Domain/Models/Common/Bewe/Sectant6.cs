using App.Domain.Models.Common.Tooth;

namespace App.Domain.Models.Common.Bewe;

/// <summary>
/// Represents a section of teeth in the oral cavity.
/// </summary>
public class Sectant6
{
    /// <summary>
    /// Gets or sets the tooth at position 47.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_47 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 46.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_46 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 45.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_45 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 44.
    /// </summary>
    public FiveSurfaceToothBEWE Tooth_44 { get; set; }

    /// <summary>
    ///  Finds the maximum value of the teeth in the sectant.
    /// </summary>
    /// <returns></returns>
    public int FindMaxValue()
    {
        int maxVal = 0;
        var teeth = new[] { Tooth_44, Tooth_45, Tooth_46, Tooth_47 };

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
