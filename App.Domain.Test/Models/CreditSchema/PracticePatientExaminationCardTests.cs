using App.Domain.Models.CreditSchema;
using App.Domain.Models.Users;

namespace App.Domain.Test.Models.CreditSchema;

public class PracticePatientExaminationCardTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        Guid examId = Guid.NewGuid();
        string studentId = "student123";

        // Act
        var practicePatientExaminationCard = new PracticePatientExaminationCard(examId, studentId);

        // Assert
        Assert.NotEqual(Guid.Empty, practicePatientExaminationCard.Id);
        Assert.Equal(examId, practicePatientExaminationCard.ExamId);
        Assert.Equal(studentId, practicePatientExaminationCard.StudentId);
        Assert.Null(practicePatientExaminationCard.DoctorComment);
        Assert.Null(practicePatientExaminationCard.NeedForDentalInterventions);
        Assert.Null(practicePatientExaminationCard.ProposedTreatment);
        Assert.Null(practicePatientExaminationCard.Description);
        Assert.Null(practicePatientExaminationCard.PatientRecommendations);
        Assert.Equal(Guid.Empty, practicePatientExaminationCard.RiskFactorAssessmentId);
        Assert.Null(practicePatientExaminationCard.PracticeRiskFactorAssessment);
        Assert.Equal(Guid.Empty, practicePatientExaminationCard.PatientExaminationResultId);
        Assert.Null(practicePatientExaminationCard.PracticePatientExaminationResult);
        Assert.Null(practicePatientExaminationCard.Exam);
        Assert.Equal(Guid.Empty, practicePatientExaminationCard.PatientId);
        Assert.Null(practicePatientExaminationCard.PracticePatient);
        Assert.Null(practicePatientExaminationCard.Student);
    }

    [Fact]
    public void SetPatientId_ShouldSetPatientId()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");
        Guid patientId = Guid.NewGuid();

        // Act
        practicePatientExaminationCard.SetPatientId(patientId);

        // Assert
        Assert.Equal(patientId, practicePatientExaminationCard.PatientId);
    }

    [Fact]
    public void SetStudentMark_ShouldSetStudentMark()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");
        decimal studentMark = 95.5m;

        // Act
        practicePatientExaminationCard.SetStudentMark(studentMark);

        // Assert
        Assert.Equal(studentMark, practicePatientExaminationCard.StudentMark);
    }

    [Fact]
    public void SetDoctorComment_ShouldSetDoctorComment()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");
        string doctorComment = "Good progress.";

        // Act
        practicePatientExaminationCard.SetDoctorComment(doctorComment);

        // Assert
        Assert.Equal(doctorComment, practicePatientExaminationCard.DoctorComment);
    }

    [Fact]
    public void SetPatientExaminationResultId_ShouldSetPatientExaminationResultId()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");
        Guid patientExaminationResultId = Guid.NewGuid();

        // Act
        practicePatientExaminationCard.SetPatientExaminationResultId(patientExaminationResultId);

        // Assert
        Assert.Equal(patientExaminationResultId, practicePatientExaminationCard.PatientExaminationResultId);
    }

    [Fact]
    public void SetRiskFactorAssessmentId_ShouldSetRiskFactorAssessmentId()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");
        Guid riskFactorAssessmentId = Guid.NewGuid();

        // Act
        practicePatientExaminationCard.SetRiskFactorAssessmentId(riskFactorAssessmentId);

        // Assert
        Assert.Equal(riskFactorAssessmentId, practicePatientExaminationCard.RiskFactorAssessmentId);
    }

    [Fact]
    public void SetPracticePatientExaminationResult_ShouldSetPracticePatientExaminationResult()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");

        var practicePatientExaminationResult = new PracticePatientExaminationResult(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        // Act
        practicePatientExaminationCard.SetPracticePatientExaminationResult(practicePatientExaminationResult);

        // Assert
        Assert.Equal(practicePatientExaminationResult, practicePatientExaminationCard.PracticePatientExaminationResult);
    }

    [Fact]
    public void SetStudent_ShouldSetStudent()
    {
        // Arrange
        var student = new ApplicationUser("test@test.com", "test", "user", "741852963", "");
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), student.Id);

        // Act
        practicePatientExaminationCard.SetStudent(student);

        // Assert
        Assert.Equal(student, practicePatientExaminationCard.Student);
    }

    [Fact]
    public void SetDescription_ShouldSetDescription()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");
        string description = "Detailed examination description.";

        // Act
        practicePatientExaminationCard.SetDescription(description);

        // Assert
        Assert.Equal(description, practicePatientExaminationCard.Description);
    }

    [Fact]
    public void SetPatientRecommendations_ShouldSetPatientRecommendations()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");
        string patientRecommendations = "Regular check-ups recommended.";

        // Act
        practicePatientExaminationCard.SetPatientRecommendations(patientRecommendations);

        // Assert
        Assert.Equal(patientRecommendations, practicePatientExaminationCard.PatientRecommendations);
    }

    [Fact]
    public void SetNeedForDentalInterventions_ShouldSetNeedForDentalInterventions()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");
        string needForDentalInterventions = "Requires cavity filling.";

        // Act
        practicePatientExaminationCard.SetNeedForDentalInterventions(needForDentalInterventions);

        // Assert
        Assert.Equal(needForDentalInterventions, practicePatientExaminationCard.NeedForDentalInterventions);
    }

    [Fact]
    public void SetProposedTreatment_ShouldSetProposedTreatment()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");
        string proposedTreatment = "Propose root canal treatment.";

        // Act
        practicePatientExaminationCard.SetProposedTreatment(proposedTreatment);

        // Assert
        Assert.Equal(proposedTreatment, practicePatientExaminationCard.ProposedTreatment);
    }
}
