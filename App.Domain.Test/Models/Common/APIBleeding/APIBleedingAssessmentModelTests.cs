using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.Test.Models.Common.APIBleeding;

public class APIBleedingAssessmentModelTests
{
    [Fact]
    public void Quadrants_ShouldBeInitializedCorrectly()
    {
        // Arrange
        var quadrant1 = new Quadrant();
        var quadrant2 = new Quadrant();
        var quadrant3 = new Quadrant();
        var quadrant4 = new Quadrant();

        // Act
        var model = new APIBleedingAssessmentModel
        {
            Quadrant1 = quadrant1,
            Quadrant2 = quadrant2,
            Quadrant3 = quadrant3,
            Quadrant4 = quadrant4
        };

        // Assert
        Assert.Equal(quadrant1, model.Quadrant1);
        Assert.Equal(quadrant2, model.Quadrant2);
        Assert.Equal(quadrant3, model.Quadrant3);
        Assert.Equal(quadrant4, model.Quadrant4);
    }

    [Fact]
    public void Quadrants_ShouldBeAssignable()
    {
        // Arrange
        var model = new APIBleedingAssessmentModel();
        var quadrant1 = new Quadrant();
        var quadrant2 = new Quadrant();
        var quadrant3 = new Quadrant();
        var quadrant4 = new Quadrant();

        // Act
        model.Quadrant1 = quadrant1;
        model.Quadrant2 = quadrant2;
        model.Quadrant3 = quadrant3;
        model.Quadrant4 = quadrant4;

        // Assert
        Assert.Equal(quadrant1, model.Quadrant1);
        Assert.Equal(quadrant2, model.Quadrant2);
        Assert.Equal(quadrant3, model.Quadrant3);
        Assert.Equal(quadrant4, model.Quadrant4);
    }
}