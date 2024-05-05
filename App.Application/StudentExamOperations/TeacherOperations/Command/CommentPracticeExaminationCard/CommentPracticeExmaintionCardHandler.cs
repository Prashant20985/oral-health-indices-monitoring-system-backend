using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentPracticeExaminationCard;

/// <summary>
/// Handler to add a comment to a practice examination card.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class CommentPracticeExmaintionCardHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<CommentPracticeExaminationCardCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the command to add a comment to a practice examination card.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(CommentPracticeExaminationCardCommand request, CancellationToken cancellationToken)
    {
        // Get the practice patient examination card by id
        var practicePatientExaminationCard = await _studentExamRepository.GetPracticePatientExaminationCardById(request.PracticeExaminationCardId);

        // Check if the practice patient examination card exists
        if (practicePatientExaminationCard is null)
            return OperationResult<Unit>.Failure("PracticePatientExaminationCard not found");

        // Set the doctor comment to the practice patient examination card
        practicePatientExaminationCard.SetDoctorComment(request.DoctorComment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
