using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Test.Models.OralHealthExamination;

public class BeweTests
{
    [Fact]
    public void CalculateBeweResult_AssessmentModelIsNull_ShouldNotThrowException()
    {
        // Arrange
        var bewe = new Bewe();

        // Act & Assert
        var exception = Record.Exception(() => bewe.CalculateBeweResult());
        Assert.IsType<NullReferenceException>(exception);
    }

    [Fact]
    public void CalculateBeweResult_AllSectantsSameValue_ShouldCalculateCorrectly()
    {
        // Arrange
        var assessmentModel = new BeweAssessmentModel
        {
            Sectant1 = new Sectant1
            {
                Tooth_17 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_16 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_15 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_14 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" }
            },
            Sectant2 = new Sectant2
            {
                Tooth_13 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_12 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_11 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_21 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_22 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_23 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" }
            },
            Sectant3 = new Sectant3
            {
                Tooth_24 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_25 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_26 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_27 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" }
            },
            Sectant4 = new Sectant4
            {
                Tooth_34 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_35 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_36 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_37 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" }
            },
            Sectant5 = new Sectant5
            {
                Tooth_43 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_42 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_41 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_31 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_32 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" },
                Tooth_33 = new FourSurfaceTooth { B = "2", L = "", D = "", M = "" }
            },
            Sectant6 = new Sectant6
            {
                Tooth_47 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_46 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_45 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_44 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" }
            }
        };
        var bewe = new Bewe();
        bewe.SetAssessmentModel(assessmentModel);

        // Act
        bewe.CalculateBeweResult();

        // Assert
        Assert.Equal(2, bewe.Sectant1);
        Assert.Equal(2, bewe.Sectant2);
        Assert.Equal(2, bewe.Sectant3);
        Assert.Equal(2, bewe.Sectant4);
        Assert.Equal(2, bewe.Sectant5);
        Assert.Equal(2, bewe.Sectant6);
        Assert.Equal(12, bewe.BeweResult);
    }

    [Fact]
    public void CalculateBeweResult_DifferentSectantValues_ShouldCalculateCorrectly()
    {
        // Arrange
        var assessmentModel = new BeweAssessmentModel
        {
            Sectant1 = new Sectant1
            {
                Tooth_17 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_16 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_15 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_14 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" }
            },
            Sectant2 = new Sectant2
            {
                Tooth_13 = new FourSurfaceTooth { B = "3", L = "", D = "", M = "" },
                Tooth_12 = new FourSurfaceTooth { B = "3", L = "", D = "", M = "" },
                Tooth_11 = new FourSurfaceTooth { B = "3", L = "", D = "", M = "" },
                Tooth_21 = new FourSurfaceTooth { B = "3", L = "", D = "", M = "" },
                Tooth_22 = new FourSurfaceTooth { B = "3", L = "", D = "", M = "" },
                Tooth_23 = new FourSurfaceTooth { B = "3", L = "", D = "", M = "" }
            },
            Sectant3 = new Sectant3
            {
                Tooth_24 = new FiveSurfaceToothBEWE { O = "", B = "", L = "", D = "", M = "" },
                Tooth_25 = new FiveSurfaceToothBEWE { O = "", B = "", L = "", D = "", M = "" },
                Tooth_26 = new FiveSurfaceToothBEWE { O = "", B = "", L = "", D = "", M = "" },
                Tooth_27 = new FiveSurfaceToothBEWE { O = "", B = "", L = "", D = "", M = "" }
            },
            Sectant4 = new Sectant4
            {
                Tooth_34 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_35 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_36 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" },
                Tooth_37 = new FiveSurfaceToothBEWE { O = "", B = "2", L = "", D = "", M = "" }
            },
            Sectant5 = new Sectant5
            {
                Tooth_43 = new FourSurfaceTooth { B = "1", L = "", D = "", M = "" },
                Tooth_42 = new FourSurfaceTooth { B = "1", L = "", D = "", M = "" },
                Tooth_41 = new FourSurfaceTooth { B = "1", L = "", D = "", M = "" },
                Tooth_31 = new FourSurfaceTooth { B = "1", L = "", D = "", M = "" },
                Tooth_32 = new FourSurfaceTooth { B = "1", L = "", D = "", M = "" },
                Tooth_33 = new FourSurfaceTooth { B = "1", L = "", D = "", M = "" }
            },
            Sectant6 = new Sectant6
            {
                Tooth_47 = new FiveSurfaceToothBEWE { O = "", B = "", L = "", D = "", M = "" },
                Tooth_46 = new FiveSurfaceToothBEWE { O = "", B = "", L = "", D = "", M = "" },
                Tooth_45 = new FiveSurfaceToothBEWE { O = "", B = "", L = "", D = "", M = "" },
                Tooth_44 = new FiveSurfaceToothBEWE { O = "", B = "", L = "", D = "", M = "" }
            }
        };

        var bewe = new Bewe();
        bewe.SetAssessmentModel(assessmentModel);

        // Act
        bewe.CalculateBeweResult();

        // Assert
        Assert.Equal(2, bewe.Sectant1);
        Assert.Equal(3, bewe.Sectant2);
        Assert.Equal(0, bewe.Sectant3);
        Assert.Equal(2, bewe.Sectant4);
        Assert.Equal(1, bewe.Sectant5);
        Assert.Equal(0, bewe.Sectant6);
        Assert.Equal(8, bewe.BeweResult);
    }
}