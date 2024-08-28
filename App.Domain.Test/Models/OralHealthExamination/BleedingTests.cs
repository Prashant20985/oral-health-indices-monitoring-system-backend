using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Test.Models.OralHealthExamination;

public class BleedingTests
{
    [Fact]
    public void CalculateBleedingResult_AssessmentModelIsNull_ShouldNotThrowException()
    {
        // Arrange
        var bleeding = new Bleeding();

        // Act
        bleeding.CalculateBleedingResult();

        // Assert
        Assert.Equal(0, bleeding.BleedingResult);
        Assert.Equal(0, bleeding.Maxilla);
        Assert.Equal(0, bleeding.Mandible);
    }

    [Fact]
    public void CalculateBleedingResult_AllQuadrantsPlus_ShouldCalculateCorrectly()
    {
        // Arrange
        var assessmentModel = new APIBleedingAssessmentModel
        {
            Quadrant1 = new Quadrant
                { Value1 = "+", Value2 = "+", Value3 = "+", Value4 = "", Value5 = "", Value6 = "", Value7 = "" },
            Quadrant2 = new Quadrant
                { Value1 = "+", Value2 = "+", Value3 = "+", Value4 = "", Value5 = "", Value6 = "", Value7 = "" },
            Quadrant3 = new Quadrant
                { Value1 = "+", Value2 = "+", Value3 = "+", Value4 = "", Value5 = "", Value6 = "", Value7 = "" },
            Quadrant4 = new Quadrant
                { Value1 = "+", Value2 = "+", Value3 = "+", Value4 = "", Value5 = "", Value6 = "", Value7 = "" }
        };

        var bleeding = new Bleeding();
        bleeding.SetAssessmentModel(assessmentModel);

        // Act
        bleeding.CalculateBleedingResult();

        // Assert
        Assert.Equal(100, bleeding.BleedingResult);
        Assert.Equal(100, bleeding.Maxilla);
        Assert.Equal(100, bleeding.Mandible);
    }

    [Fact]
    public void CalculateBleedingResult_AllQuadrantsMinus_ShouldCalculateCorrectly()
    {
        // Arrange
        var assessmentModel = new APIBleedingAssessmentModel
        {
            Quadrant1 = new Quadrant
                { Value1 = "-", Value2 = "-", Value3 = "-", Value4 = "", Value5 = "", Value6 = "", Value7 = "" },
            Quadrant2 = new Quadrant
                { Value1 = "-", Value2 = "-", Value3 = "-", Value4 = "", Value5 = "", Value6 = "", Value7 = "" },
            Quadrant3 = new Quadrant
                { Value1 = "-", Value2 = "-", Value3 = "-", Value4 = "", Value5 = "", Value6 = "", Value7 = "" },
            Quadrant4 = new Quadrant
                { Value1 = "-", Value2 = "-", Value3 = "-", Value4 = "", Value5 = "", Value6 = "", Value7 = "" }
        };

        var bleeding = new Bleeding();

        bleeding.SetAssessmentModel(assessmentModel);

        // Act
        bleeding.CalculateBleedingResult();

        // Assert
        Assert.Equal(0, bleeding.BleedingResult);
        Assert.Equal(0, bleeding.Maxilla);
        Assert.Equal(0, bleeding.Mandible);
    }

    [Fact]
    public void CalculateBleedingResult_MixedQuadrants_ShouldCalculateCorrectly()
    {
        // Arrange
        var assessmentModel = new APIBleedingAssessmentModel
        {
            Quadrant1 = new Quadrant
                { Value1 = "+", Value2 = "-", Value3 = "+", Value4 = "", Value5 = "", Value6 = "", Value7 = "" },
            Quadrant2 = new Quadrant
                { Value1 = "+", Value2 = "-", Value3 = "+", Value4 = "", Value5 = "", Value6 = "", Value7 = "" },
            Quadrant3 = new Quadrant
                { Value1 = "+", Value2 = "-", Value3 = "+", Value4 = "", Value5 = "", Value6 = "", Value7 = "" },
            Quadrant4 = new Quadrant
                { Value1 = "+", Value2 = "-", Value3 = "+", Value4 = "", Value5 = "", Value6 = "", Value7 = "" }
        };

        var bleeding = new Bleeding();

        bleeding.SetAssessmentModel(assessmentModel);

        // Act
        bleeding.CalculateBleedingResult();

        // Assert
        Assert.Equal(67, bleeding.BleedingResult);
        Assert.Equal(67, bleeding.Maxilla);
        Assert.Equal(67, bleeding.Mandible);
    }
}