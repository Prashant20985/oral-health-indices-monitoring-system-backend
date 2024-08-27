using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.Test.Models.Common.DMFT_DMFS;

public class DMFT_DMFSAssessmentModelTests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var upperMouth = new UpperMouth();
        var lowerMouth = new LowerMouth();

        var assessmentModel = new DMFT_DMFSAssessmentModel
        {
            UpperMouth = upperMouth,
            LowerMouth = lowerMouth
        };

        // Assert
        Assert.Equal(upperMouth, assessmentModel.UpperMouth);
        Assert.Equal(lowerMouth, assessmentModel.LowerMouth);
    }

    [Fact]
    public void UpperMouth_ShouldBeNullByDefault()
    {
        // Arrange
        var assessmentModel = new DMFT_DMFSAssessmentModel();

        // Assert
        Assert.Null(assessmentModel.UpperMouth);
    }

    [Fact]
    public void LowerMouth_ShouldBeNullByDefault()
    {
        // Arrange
        var assessmentModel = new DMFT_DMFSAssessmentModel();

        // Assert
        Assert.Null(assessmentModel.LowerMouth);
    }
}