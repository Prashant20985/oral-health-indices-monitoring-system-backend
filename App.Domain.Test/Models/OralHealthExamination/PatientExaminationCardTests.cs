using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Test.Models.OralHealthExamination;

public class PatientExaminationCardTests
{
    [Fact]
    public void SetTestMode_ShouldSetIsRegularModeToFalse()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());

        // Act
        patientExaminationCard.SetTestMode();

        // Assert
        Assert.False(patientExaminationCard.IsRegularMode);
    }

    [Fact]
    public void SetRegularMode_ShouldSetIsRegularModeToTrue()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());

        // Act
        patientExaminationCard.SetRegularMode();

        // Assert
        Assert.True(patientExaminationCard.IsRegularMode);
    }

    [Fact]
    public void SetDoctorId_ShouldSetDoctorId()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var doctorId = "doctorId";

        // Act
        patientExaminationCard.SetDoctorId(doctorId);

        // Assert
        Assert.Equal(doctorId, patientExaminationCard.DoctorId);
    }

    [Fact]
    public void SetStudentId_ShouldSetStudentId()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var studentId = "studentId";

        // Act
        patientExaminationCard.SetStudentId(studentId);

        // Assert
        Assert.Equal(studentId, patientExaminationCard.StudentId);
    }

    [Fact]
    public void SetRiskFactorAssesmentId_ShouldSetRiskFactorAssesmentId()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var riskFactorAssesmentId = Guid.NewGuid();

        // Act
        patientExaminationCard.SetRiskFactorAssesmentId(riskFactorAssesmentId);

        // Assert
        Assert.Equal(riskFactorAssesmentId, patientExaminationCard.RiskFactorAssesmentId);
    }

    [Fact]
    public void SetPatientExaminationResultId_ShouldSetPatientExaminationResultId()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var patientExaminationResultId = Guid.NewGuid();

        // Act
        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResultId);

        // Assert
        Assert.Equal(patientExaminationResultId, patientExaminationCard.PatientExaminationResultId);
    }

    [Fact]
    public void SetTotalScore_ShouldSetTotalScore()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var totalScore = 95.5m;

        // Act
        patientExaminationCard.SetTotalScore(totalScore);

        // Assert
        Assert.Equal(totalScore, patientExaminationCard.TotalScore);
    }

    [Fact]
    public void AddDoctorComment_ShouldSetDoctorComment()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var comment = "Doctor's comment";

        // Act
        patientExaminationCard.AddDoctorComment(comment);

        // Assert
        Assert.Equal(comment, patientExaminationCard.DoctorComment);
    }

    [Fact]
    public void AddStudentComment_ShouldSetStudentComment()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var comment = "Student's comment";

        // Act
        patientExaminationCard.AddStudentComment(comment);

        // Assert
        Assert.Equal(comment, patientExaminationCard.StudentComment);
    }

    [Fact]
    public void SetNeedForDentalInterventions_ShouldSetNeedForDentalInterventions()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var needForDentalInterventions = "Need for dental interventions";

        // Act
        patientExaminationCard.SetNeedForDentalInterventions(needForDentalInterventions);

        // Assert
        Assert.Equal(needForDentalInterventions, patientExaminationCard.NeedForDentalInterventions);
    }

    [Fact]
    public void SetProposedTreatment_ShouldSetProposedTreatment()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var proposedTreatment = "Proposed treatment";

        // Act
        patientExaminationCard.SetProposedTreatment(proposedTreatment);

        // Assert
        Assert.Equal(proposedTreatment, patientExaminationCard.ProposedTreatment);
    }

    [Fact]
    public void SetDescription_ShouldSetDescription()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var description = "Description";

        // Act
        patientExaminationCard.SetDescription(description);

        // Assert
        Assert.Equal(description, patientExaminationCard.Description);
    }

    [Fact]
    public void SetPatientRecommendations_ShouldSetPatientRecommendations()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var patientRecommendations = "Patient recommendations";

        // Act
        patientExaminationCard.SetPatientRecommendations(patientRecommendations);

        // Assert
        Assert.Equal(patientRecommendations, patientExaminationCard.PatientRecommendations);
    }
}