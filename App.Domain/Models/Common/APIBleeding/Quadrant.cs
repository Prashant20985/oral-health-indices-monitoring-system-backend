namespace App.Domain.Models.Common.APIBleeding;

/// <summary>
/// Represents a quadrant with seven values.
/// </summary>
public class Quadrant
{
    /// <summary>
    /// Gets or sets the first value.
    /// </summary>
    public string Value1 { get; set; }

    /// <summary>
    /// Gets or sets the second value.
    /// </summary>
    public string Value2 { get; set; }

    /// <summary>
    /// Gets or sets the third value.
    /// </summary>
    public string Value3 { get; set; }

    /// <summary>
    /// Gets or sets the fourth value.
    /// </summary>
    public string Value4 { get; set; }

    /// <summary>
    /// Gets or sets the fifth value.
    /// </summary>
    public string Value5 { get; set; }

    /// <summary>
    /// Gets or sets the sixth value.
    /// </summary>
    public string Value6 { get; set; }

    /// <summary>
    /// Gets or sets the seventh value.
    /// </summary>
    public string Value7 { get; set; }

    public string[] ToArray() => [Value1, Value2, Value3, Value4, Value5, Value6, Value7];
}

