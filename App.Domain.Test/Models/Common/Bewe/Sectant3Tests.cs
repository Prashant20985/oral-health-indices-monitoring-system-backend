using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;

namespace App.Domain.Test.Models.Common.Bewe;

public class Sectant3Tests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var tooth24 = new FiveSurfaceToothBEWE { B = "1", O = "2", L = "3" };
        var tooth25 = new FiveSurfaceToothBEWE { B = "4", O = "5", L = "6" };
        var tooth26 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth27 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant3
        {
            Tooth_24 = tooth24,
            Tooth_25 = tooth25,
            Tooth_26 = tooth26,
            Tooth_27 = tooth27
        };

        // Assert
        Assert.Equal(tooth24, sectant.Tooth_24);
        Assert.Equal(tooth25, sectant.Tooth_25);
        Assert.Equal(tooth26, sectant.Tooth_26);
        Assert.Equal(tooth27, sectant.Tooth_27);
    }

    [Fact]
    public void FindMaxValue_ShouldReturnCorrectMaxValue()
    {
        // Arrange
        var tooth24 = new FiveSurfaceToothBEWE { B = "1", O = "2", L = "3" };
        var tooth25 = new FiveSurfaceToothBEWE { B = "4", O = "5", L = "6" };
        var tooth26 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth27 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant3
        {
            Tooth_24 = tooth24,
            Tooth_25 = tooth25,
            Tooth_26 = tooth26,
            Tooth_27 = tooth27
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
        var sectant = new Sectant3();

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(0, maxValue);
    }

    [Fact]
    public void FindMaxValue_ShouldIgnoreInvalidValues()
    {
        // Arrange
        var tooth24 = new FiveSurfaceToothBEWE { B = "x", O = "2", L = "3" };
        var tooth25 = new FiveSurfaceToothBEWE { B = "4", O = "invalid", L = "6" };
        var tooth26 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth27 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant3
        {
            Tooth_24 = tooth24,
            Tooth_25 = tooth25,
            Tooth_26 = tooth26,
            Tooth_27 = tooth27
        };

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(12, maxValue);
    }

}
