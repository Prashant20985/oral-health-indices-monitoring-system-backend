using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;

namespace App.Domain.Test.Models.Common.Bewe;

public class Sectant2Tests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var tooth11 = new FourSurfaceTooth { B = "1", L = "2" };
        var tooth12 = new FourSurfaceTooth { B = "3", L = "4" };
        var tooth13 = new FourSurfaceTooth { B = "5", L = "6" };
        var tooth21 = new FourSurfaceTooth { B = "7", L = "8" };
        var tooth22 = new FourSurfaceTooth { B = "9", L = "10" };
        var tooth23 = new FourSurfaceTooth { B = "11", L = "12" };

        var sectant = new Sectant2
        {
            Tooth_11 = tooth11,
            Tooth_12 = tooth12,
            Tooth_13 = tooth13,
            Tooth_21 = tooth21,
            Tooth_22 = tooth22,
            Tooth_23 = tooth23
        };

        // Assert
        Assert.Equal(tooth11, sectant.Tooth_11);
        Assert.Equal(tooth12, sectant.Tooth_12);
        Assert.Equal(tooth13, sectant.Tooth_13);
        Assert.Equal(tooth21, sectant.Tooth_21);
        Assert.Equal(tooth22, sectant.Tooth_22);
        Assert.Equal(tooth23, sectant.Tooth_23);
    }

    [Fact]
    public void FindMaxValue_ShouldReturnCorrectMaxValue()
    {
        // Arrange
        var tooth11 = new FourSurfaceTooth { B = "1", L = "2" };
        var tooth12 = new FourSurfaceTooth { B = "3", L = "4" };
        var tooth13 = new FourSurfaceTooth { B = "5", L = "6" };
        var tooth21 = new FourSurfaceTooth { B = "7", L = "8" };
        var tooth22 = new FourSurfaceTooth { B = "9", L = "10" };
        var tooth23 = new FourSurfaceTooth { B = "11", L = "12" };

        var sectant = new Sectant2
        {
            Tooth_11 = tooth11,
            Tooth_12 = tooth12,
            Tooth_13 = tooth13,
            Tooth_21 = tooth21,
            Tooth_22 = tooth22,
            Tooth_23 = tooth23
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
        var sectant = new Sectant2();

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(0, maxValue);
    }

    [Fact]
    public void FindMaxValue_ShouldIgnoreInvalidValues()
    {
        // Arrange
        var tooth11 = new FourSurfaceTooth { B = "x", L = "2" };
        var tooth12 = new FourSurfaceTooth { B = "3", L = "x" };
        var tooth13 = new FourSurfaceTooth { B = "5", L = "6" };
        var tooth21 = new FourSurfaceTooth { B = "7", L = "8" };
        var tooth22 = new FourSurfaceTooth { B = "9", L = "10" };
        var tooth23 = new FourSurfaceTooth { B = "11", L = "12" };

        var sectant = new Sectant2
        {
            Tooth_11 = tooth11,
            Tooth_12 = tooth12,
            Tooth_13 = tooth13,
            Tooth_21 = tooth21,
            Tooth_22 = tooth22,
            Tooth_23 = tooth23
        };

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(12, maxValue);
    }
}
