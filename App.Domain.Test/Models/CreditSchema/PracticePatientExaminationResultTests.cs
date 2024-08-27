using App.Domain.Models.CreditSchema;

namespace App.Domain.Test.Models.CreditSchema;

public class PracticePatientExaminationResultTests
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
        var practicePatientExaminationResult =
            new PracticePatientExaminationResult(beweId, dMFT_DMFSId, aPIId, bleedingId);

        // Assert
        Assert.NotEqual(Guid.Empty, practicePatientExaminationResult.Id);
        Assert.Equal(beweId, practicePatientExaminationResult.BeweId);
        Assert.Equal(dMFT_DMFSId, practicePatientExaminationResult.DMFT_DMFSId);
        Assert.Equal(aPIId, practicePatientExaminationResult.APIId);
        Assert.Equal(bleedingId, practicePatientExaminationResult.BleedingId);
        Assert.Null(practicePatientExaminationResult.Bewe);
        Assert.Null(practicePatientExaminationResult.DMFT_DMFS);
        Assert.Null(practicePatientExaminationResult.API);
        Assert.Null(practicePatientExaminationResult.Bleeding);
        Assert.Null(practicePatientExaminationResult.PracticePatientExaminationCard);
    }

    [Fact]
    public void SetBewe_ShouldSetBewe()
    {
        // Arrange
        var practicePatientExaminationResult =
            new PracticePatientExaminationResult(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        var bewe = new PracticeBewe(18);

        // Act
        practicePatientExaminationResult.Bewe = bewe;

        // Assert
        Assert.Equal(bewe, practicePatientExaminationResult.Bewe);
    }

    [Fact]
    public void SetDMFT_DMFS_ShouldSetDMFT_DMFS()
    {
        // Arrange
        var practicePatientExaminationResult =
            new PracticePatientExaminationResult(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        var dMFT_DMFS = new PracticeDMFT_DMFS(18, 18);

        // Act
        practicePatientExaminationResult.DMFT_DMFS = dMFT_DMFS;

        // Assert
        Assert.Equal(dMFT_DMFS, practicePatientExaminationResult.DMFT_DMFS);
    }

    [Fact]
    public void SetAPI_ShouldSetAPI()
    {
        // Arrange
        var practicePatientExaminationResult =
            new PracticePatientExaminationResult(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        var api = new PracticeAPI(16, 16, 16);

        // Act
        practicePatientExaminationResult.API = api;

        // Assert
        Assert.Equal(api, practicePatientExaminationResult.API);
    }

    [Fact]
    public void SetBleeding_ShouldSetBleeding()
    {
        // Arrange
        var practicePatientExaminationResult =
            new PracticePatientExaminationResult(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        var bleeding = new PracticeBleeding(16, 16, 16);

        // Act
        practicePatientExaminationResult.Bleeding = bleeding;

        // Assert
        Assert.Equal(bleeding, practicePatientExaminationResult.Bleeding);
    }

    [Fact]
    public void SetPracticePatientExaminationCard_ShouldSetPracticePatientExaminationCard()
    {
        // Arrange
        var practicePatientExaminationResult =
            new PracticePatientExaminationResult(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");

        // Act
        practicePatientExaminationResult.PracticePatientExaminationCard = practicePatientExaminationCard;

        // Assert
        Assert.Equal(practicePatientExaminationCard, practicePatientExaminationResult.PracticePatientExaminationCard);
    }
}