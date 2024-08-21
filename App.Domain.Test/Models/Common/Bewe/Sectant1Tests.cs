using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;

namespace App.Domain.Test.Models.Common.Bewe;

public class Sectant1Tests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var tooth17 = new FiveSurfaceToothBEWE { B = "1", O = "2", L = "3" };
        var tooth16 = new FiveSurfaceToothBEWE { B = "4", O = "5", L = "6" };
        var tooth15 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth14 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant1
        {
            Tooth_17 = tooth17,
            Tooth_16 = tooth16,
            Tooth_15 = tooth15,
            Tooth_14 = tooth14
        };

        // Assert
        Assert.Equal(tooth17, sectant.Tooth_17);
        Assert.Equal(tooth16, sectant.Tooth_16);
        Assert.Equal(tooth15, sectant.Tooth_15);
        Assert.Equal(tooth14, sectant.Tooth_14);
    }

    [Fact]
    public void FindMaxValue_ShouldReturnCorrectMaxValue()
    {
        // Arrange
        var tooth17 = new FiveSurfaceToothBEWE { B = "1", O = "2", L = "3" };
        var tooth16 = new FiveSurfaceToothBEWE { B = "4", O = "5", L = "6" };
        var tooth15 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth14 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant1
        {
            Tooth_17 = tooth17,
            Tooth_16 = tooth16,
            Tooth_15 = tooth15,
            Tooth_14 = tooth14
        };

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(12, maxValue);
    }

    [Fact]
    public void FindMaxValue_ShouldReturnZero_WhenNoTeethAreSet()
    {
        // Arrange
        var sectant = new Sectant1();

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(0, maxValue);
    }

    [Fact]
    public void FindMaxValue_ShouldIgnoreInvalidValues()
    {
        // Arrange
        var tooth17 = new FiveSurfaceToothBEWE { B = "x", O = "2", L = "3" };
        var tooth16 = new FiveSurfaceToothBEWE { B = "4", O = "x", L = "6" };
        var tooth15 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth14 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant1
        {
            Tooth_17 = tooth17,
            Tooth_16 = tooth16,
            Tooth_15 = tooth15,
            Tooth_14 = tooth14
        };

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(12, maxValue);
    }
}
