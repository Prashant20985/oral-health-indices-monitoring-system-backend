using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Test.Models.OralHealthExamination;

public class DMFT_DMFSTests
{
    [Fact]
    public void AddDoctorComment_ShouldSetDoctorComment()
    {
        // Arrange
        var dMFT_DMFS = new DMFT_DMFS();
        var comment = "Doctor's comment";

        // Act
        dMFT_DMFS.AddDoctorComment(comment);

        // Assert
        Assert.Equal(comment, dMFT_DMFS.DoctorComment);
    }

    [Fact]
    public void AddStudentComment_ShouldSetStudentComment()
    {
        // Arrange
        var dMFT_DMFS = new DMFT_DMFS();
        var comment = "Student's comment";

        // Act
        dMFT_DMFS.AddStudentComment(comment);

        // Assert
        Assert.Equal(comment, dMFT_DMFS.StudentComment);
    }

    [Fact]
    public void SetDMFTResult_ShouldSetDMFTResult()
    {
        // Arrange
        var dMFT_DMFS = new DMFT_DMFS();
        var dmftResult = 5.5m;

        // Act
        dMFT_DMFS.SetDMFTResult(dmftResult);

        // Assert
        Assert.Equal(dmftResult, dMFT_DMFS.DMFTResult);
    }

    [Fact]
    public void SetDMFSResult_ShouldSetDMFSResult()
    {
        // Arrange
        var dMFT_DMFS = new DMFT_DMFS();
        var dmfsResult = 10.2m;

        // Act
        dMFT_DMFS.SetDMFSResult(dmfsResult);

        // Assert
        Assert.Equal(dmfsResult, dMFT_DMFS.DMFSResult);
    }

    [Fact]
    public void SetDMFT_DMFSAssessmentModel_ShouldSetAssessmentModel()
    {
        // Arrange
        var dMFT_DMFS = new DMFT_DMFS();
        var assessmentModel = new DMFT_DMFSAssessmentModel();

        // Act
        dMFT_DMFS.SetDMFT_DMFSAssessmentModel(assessmentModel);

        // Assert
        Assert.Equal(assessmentModel, dMFT_DMFS.AssessmentModel);
    }

    [Fact]
    public void SetProstheticStatus_ShouldSetProstheticStatus()
    {
        // Arrange
        var dMFT_DMFS = new DMFT_DMFS();
        var prostheticStatus = "Status";

        // Act
        dMFT_DMFS.SetProstheticStatus(prostheticStatus);

        // Assert
        Assert.Equal(prostheticStatus, dMFT_DMFS.ProstheticStatus);
    }
}