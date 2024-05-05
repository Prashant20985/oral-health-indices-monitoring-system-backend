using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.GradingPracticeExaminationCard;

/// <summary>
/// Handles the command to grade a practice examination card.
/// </summary>
/// <param name="studentExamRepository">The student exam repository</param>
internal sealed class GradePracticeExaminationCardHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<GradePracticeExaminationCardCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the command to grade a practice examination card.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(GradePracticeExaminationCardCommand request, CancellationToken cancellationToken)
    {
        // Get the practice examination card by id
        var practicePatientExaminationCard = await _studentExamRepository
            .GetPracticePatientExaminationCardById(request.PracticeExaminationCardId);

        // Check if the practice examination card exists
        if (practicePatientExaminationCard is null)
            return OperationResult<Unit>.Failure("Examination Card not found");

        // Get the exam by id
        var exam = await _studentExamRepository
            .GetExamById(practicePatientExaminationCard.ExamId);

        // Check if the exam exists
        if (exam is null)
            return OperationResult<Unit>.Failure("Exam not found");

        // Check if the student mark is valid
        if (request.StudentMark >= 0 && request.StudentMark <= exam.MaxMark)
        {
            // Set the student mark to the practice examination card
            practicePatientExaminationCard.SetStudentMark(request.StudentMark);

            // Return success
            return OperationResult<Unit>.Success(Unit.Value);
        }

        // Return an error if the student mark is not valid
        return OperationResult<Unit>.Failure("Student mark is not valid");
    }
}
