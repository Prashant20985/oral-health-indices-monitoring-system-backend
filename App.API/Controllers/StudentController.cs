using App.Application.StudentExamOperations.StudentOperations.Query.ExamEligbility;
using App.Application.StudentOperations.Query.StudentGroupDetails;
using App.Application.StudentOperations.Query.StudentGroupsList;
using App.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.API.Controllers;

[Authorize(Roles = "Student")]
public class StudentController : BaseController
{
    /// <summary>
    /// Fetches the list of student groups with exams.
    /// </summary>
    /// <returns>List of student groups with exams.</returns>
    [HttpGet("student-groups")]
    public async Task<ActionResult<List<GroupWithExamsListDto>>> GetStudentGroupsWithExams() =>
        HandleOperationResult(await Mediator.Send(new FetchStudentGroupsWithExamsListQuery(User.FindFirstValue(ClaimTypes.NameIdentifier))));

    /// <summary>
    /// Fetches the details of a student group with exams.
    /// </summary>
    /// <returns>Details of a student group with exams.</returns>
    [HttpGet("student-group-details/{groupId}")]
    public async Task<ActionResult<GroupWithExamsListDto>> GetStudentGroupDetails(Guid groupId) =>
        HandleOperationResult(await Mediator.Send(
            new FetchStudentGroupDetailsWithExamsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier), groupId)));

    /// <summary>
    /// Gets the eligibility of the student to take the exam.
    /// </summary>
    /// <returns>True if student is eligible to take the exam, false otherwise.</returns>
    [HttpGet("exam-eligibility/{examId}")]
    public async Task<ActionResult<bool>> CheckExamEligibility(Guid examId) =>
        HandleOperationResult(await Mediator.Send(new ExamEligibiltyQuery(examId, User.FindFirstValue(ClaimTypes.NameIdentifier))));
}
