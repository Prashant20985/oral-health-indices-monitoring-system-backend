using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.Test.Models.Common.APIBleeding;

public class QuadrantTests
{
    [Fact]
    public void Quadrant_Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var quadrant = new Quadrant
        {
            Value1 = "Value1",
            Value2 = "Value2",
            Value3 = "Value3",
            Value4 = "Value4",
            Value5 = "Value5",
            Value6 = "Value6",
            Value7 = "Value7"
        };

        // Assert
        Assert.Equal("Value1", quadrant.Value1);
        Assert.Equal("Value2", quadrant.Value2);
        Assert.Equal("Value3", quadrant.Value3);
        Assert.Equal("Value4", quadrant.Value4);
        Assert.Equal("Value5", quadrant.Value5);
        Assert.Equal("Value6", quadrant.Value6);
        Assert.Equal("Value7", quadrant.Value7);
    }

    [Fact]
    public void ToArray_ShouldReturnCorrectArray()
    {
        // Arrange
        var quadrant = new Quadrant
        {
            Value1 = "Value1",
            Value2 = "Value2",
            Value3 = "Value3",
            Value4 = "Value4",
            Value5 = "Value5",
            Value6 = "Value6",
            Value7 = "Value7"
        };

        // Act
        var values = quadrant.ToArray();

        // Assert
        Assert.Equal(7, values.Length);
        Assert.Equal("Value1", values[0]);
        Assert.Equal("Value2", values[1]);
        Assert.Equal("Value3", values[2]);
        Assert.Equal("Value4", values[3]);
        Assert.Equal("Value5", values[4]);
        Assert.Equal("Value6", values[5]);
        Assert.Equal("Value7", values[6]);
    }
}