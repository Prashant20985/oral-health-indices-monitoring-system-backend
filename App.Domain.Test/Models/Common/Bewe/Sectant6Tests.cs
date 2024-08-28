using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;

namespace App.Domain.Test.Models.Common.Bewe;

public class Sectant6Tests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var tooth44 = new FiveSurfaceToothBEWE { B = "1", O = "2", L = "3" };
        var tooth45 = new FiveSurfaceToothBEWE { B = "4", O = "5", L = "6" };
        var tooth46 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth47 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant6
        {
            Tooth_44 = tooth44,
            Tooth_45 = tooth45,
            Tooth_46 = tooth46,
            Tooth_47 = tooth47
        };

        // Assert
        Assert.Equal(tooth44, sectant.Tooth_44);
        Assert.Equal(tooth45, sectant.Tooth_45);
        Assert.Equal(tooth46, sectant.Tooth_46);
        Assert.Equal(tooth47, sectant.Tooth_47);
    }

    [Fact]
    public void FindMaxValue_ShouldReturnCorrectMaxValue()
    {
        // Arrange
        var tooth44 = new FiveSurfaceToothBEWE { B = "1", O = "2", L = "3" };
        var tooth45 = new FiveSurfaceToothBEWE { B = "4", O = "5", L = "6" };
        var tooth46 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth47 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant6
        {
            Tooth_44 = tooth44,
            Tooth_45 = tooth45,
            Tooth_46 = tooth46,
            Tooth_47 = tooth47
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
        var sectant = new Sectant6();

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(0, maxValue);
    }

    [Fact]
    public void FindMaxValue_ShouldIgnoreInvalidValues()
    {
        // Arrange
        var tooth44 = new FiveSurfaceToothBEWE { B = "x", O = "2", L = "3" };
        var tooth45 = new FiveSurfaceToothBEWE { B = "4", O = "invalid", L = "6" };
        var tooth46 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth47 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant6
        {
            Tooth_44 = tooth44,
            Tooth_45 = tooth45,
            Tooth_46 = tooth46,
            Tooth_47 = tooth47
        };

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(12, maxValue);
    }
}