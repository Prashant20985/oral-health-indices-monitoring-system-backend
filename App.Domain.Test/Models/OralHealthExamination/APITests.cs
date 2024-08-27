using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Test.Models.OralHealthExamination;

public class APITests
{
    [Fact]
    public void CalculateAPIResult_AssessmentModelIsNull_ShouldSetAPIResultToZero()
    {
        // Arrange
        var api = new API();

        // Act
        api.CalculateAPIResult();

        // Assert
        Assert.Equal(0, api.APIResult);
        Assert.Equal(0, api.Maxilla);
        Assert.Equal(0, api.Mandible);
    }

    [Fact]
    public void CalculateAPIResult_AllQuadrantsPlus_ShouldCalculateCorrectly()
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
        var api = new API();
        api.SetAssessmentModel(assessmentModel);

        // Act
        api.CalculateAPIResult();

        // Assert
        Assert.Equal(100, api.APIResult);
        Assert.Equal(100, api.Maxilla);
        Assert.Equal(100, api.Mandible);
    }

    [Fact]
    public void CalculateAPIResult_AllQuadrantsMinus_ShouldCalculateCorrectly()
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
        var api = new API();
        api.SetAssessmentModel(assessmentModel);

        // Act
        api.CalculateAPIResult();

        // Assert
        Assert.Equal(0, api.APIResult);
        Assert.Equal(0, api.Maxilla);
        Assert.Equal(0, api.Mandible);
    }

    [Fact]
    public void CalculateAPIResult_MixedQuadrants_ShouldCalculateCorrectly()
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
        var api = new API();
        api.SetAssessmentModel(assessmentModel);

        // Act
        api.CalculateAPIResult();

        // Assert
        Assert.Equal(67, api.APIResult);
        Assert.Equal(67, api.Maxilla);
        Assert.Equal(67, api.Mandible);
    }
}