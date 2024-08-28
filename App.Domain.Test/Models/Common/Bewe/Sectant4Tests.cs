using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;

namespace App.Domain.Test.Models.Common.Bewe;

public class Sectant4Tests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var tooth34 = new FiveSurfaceToothBEWE { B = "1", O = "2", L = "3" };
        var tooth35 = new FiveSurfaceToothBEWE { B = "4", O = "5", L = "6" };
        var tooth36 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth37 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant4
        {
            Tooth_34 = tooth34,
            Tooth_35 = tooth35,
            Tooth_36 = tooth36,
            Tooth_37 = tooth37
        };

        // Assert
        Assert.Equal(tooth34, sectant.Tooth_34);
        Assert.Equal(tooth35, sectant.Tooth_35);
        Assert.Equal(tooth36, sectant.Tooth_36);
        Assert.Equal(tooth37, sectant.Tooth_37);
    }

    [Fact]
    public void FindMaxValue_ShouldReturnCorrectMaxValue()
    {
        // Arrange
        var tooth34 = new FiveSurfaceToothBEWE { B = "1", O = "2", L = "3" };
        var tooth35 = new FiveSurfaceToothBEWE { B = "4", O = "5", L = "6" };
        var tooth36 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth37 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant4
        {
            Tooth_34 = tooth34,
            Tooth_35 = tooth35,
            Tooth_36 = tooth36,
            Tooth_37 = tooth37
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
        var sectant = new Sectant4();

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(0, maxValue);
    }

    [Fact]
    public void FindMaxValue_ShouldIgnoreInvalidValues()
    {
        // Arrange
        var tooth34 = new FiveSurfaceToothBEWE { B = "x", O = "2", L = "3" };
        var tooth35 = new FiveSurfaceToothBEWE { B = "4", O = "invalid", L = "6" };
        var tooth36 = new FiveSurfaceToothBEWE { B = "7", O = "8", L = "9" };
        var tooth37 = new FiveSurfaceToothBEWE { B = "10", O = "11", L = "12" };

        var sectant = new Sectant4
        {
            Tooth_34 = tooth34,
            Tooth_35 = tooth35,
            Tooth_36 = tooth36,
            Tooth_37 = tooth37
        };

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(12, maxValue);
    }
}