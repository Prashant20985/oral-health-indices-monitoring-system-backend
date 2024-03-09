using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;

namespace App.Domain.Repository;

/// <summary>
/// Interface for interacting with student exams in the repository.
/// </summary>
public interface IStudentExamRepository
{
    /// <summary>
    /// Publishes an exam.
    /// </summary>
    /// <param name="exam">The exam to be published.</param>
    /// <returns>The published exam.</returns>
    Task<Exam> PublishExam(Exam exam);

    /// <summary>
    /// Deletes an exam by its ID.
    /// </summary>
    /// <param name="examId">The ID of the exam to be deleted.</param>
    void DeleteExam(Guid examId);

    /// <summary>
    /// Retrieves an exam by its ID.
    /// </summary>
    /// <param name="examId">The ID of the exam to retrieve.</param>
    /// <returns>The exam corresponding to the provided ID.</returns>
    Task<Exam> GetExamById(Guid examId);

    /// <summary>
    /// Retrieves exam data transfer objects (DTOs) by group ID.
    /// </summary>
    /// <param name="groupId">The ID of the group for which to retrieve exam DTOs.</param>
    /// <returns>A list of exam DTOs belonging to the specified group.</returns>
    Task<List<ExamDto>> GetExamDtosByGroupId(Guid groupId);

    /// <summary>
    /// Retrieves an exam data transfer object (DTO) by its ID.
    /// </summary>
    /// <param name="examId">The ID of the exam DTO to retrieve.</param>
    /// <returns>The exam DTO corresponding to the provided ID.</returns>
    Task<ExamDto> GetExamDtoById(Guid examId);

    /// <summary>
    /// Retrieves practice patient examination cards by exam ID.
    /// </summary>
    /// <param name="examId">The ID of the exam for which to retrieve examination cards.</param>
    /// <returns>A list of practice patient examination card DTOs.</returns>
    Task<List<PracticePatientExaminationCardDto>> GetPracticePatientExaminationCardsByExamId(Guid examId);

    /// <summary>
    /// Retrieves a practice patient examination card DTO by its ID.
    /// </summary>
    /// <param name="practicePatientExaminationCardId">The ID of the practice patient examination card DTO to retrieve.</param>
    /// <returns>The practice patient examination card DTO corresponding to the provided ID.</returns>
    Task<PracticePatientExaminationCardDto> GetPracticePatientExaminationCardDtoById(Guid practicePatientExaminationCardId);

    /// <summary>
    /// Retrieves a practice patient examination card by its ID.
    /// </summary>
    /// <param name="practicePatientExaminationCardId">The ID of the practice patient examination card to retrieve.</param>
    /// <returns>The practice patient examination card corresponding to the provided ID.</returns>
    Task<PracticePatientExaminationCard> GetPracticePatientExaminationCardById(Guid practicePatientExaminationCardId);

    /// <summary>
    /// Retrieves a practice risk factor assessment by practice patient examination card ID.
    /// </summary>
    /// <param name="practicePatientExaminationCardId">The ID of the practice patient examination card.</param>
    /// <returns>The practice risk factor assessment corresponding to the provided practice patient examination card ID.</returns>
    Task<PracticeRiskFactorAssessment> GetPracticeRiskFactorAssessment(Guid practicePatientExaminationCardId);

    /// <summary>
    /// Retrieves a practice API bleeding assessment by practice patient examination card ID.
    /// </summary>
    /// <param name="practicePatientExaminationCardId">The ID of the practice patient examination card.</param>
    /// <returns>The practice API bleeding assessment corresponding to the provided practice patient examination card ID.</returns>
    Task<PracticeAPIBleeding> GetPracticeAPIBleedingByCardId(Guid practicePatientExaminationCardId);

    /// <summary>
    /// Retrieves a practice DMFT/DMFS assessment by practice patient examination card ID.
    /// </summary>
    /// <param name="practicePatientExaminationCardId">The ID of the practice patient examination card.</param>
    /// <returns>The practice DMFT/DMFS assessment corresponding to the provided practice patient examination card ID.</returns>
    Task<PracticeDMFT_DMFS> GetPracticeDMFT_DMFSByCardId(Guid practicePatientExaminationCardId);

    /// <summary>
    /// Retrieves a practice BEWE assessment by practice patient examination card ID.
    /// </summary>
    /// <param name="practicePatientExaminationCardId">The ID of the practice patient examination card.</param>
    /// <returns>The practice BEWE assessment corresponding to the provided practice patient examination card ID.</returns>
    Task<PracticeBewe> GetPracticeBeweByCardId(Guid practicePatientExaminationCardId);

    /// <summary>
    /// Retrieves exam data transfer objects (DTOs) by student ID.
    /// </summary>
    /// <param name="studentId">The ID of the student for whom to retrieve exam DTOs.</param>
    /// <returns>A list of exam DTOs belonging to the specified student.</returns>
    Task<List<ExamDto>> GetExamDtosByStudentId(string studentId);

    /// <summary>
    /// Adds a practice patient and associates it with a practice patient examination card.
    /// </summary>
    /// <param name="practicePatient">The practice patient to add.</param>
    /// <param name="practicePatientExaminationCardId">The ID of the practice patient examination card to associate with the practice patient.</param>
    /// <returns>The added patient DTO.</returns>
    Task<PatientDto> AddPracticePatient(PracticePatient practicePatient, Guid practicePatientExaminationCardId);

    /// <summary>
    /// Adds a practice risk factor assessment.
    /// </summary>
    /// <param name="practiceRiskFactorAssessment">The practice risk factor assessment to add.</param>
    /// <returns>The added practice risk factor assessment.</returns>
    Task<PracticeRiskFactorAssessment> AddPracticeRiskFactorAssessment(PracticeRiskFactorAssessment practiceRiskFactorAssessment);

    /// <summary>
    /// Adds a practice DMFT/DMFS assessment.
    /// </summary>
    /// <param name="practiceDMFT_DMFS">The practice DMFT/DMFS assessment to add.</param>
    /// <returns>The added practice DMFT/DMFS assessment.</returns>
    Task<PracticeDMFT_DMFS> AddPracticeDMFT_DMFS(PracticeDMFT_DMFS practiceDMFT_DMFS);

    /// <summary>
    /// Adds a practice API bleeding assessment.
    /// </summary>
    /// <param name="practiceAPIBleeding">The practice API bleeding assessment to add.</param>
    /// <returns>The added practice API bleeding assessment.</returns>
    Task<PracticeAPIBleeding> AddPracticeAPIBleeding(PracticeAPIBleeding practiceAPIBleeding);

    /// <summary>
    /// Adds a practice BEWE assessment.
    /// </summary>
    /// <param name="practiceBewe">The practice BEWE assessment to add.</param>
    /// <returns>The added practice BEWE assessment.</returns>
    Task<PracticeBewe> AddPracticeBewe(PracticeBewe practiceBewe);

    /// <summary>
    /// Adds a practice patient examination result.
    /// </summary>
    /// <param name="practicePatientExaminationResult">The practice patient examination result to add.</param>
    /// <returns>The added practice patient examination result.</returns>
    Task<PracticePatientExaminationResult> AddPracticePatientExaminationResult(PracticePatientExaminationResult practicePatientExaminationResult);
}

