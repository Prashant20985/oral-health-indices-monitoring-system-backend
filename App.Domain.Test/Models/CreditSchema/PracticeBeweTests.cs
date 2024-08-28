using App.Domain.Models.Common.Bewe;
using App.Domain.Models.CreditSchema;

namespace App.Domain.Test.Models.CreditSchema;

public class PracticeBeweTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var beweResult = 5.5m;

        // Act
        var practiceBewe = new PracticeBewe(beweResult);

        // Assert
        Assert.NotEqual(Guid.Empty, practiceBewe.Id);
        Assert.Equal(beweResult, practiceBewe.BeweResult);
        Assert.Equal(0, practiceBewe.Sectant1);
        Assert.Equal(0, practiceBewe.Sectant2);
        Assert.Equal(0, practiceBewe.Sectant3);
        Assert.Equal(0, practiceBewe.Sectant4);
        Assert.Equal(0, practiceBewe.Sectant5);
        Assert.Equal(0, practiceBewe.Sectant6);
        Assert.Null(practiceBewe.Comment);
        Assert.Null(practiceBewe.AssessmentModel);
        Assert.Null(practiceBewe.PatientExaminationResult);
    }

    [Fact]
    public void AddComment_ShouldSetComment()
    {
        // Arrange
        var practiceBewe = new PracticeBewe(5.5m);
        var comment = "This is a test comment.";

        // Act
        practiceBewe.AddComment(comment);

        // Assert
        Assert.Equal(comment, practiceBewe.Comment);
    }

    [Fact]
    public void SetAssessmentModel_ShouldSetAssessmentModel()
    {
        // Arrange
        var practiceBewe = new PracticeBewe(5.5m);
        var assessmentModel = new BeweAssessmentModel();

        // Act
        practiceBewe.SetAssessmentModel(assessmentModel);

        // Assert
        Assert.Equal(assessmentModel, practiceBewe.AssessmentModel);
    }

    [Fact]
    public void SetSectant1_ShouldSetSectant1()
    {
        // Arrange
        var practiceBewe = new PracticeBewe(5.5m);
        var sectant1 = 1.1m;

        // Act
        practiceBewe.SetSectant1(sectant1);

        // Assert
        Assert.Equal(sectant1, practiceBewe.Sectant1);
    }

    [Fact]
    public void SetSectant2_ShouldSetSectant2()
    {
        // Arrange
        var practiceBewe = new PracticeBewe(5.5m);
        var sectant2 = 2.2m;

        // Act
        practiceBewe.SetSectant2(sectant2);

        // Assert
        Assert.Equal(sectant2, practiceBewe.Sectant2);
    }

    [Fact]
    public void SetSectant3_ShouldSetSectant3()
    {
        // Arrange
        var practiceBewe = new PracticeBewe(5.5m);
        var sectant3 = 3.3m;

        // Act
        practiceBewe.SetSectant3(sectant3);

        // Assert
        Assert.Equal(sectant3, practiceBewe.Sectant3);
    }

    [Fact]
    public void SetSectant4_ShouldSetSectant4()
    {
        // Arrange
        var practiceBewe = new PracticeBewe(5.5m);
        var sectant4 = 4.4m;

        // Act
        practiceBewe.SetSectant4(sectant4);

        // Assert
        Assert.Equal(sectant4, practiceBewe.Sectant4);
    }

    [Fact]
    public void SetSectant5_ShouldSetSectant5()
    {
        // Arrange
        var practiceBewe = new PracticeBewe(5.5m);
        var sectant5 = 5.5m;

        // Act
        practiceBewe.SetSectant5(sectant5);

        // Assert
        Assert.Equal(sectant5, practiceBewe.Sectant5);
    }

    [Fact]
    public void SetSectant6_ShouldSetSectant6()
    {
        // Arrange
        var practiceBewe = new PracticeBewe(5.5m);
        var sectant6 = 6.6m;

        // Act
        practiceBewe.SetSectant6(sectant6);

        // Assert
        Assert.Equal(sectant6, practiceBewe.Sectant6);
    }
}