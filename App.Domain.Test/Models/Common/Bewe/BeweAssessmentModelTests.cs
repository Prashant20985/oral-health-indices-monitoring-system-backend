using App.Domain.Models.Common.Bewe;

namespace App.Domain.Test.Models.Common.Bewe;

public class BeweAssessmentModelTests
{
    [Fact]
    public void Sectants_ShouldBeInitializedCorrectly()
    {
        // Arrange
        var sectant1 = new Sectant1();
        var sectant2 = new Sectant2();
        var sectant3 = new Sectant3();
        var sectant4 = new Sectant4();
        var sectant5 = new Sectant5();
        var sectant6 = new Sectant6();

        // Act
        var model = new BeweAssessmentModel
        {
            Sectant1 = sectant1,
            Sectant2 = sectant2,
            Sectant3 = sectant3,
            Sectant4 = sectant4,
            Sectant5 = sectant5,
            Sectant6 = sectant6
        };

        // Assert
        Assert.Equal(sectant1, model.Sectant1);
        Assert.Equal(sectant2, model.Sectant2);
        Assert.Equal(sectant3, model.Sectant3);
        Assert.Equal(sectant4, model.Sectant4);
        Assert.Equal(sectant5, model.Sectant5);
        Assert.Equal(sectant6, model.Sectant6);
    }

    [Fact]
    public void Sectants_ShouldBeAssignable()
    {
        // Arrange
        var model = new BeweAssessmentModel();
        var sectant1 = new Sectant1();
        var sectant2 = new Sectant2();
        var sectant3 = new Sectant3();
        var sectant4 = new Sectant4();
        var sectant5 = new Sectant5();
        var sectant6 = new Sectant6();

        // Act
        model.Sectant1 = sectant1;
        model.Sectant2 = sectant2;
        model.Sectant3 = sectant3;
        model.Sectant4 = sectant4;
        model.Sectant5 = sectant5;
        model.Sectant6 = sectant6;

        // Assert
        Assert.Equal(sectant1, model.Sectant1);
        Assert.Equal(sectant2, model.Sectant2);
        Assert.Equal(sectant3, model.Sectant3);
        Assert.Equal(sectant4, model.Sectant4);
        Assert.Equal(sectant5, model.Sectant5);
        Assert.Equal(sectant6, model.Sectant6);
    }
}