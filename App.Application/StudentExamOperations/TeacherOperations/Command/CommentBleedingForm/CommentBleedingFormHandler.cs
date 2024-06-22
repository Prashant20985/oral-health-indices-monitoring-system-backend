using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentBleedingForm;

/// <summary>
/// Handles the command for adding a comment to the Bleeding form of a practice examination card.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal class CommentBleedingFormHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<CommentBleedingFormCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the <see cref="CommentBleedingFormCommand"/> command.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(CommentBleedingFormCommand request, CancellationToken cancellationToken)
    {
        // Get the practice Bleeding form by card id
        var bleedingForm = await _studentExamRepository.GetPracticeBleedingByCardId(request.PracticeExaminationCardId);

        // Check if the practice Bleeding form exists
        if (bleedingForm is null)
            return OperationResult<Unit>.Failure("Practice Bleeding not found");

        // Add the doctor comment to the practice Bleeding form
        bleedingForm.AddComment(request.DoctorComment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
