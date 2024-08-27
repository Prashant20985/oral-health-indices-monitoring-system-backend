using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;

namespace App.Domain.Test.Models.Common.Bewe;

public class Sectant5Tests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var tooth31 = new FourSurfaceTooth { B = "1", L = "2" };
        var tooth32 = new FourSurfaceTooth { B = "3", L = "4" };
        var tooth33 = new FourSurfaceTooth { B = "5", L = "6" };
        var tooth41 = new FourSurfaceTooth { B = "7", L = "8" };
        var tooth42 = new FourSurfaceTooth { B = "9", L = "10" };
        var tooth43 = new FourSurfaceTooth { B = "11", L = "12" };

        var sectant = new Sectant5
        {
            Tooth_31 = tooth31,
            Tooth_32 = tooth32,
            Tooth_33 = tooth33,
            Tooth_41 = tooth41,
            Tooth_42 = tooth42,
            Tooth_43 = tooth43
        };

        // Assert
        Assert.Equal(tooth31, sectant.Tooth_31);
        Assert.Equal(tooth32, sectant.Tooth_32);
        Assert.Equal(tooth33, sectant.Tooth_33);
        Assert.Equal(tooth41, sectant.Tooth_41);
        Assert.Equal(tooth42, sectant.Tooth_42);
        Assert.Equal(tooth43, sectant.Tooth_43);
    }

    [Fact]
    public void FindMaxValue_ShouldReturnCorrectMaxValue()
    {
        // Arrange
        var tooth31 = new FourSurfaceTooth { B = "1", L = "2" };
        var tooth32 = new FourSurfaceTooth { B = "3", L = "4" };
        var tooth33 = new FourSurfaceTooth { B = "5", L = "6" };
        var tooth41 = new FourSurfaceTooth { B = "7", L = "8" };
        var tooth42 = new FourSurfaceTooth { B = "9", L = "10" };
        var tooth43 = new FourSurfaceTooth { B = "11", L = "12" };

        var sectant = new Sectant5
        {
            Tooth_31 = tooth31,
            Tooth_32 = tooth32,
            Tooth_33 = tooth33,
            Tooth_41 = tooth41,
            Tooth_42 = tooth42,
            Tooth_43 = tooth43
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
        var sectant = new Sectant5();

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(0, maxValue);
    }

    [Fact]
    public void FindMaxValue_ShouldIgnoreInvalidValues()
    {
        // Arrange
        var tooth31 = new FourSurfaceTooth { B = "x", L = "2" };
        var tooth32 = new FourSurfaceTooth { B = "3", L = "invalid" };
        var tooth33 = new FourSurfaceTooth { B = "5", L = "6" };
        var tooth41 = new FourSurfaceTooth { B = "7", L = "8" };
        var tooth42 = new FourSurfaceTooth { B = "9", L = "10" };
        var tooth43 = new FourSurfaceTooth { B = "11", L = "12" };

        var sectant = new Sectant5
        {
            Tooth_31 = tooth31,
            Tooth_32 = tooth32,
            Tooth_33 = tooth33,
            Tooth_41 = tooth41,
            Tooth_42 = tooth42,
            Tooth_43 = tooth43
        };

        // Act
        var maxValue = sectant.FindMaxValue();

        // Assert
        Assert.Equal(12, maxValue);
    }

}
