using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Test.Models.OralHealthExamination;

public class PatientExaminationResultTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var beweId = Guid.NewGuid();
        var dMFT_DMFSId = Guid.NewGuid();
        var aPIId = Guid.NewGuid();
        var bleedingId = Guid.NewGuid();

        // Act
        var patientExaminationResult = new PatientExaminationResult(beweId, dMFT_DMFSId, aPIId, bleedingId);

        // Assert
        Assert.Equal(beweId, patientExaminationResult.BeweId);
        Assert.Equal(dMFT_DMFSId, patientExaminationResult.DMFT_DMFSId);
        Assert.Equal(aPIId, patientExaminationResult.APIId);
        Assert.Equal(bleedingId, patientExaminationResult.BleedingId);
    }
}