using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.CreditSchema;

namespace App.Domain.Test.Models.CreditSchema;

public class PracticeBleedingTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        int bleedingResult = 90;
        int maxilla = 45;
        int mandible = 45;

        // Act
        var practiceBleeding = new PracticeBleeding(bleedingResult, maxilla, mandible);

        // Assert
        Assert.NotEqual(Guid.Empty, practiceBleeding.Id);
        Assert.Equal(bleedingResult, practiceBleeding.BleedingResult);
        Assert.Equal(maxilla, practiceBleeding.Maxilla);
        Assert.Equal(mandible, practiceBleeding.Mandible);
        Assert.Null(practiceBleeding.Comment);
        Assert.Null(practiceBleeding.AssessmentModel);
        Assert.Null(practiceBleeding.PatientExaminationResult);
    }

    [Fact]
    public void AddComment_ShouldSetComment()
    {
        // Arrange
        var practiceBleeding = new PracticeBleeding(90, 45, 45);
        string comment = "This is a test comment.";

        // Act
        practiceBleeding.AddComment(comment);

        // Assert
        Assert.Equal(comment, practiceBleeding.Comment);
    }

    [Fact]
    public void SetAssessmentModel_ShouldSetAssessmentModel()
    {
        // Arrange
        var practiceBleeding = new PracticeBleeding(90, 45, 45);
        var assessmentModel = new APIBleedingAssessmentModel();

        // Act
        practiceBleeding.SetAssessmentModel(assessmentModel);

        // Assert
        Assert.Equal(assessmentModel, practiceBleeding.AssessmentModel);
    }

}
