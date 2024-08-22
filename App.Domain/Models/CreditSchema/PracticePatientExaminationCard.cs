using App.Domain.Models.Users;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents the result of an examination for a student in a practice scenario.
/// </summary>
public class PracticePatientExaminationCard(Guid examId, string studentId)
{
    /// <summary>
    /// Gets or sets the unique identifier of the student examination result.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the mark obtained by the student in the examination.
    /// </summary>
    public decimal StudentMark { get; private set; }

    /// <summary>
    /// Gets or sets the comment provided by the doctor regarding the examination.
    /// </summary>
    public string DoctorComment { get; private set; }

    /// <summary>
    /// Gets or sets the need for dental interventions identified during the examination.
    /// </summary>
    public string NeedForDentalInterventions { get; private set; }

    /// <summary>
    /// Gets or sets the proposed treatment for the patient based on the examination.
    /// </summary>
    public string ProposedTreatment { get; private set; }

    /// <summary>
    /// Gets or sets the description of the examination.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets or sets the recommendations provided to the patient based on the examination.
    /// </summary>
    public string PatientRecommendations { get; private set; }

    /// <summary>
    /// Gets or sets the unique identifier of the risk factor assessment associated with this examination.
    /// </summary>
    public Guid RiskFactorAssessmentId { get; private set; }

    /// <summary>
    /// Gets or sets the risk factor assessment associated with this examination.
    /// </summary>
    public virtual PracticeRiskFactorAssessment PracticeRiskFactorAssessment { get; private set; }

    /// <summary>
    /// Gets or sets the unique identifier of the patient examination result associated with this examination.
    /// </summary>
    public Guid PatientExaminationResultId { get; private set; }

    /// <summary>
    /// Gets or sets the patient examination result associated with this examination.
    /// </summary>
    public virtual PracticePatientExaminationResult PracticePatientExaminationResult { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the exam associated with this examination.
    /// </summary>
    public Guid ExamId { get; private set; } = examId;

    /// <summary>
    /// Gets or sets the exam associated with this examination.
    /// </summary>
    public virtual Exam Exam { get; private set; }

    /// <summary>
    /// Gets or sets the unique identifier of the patient associated with this examination.
    /// </summary>
    public Guid PatientId { get; private set; }

    /// <summary>
    /// Gets or sets the patient associated with this examination.
    /// </summary>
    public virtual PracticePatient PracticePatient { get; private set; }

    /// <summary>
    /// Gets or sets the student identifier associated with this examination.
    /// </summary>
    public string StudentId { get; private set; } = studentId;

    /// <summary>
    /// Gets or sets the student associated with this examination.
    /// </summary>
    public virtual ApplicationUser Student { get; private set; }

    /// <summary>
    ///  Sets the unique identifier of the patient associated with this examination.
    /// </summary>
    /// <param name="patientId"></param>
    public void SetPatientId(Guid patientId) => PatientId = patientId;
    
    /// <summary>
    ///   Sets the mark obtained by the student in the examination.
    /// </summary>
    /// <param name="studentMark"></param>
    public void SetStudentMark(decimal studentMark) => StudentMark = studentMark;
    
    /// <summary>
    ///  Sets the comment provided by the doctor regarding the examination.
    /// </summary>
    /// <param name="doctorComment"></param>
    public void SetDoctorComment(string doctorComment) => DoctorComment = doctorComment;
    
    /// <summary>
    ///  Sets examination result.
    /// </summary>
    /// <param name="patientExaminationResultId"></param>
    public void SetPatientExaminationResultId(Guid patientExaminationResultId) => PatientExaminationResultId = patientExaminationResultId;
    
    /// <summary>
    ///  Sets risk factor assessment.
    /// </summary>
    /// <param name="riskFactorAssessmentId"></param>
    public void SetRiskFactorAssessmentId(Guid riskFactorAssessmentId) => RiskFactorAssessmentId = riskFactorAssessmentId;
    
    /// <summary>
    ///  Sets practice patient examination result.
    /// </summary>
    /// <param name="practicePatientExaminationResult"></param>
    public void SetPracticePatientExaminationResult(PracticePatientExaminationResult practicePatientExaminationResult) => PracticePatientExaminationResult = practicePatientExaminationResult;
    
    /// <summary>
    ///  Sets student.
    /// </summary>
    /// <param name="student"></param>
    public void SetStudent(ApplicationUser student) => Student = student;
    
    /// <summary>
    ///  Sets description of the examination.
    /// </summary>
    /// <param name="description"></param>
    public void SetDescription(string description) => Description = description;
    
    /// <summary>
    ///  Sets patient recommendations.
    /// </summary>
    /// <param name="patientRecommendations"></param>
    public void SetPatientRecommendations(string patientRecommendations) => PatientRecommendations = patientRecommendations;
    
    /// <summary>
    ///  Sets need for dental interventions.
    /// </summary>
    /// <param name="needForDentalInterventions"></param>
    public void SetNeedForDentalInterventions(string needForDentalInterventions) => NeedForDentalInterventions = needForDentalInterventions;
    
    /// <summary>
    ///  Sets proposed treatment.
    /// </summary>
    /// <param name="proposedTreatment"></param>
    public void SetProposedTreatment(string proposedTreatment) => ProposedTreatment = proposedTreatment;
}

