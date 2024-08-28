using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;
/// <summary>
/// Represents a patient examination card with details about the examination, comments, and associated entities.
/// </summary>
public class PatientExaminationCard
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatientExaminationCard"/> class.
    /// </summary>
    /// <param name="patientId">The ID of the patient associated with the examination card.</param>
    public PatientExaminationCard(Guid patientId)
    {
        Id = Guid.NewGuid();
        DateOfExamination = DateTime.UtcNow;
        PatientId = patientId;
    }

    /// <summary>
    /// Gets the unique identifier for the examination card.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the date of the examination.
    /// </summary>
    public DateTime DateOfExamination { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the examination is in regular mode.
    /// </summary>
    public bool IsRegularMode { get; private set; }

    /// <summary>
    /// Gets the doctor's comment.
    /// </summary>
    public string DoctorComment { get; private set; }

    /// <summary>
    /// Gets the student's comment.
    /// </summary>
    public string StudentComment { get; private set; }

    /// <summary>
    /// Gets the total score of the examination.
    /// </summary>
    public decimal? TotalScore { get; private set; }

    /// <summary>
    /// Gets the need for dental interventions.
    /// </summary>
    public string NeedForDentalInterventions { get; private set; }

    /// <summary>
    /// Gets the proposed treatment.
    /// </summary>
    public string ProposedTreatment { get; private set; }

    /// <summary>
    /// Gets the description of the examination.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the recommendations for the patient.
    /// </summary>
    public string PatientRecommendations { get; private set; }

    /// <summary>
    /// Gets the ID of the patient associated with the examination card.
    /// </summary>
    public Guid PatientId { get; private set; }

    /// <summary>
    /// Gets or sets the patient associated with the examination card.
    /// </summary>
    public virtual Patient Patient { get; set; }

    /// <summary>
    /// Gets the ID of the risk factor assessment associated with the examination card.
    /// </summary>
    public Guid RiskFactorAssesmentId { get; private set; }

    /// <summary>
    /// Gets or sets the risk factor assessment associated with the examination card.
    /// </summary>
    public virtual RiskFactorAssessment RiskFactorAssessment { get; set; }

    /// <summary>
    /// Gets the ID of the doctor associated with the examination card.
    /// </summary>
    public string DoctorId { get; private set; }

    /// <summary>
    /// Gets or sets the doctor associated with the examination card.
    /// </summary>
    public virtual ApplicationUser Doctor { get; set; }

    /// <summary>
    /// Gets the ID of the student associated with the examination card.
    /// </summary>
    public string? StudentId { get; private set; }

    /// <summary>
    /// Gets or sets the student associated with the examination card.
    /// </summary>
    public virtual ApplicationUser Student { get; set; }

    /// <summary>
    /// Gets the ID of the patient examination result associated with the examination card.
    /// </summary>
    public Guid PatientExaminationResultId { get; private set; }

    /// <summary>
    /// Gets or sets the patient examination result associated with the examination card.
    /// </summary>
    public virtual PatientExaminationResult PatientExaminationResult { get; set; }

    /// <summary>
    /// Sets the examination to test mode.
    /// </summary>
    public void SetTestMode() => IsRegularMode = false;

    /// <summary>
    /// Sets the examination to regular mode.
    /// </summary>
    public void SetRegularMode() => IsRegularMode = true;

    /// <summary>
    /// Sets the ID of the doctor associated with the examination card.
    /// </summary>
    /// <param name="doctorId">The ID of the doctor to set.</param>
    public void SetDoctorId(string doctorId) => DoctorId = doctorId;

    /// <summary>
    /// Sets the ID of the student associated with the examination card.
    /// </summary>
    /// <param name="studentId">The ID of the student to set.</param>
    public void SetStudentId(string studentId) => StudentId = studentId;

    /// <summary>
    /// Sets the ID of the risk factor assessment associated with the examination card.
    /// </summary>
    /// <param name="riskFactorAssesmentId">The ID of the risk factor assessment to set.</param>
    public void SetRiskFactorAssesmentId(Guid riskFactorAssesmentId) => RiskFactorAssesmentId = riskFactorAssesmentId;

    /// <summary>
    /// Sets the ID of the patient examination result associated with the examination card.
    /// </summary>
    /// <param name="patientExaminationResultId">The ID of the patient examination result to set.</param>
    public void SetPatientExaminationResultId(Guid patientExaminationResultId) => PatientExaminationResultId = patientExaminationResultId;

    /// <summary>
    /// Sets the total score of the examination.
    /// </summary>
    /// <param name="totalScore">The total score to set.</param>
    public void SetTotalScore(decimal totalScore) => TotalScore = totalScore;

    /// <summary>
    /// Adds a comment from the doctor.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    public void AddDoctorComment(string comment) => DoctorComment = comment;

    /// <summary>
    /// Adds a comment from the student.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    public void AddStudentComment(string comment) => StudentComment = comment;

    /// <summary>
    /// Sets the need for dental interventions.
    /// </summary>
    /// <param name="needForDentalInterventions">The need for dental interventions to set.</param>
    public void SetNeedForDentalInterventions(string needForDentalInterventions) => NeedForDentalInterventions = needForDentalInterventions;

    /// <summary>
    /// Sets the proposed treatment.
    /// </summary>
    /// <param name="proposedTreatment">The proposed treatment to set.</param>
    public void SetProposedTreatment(string proposedTreatment) => ProposedTreatment = proposedTreatment;

    /// <summary>
    /// Sets the description of the examination.
    /// </summary>
    /// <param name="description">The description to set.</param>
    public void SetDescription(string description) => Description = description;

    /// <summary>
    /// Sets the recommendations for the patient.
    /// </summary>
    /// <param name="patientRecommendations">The recommendations to set.</param>
    public void SetPatientRecommendations(string patientRecommendations) => PatientRecommendations = patientRecommendations;
}