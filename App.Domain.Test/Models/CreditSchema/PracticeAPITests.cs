using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.CreditSchema;

namespace App.Domain.Test.Models.CreditSchema;

public class PracticeAPITests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        int aPIResult = 85;
        int maxilla = 40;
        int mandible = 45;

        // Act
        var practiceAPI = new PracticeAPI(aPIResult, maxilla, mandible);

        // Assert
        Assert.NotEqual(Guid.Empty, practiceAPI.Id);
        Assert.Equal(aPIResult, practiceAPI.APIResult);
        Assert.Equal(maxilla, practiceAPI.Maxilla);
        Assert.Equal(mandible, practiceAPI.Mandible);
        Assert.Null(practiceAPI.Comment);
        Assert.Null(practiceAPI.AssessmentModel);
        Assert.Null(practiceAPI.PatientExaminationResult);
    }

    [Fact]
    public void AddComment_ShouldSetComment()
    {
        // Arrange
        var practiceAPI = new PracticeAPI(85, 40, 45);
        string comment = "This is a test comment.";

        // Act
        practiceAPI.AddComment(comment);

        // Assert
        Assert.Equal(comment, practiceAPI.Comment);
    }

    [Fact]
    public void SetAssessmentModel_ShouldSetAssessmentModel()
    {
        // Arrange
        var practiceAPI = new PracticeAPI(85, 40, 45);
        var assessmentModel = new APIBleedingAssessmentModel();

        // Act
        practiceAPI.SetAssessmentModel(assessmentModel);

        // Assert
        Assert.Equal(assessmentModel, practiceAPI.AssessmentModel);
    }
}
