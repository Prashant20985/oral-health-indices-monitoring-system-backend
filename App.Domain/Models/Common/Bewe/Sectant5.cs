using App.Domain.Models.Common.Tooth;

namespace App.Domain.Models.Common.Bewe;

/// <summary>
/// Represents a section of teeth in the oral cavity.
/// </summary>
public class Sectant5
{
    /// <summary>
    /// Gets or sets the tooth at position 43.
    /// </summary>
    public FourSurfaceTooth Tooth_43 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 42.
    /// </summary>
    public FourSurfaceTooth Tooth_42 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 41.
    /// </summary>
    public FourSurfaceTooth Tooth_41 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 31.
    /// </summary>
    public FourSurfaceTooth Tooth_31 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 32.
    /// </summary>
    public FourSurfaceTooth Tooth_32 { get; set; }

    /// <summary>
    /// Gets or sets the tooth at position 33.
    /// </summary>
    public FourSurfaceTooth Tooth_33 { get; set; }

    public int FindMaxValue()
    {
        int maxVal = 0;
        var teeth = new[] { Tooth_31, Tooth_32, Tooth_33, Tooth_41, Tooth_42, Tooth_43 };

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

