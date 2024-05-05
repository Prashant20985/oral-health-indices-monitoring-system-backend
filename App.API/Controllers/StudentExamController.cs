using App.Application.StudentExamOperations.CommonOperations.Query.ExamDetails;
using App.Application.StudentExamOperations.CommonOperations.Query.ExamsList;
using App.Application.StudentExamOperations.StudentOperations.Command.AddPracticePatientExmaintionCard;
using App.Application.StudentExamOperations.StudentOperations.Query;
using App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIBleedingForm;
using App.Application.StudentExamOperations.TeacherOperations.Command.CommentBeweForm;
using App.Application.StudentExamOperations.TeacherOperations.Command.CommentDMFT_DMFSForm;
using App.Application.StudentExamOperations.TeacherOperations.Command.CommentPracticeExaminationCard;
using App.Application.StudentExamOperations.TeacherOperations.Command.DeleteExam;
using App.Application.StudentExamOperations.TeacherOperations.Command.GradingPracticeExaminationCard;
using App.Application.StudentExamOperations.TeacherOperations.Command.MarkExamAsGraded;
using App.Application.StudentExamOperations.TeacherOperations.Command.PublishExam;
using App.Application.StudentExamOperations.TeacherOperations.Command.UpdateExam;
using App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardDetails;
using App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardsList;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.DTOs.ExamDtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers;

/// <summary>
/// Controller for handling student exam operations.
/// </summary>
public class StudentExamController : BaseController
{
    /// <summary>
    /// Publishes an exam.
    /// </summary>
    /// <param name="publishExam">Details of the exam to be published.</param>
    /// <returns>An ActionResult of type ExamDto.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpPost("publish-exam")]
    public async Task<ActionResult<ExamDto>> PublishExam(PublishExamDto publishExam) =>
        HandleOperationResult(await Mediator.Send(new PublishExamCommand(publishExam)));

    /// <summary>
    /// Deletes an exam.
    /// </summary>
    /// <param name="examId">The ID of the exam to be deleted.</param>
    /// <returns>An ActionResult.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpDelete("exam/{examId}")]
    public async Task<ActionResult> DeleteExam(Guid examId) =>
        HandleOperationResult(await Mediator.Send(new DeleteExamCommand(examId)));

    /// <summary>
    /// Updates an exam.
    /// </summary>
    /// <param name="examId">The ID of the exam to be updated.</param>
    /// <param name="updateExam">Updated details of the exam.</param>
    /// <returns>An ActionResult of type ExamDto.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpPut("update-exam/{examId}")]
    public async Task<ActionResult<ExamDto>> UpdateExam(Guid examId, UpdateExamDto updateExam) =>
        HandleOperationResult(await Mediator.Send(new UpdateExamCommand(examId, updateExam)));

    /// <summary>
    /// Adds a comment to a practice patient examination card.
    /// </summary>
    /// <param name="cardId">The ID of the examination card.</param>
    /// <param name="comment">The comment to be added.</param>
    /// <returns>An ActionResult.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpPut("comment-card/{cardId}")]
    public async Task<ActionResult> CommentPracticePatientExaminationCard(Guid cardId, string comment) =>
        HandleOperationResult(await Mediator.Send(new CommentPracticeExaminationCardCommand(cardId, comment)));

    /// <summary>
    /// Adds a comment to an API Bleeding form.
    /// </summary>
    /// <param name="cardId">The ID of the examination card.</param>
    /// <param name="comment">The comment to be added.</param>
    /// <returns>An ActionResult.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpPut("comment-apiBleedingForm/{cardId}")]
    public async Task<ActionResult> CommentPracticeAPIBleedingForm(Guid cardId, string comment) =>
        HandleOperationResult(await Mediator.Send(new CommentAPIBleedingFormCommand(cardId, comment)));

    /// <summary>
    /// Adds a comment to a DMFT/DMFS form.
    /// </summary>
    /// <param name="cardId">The ID of the examination card.</param>
    /// <param name="comment">The comment to be added.</param>
    /// <returns>An ActionResult.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpPut("comment-dmft_dmfsForm/{cardId}")]
    public async Task<ActionResult> CommentDMFT_DMFSForm(Guid cardId, string comment) =>
        HandleOperationResult(await Mediator.Send(new CommentDMFT_DMFSFormCommand(cardId, comment)));

    /// <summary>
    /// Adds a comment to a BEWE form.
    /// </summary>
    /// <param name="cardId">The ID of the examination card.</param>
    /// <param name="comment">The comment to be added.</param>
    /// <returns>An ActionResult.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpPut("comment-beweForm/{cardId}")]
    public async Task<ActionResult> CommentPracticeBeweForm(Guid cardId, string comment) =>
        HandleOperationResult(await Mediator.Send(new CommentBeweFormCommand(cardId, comment)));

    /// <summary>
    /// Marks an exam as graded.
    /// </summary>
    /// <param name="examId">The ID of the exam to be marked.</param>
    /// <returns>An ActionResult.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpPut("markAsGraded/{examId}")]
    public async Task<ActionResult> MarkAsGraded(Guid examId) =>
        HandleOperationResult(await Mediator.Send(new MarkAsGradedCommand(examId)));

    /// <summary>
    /// Grades an examination card.
    /// </summary>
    /// <param name="cardId">The ID of the examination card to be graded.</param>
    /// <param name="studentMark">The mark assigned to the examination card by the student.</param>
    /// <returns>An ActionResult.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpPut("gradeExaminationCard/{cardId}")]
    public async Task<ActionResult> GradeExaminationCard(Guid cardId, int studentMark) =>
        HandleOperationResult(await Mediator.Send(new GradePracticeExaminationCardCommand(cardId, studentMark)));

    /// <summary>
    /// Retrieves a list of exams by group ID.
    /// </summary>
    /// <param name="groupId">The ID of the group.</param>
    /// <returns>An ActionResult of a list of ExamDto.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner, Student")]
    [HttpGet("exams/{groupId}")]
    public async Task<ActionResult<List<ExamDto>>> GetExamsList(Guid groupId) =>
        HandleOperationResult(await Mediator.Send(new FetchExamsListByGroupIdQuery(groupId)));

    /// <summary>
    /// Retrieves details of an exam by exam ID.
    /// </summary>
    /// <param name="examId">The ID of the exam.</param>
    /// <returns>An ActionResult of type ExamDto.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner, Student")]
    [HttpGet("exam-details/{examId}")]
    public async Task<ActionResult<ExamDto>> GetExamDetails(Guid examId) =>
        HandleOperationResult(await Mediator.Send(new FetchExamDetailsQuery(examId)));

    /// <summary>
    /// Retrieves examination cards by exam ID.
    /// </summary>
    /// <param name="examId">The ID of the exam.</param>
    /// <returns>An ActionResult of a list of PracticePatientExaminationCardDto.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpGet("exam-cards/{examId}")]
    public async Task<ActionResult<List<PracticePatientExaminationCardDto>>> GetExaminationCards(Guid examId) =>
        HandleOperationResult(await Mediator.Send(new FetchPracticePatientExaminationCardsByExamIdQuery(examId)));

    /// <summary>
    /// Retrieves details of an examination card by card ID.
    /// </summary>
    /// <param name="cardId">The ID of the examination card.</param>
    /// <returns>An ActionResult of type PracticePatientExaminationCardDto.</returns>
    [Authorize(Roles = "Dentist_Teacher_Examiner")]
    [HttpGet("exam-card-details/{cardId}")]
    public async Task<ActionResult<PracticePatientExaminationCardDto>> GetExaminationCardDetails(Guid cardId) =>
        HandleOperationResult(await Mediator.Send(new FetchPracticePatientExaminationCardDetailsQuery(cardId)));

    /// <summary>
    /// Adds a practice patient examination card for a specific exam.
    /// </summary>
    /// <param name="examId">The ID of the exam.</param>
    /// <param name="cardInputModel">The input model for the practice patient examination card.</param>
    /// <returns>An ActionResult.</returns>
    [Authorize(Roles = "Student")]
    [HttpPost("exam-solution/{examId}")]
    public async Task<ActionResult> AddPracticePatientExaminationCard(
        Guid examId,
        [FromBody] PracticePatientExaminationCardInputModel cardInputModel) =>
        HandleOperationResult(await Mediator.Send(new AddPracticePatientExaminationCardCommand(
            examId,
            User.FindFirstValue(ClaimTypes.NameIdentifier),
            cardInputModel)));

    /// <summary>
    /// Retrieves the solution for a student exam by exam ID.
    /// </summary>
    /// <param name="examId">The ID of the exam.</param>
    /// <returns>An ActionResult of type PracticePatientExaminationCardDto.</returns>
    [Authorize(Roles = "Student")]
    [HttpGet("exam-solution/{examId}")]
    public async Task<ActionResult<PracticePatientExaminationCardDto>> GetStudentExamSolution(Guid examId) =>
        HandleOperationResult(await Mediator.Send(new FetchStudentExamSolutionQuery(examId, User.FindFirstValue(ClaimTypes.NameIdentifier))));
}

