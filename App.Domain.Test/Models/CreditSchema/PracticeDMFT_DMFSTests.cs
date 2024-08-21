using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.CreditSchema;

namespace App.Domain.Test.Models.CreditSchema;

public class PracticeDMFT_DMFSTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        decimal dMFTResult = 3.5m;
        decimal dMFSResult = 7.2m;

        // Act
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(dMFTResult, dMFSResult);

        // Assert
        Assert.NotEqual(Guid.Empty, practiceDMFT_DMFS.Id);
        Assert.Equal(dMFTResult, practiceDMFT_DMFS.DMFTResult);
        Assert.Equal(dMFSResult, practiceDMFT_DMFS.DMFSResult);
        Assert.Null(practiceDMFT_DMFS.Comment);
        Assert.Null(practiceDMFT_DMFS.AssessmentModel);
        Assert.Null(practiceDMFT_DMFS.PatientExaminationResult);
        Assert.Null(practiceDMFT_DMFS.ProstheticStatus);
    }

    [Fact]
    public void AddComment_ShouldSetComment()
    {
        // Arrange
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(3.5m, 7.2m);
        string comment = "This is a test comment.";

        // Act
        practiceDMFT_DMFS.AddComment(comment);

        // Assert
        Assert.Equal(comment, practiceDMFT_DMFS.Comment);
    }

    [Fact]
    public void SetAssessmentModel_ShouldSetAssessmentModel()
    {
        // Arrange
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(3.5m, 7.2m);
        var assessmentModel = new DMFT_DMFSAssessmentModel();

        // Act
        practiceDMFT_DMFS.SetAssessmentModel(assessmentModel);

        // Assert
        Assert.Equal(assessmentModel, practiceDMFT_DMFS.AssessmentModel);
    }

    [Fact]
    public void SetProstheticStatus_ShouldSetProstheticStatus()
    {
        // Arrange
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(3.5m, 7.2m);
        string prostheticStatus = "Complete";

        // Act
        practiceDMFT_DMFS.SetProstheticStatus(prostheticStatus);

        // Assert
        Assert.Equal(prostheticStatus, practiceDMFT_DMFS.ProstheticStatus);
    }
}

