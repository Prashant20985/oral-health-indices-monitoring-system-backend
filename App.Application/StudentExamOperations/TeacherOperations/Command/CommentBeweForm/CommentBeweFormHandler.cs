using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentBeweForm;

/// <summary>
/// Handles the command to add a comment to the BEWE form of a practice examination card.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class CommentBeweFormHandler(IStudentExamRepository studentExamRepository) : IRequestHandler<CommentBeweFormCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the <see cref="CommentBeweFormCommand"/> command.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(CommentBeweFormCommand request, CancellationToken cancellationToken)
    {
        // Get the practice BEWE form by card id
        var beweForm = await _studentExamRepository.GetPracticeBeweByCardId(request.PracticeExaminationCardId);

        // Check if the practice BEWE form exists
        if (beweForm is null)
            return OperationResult<Unit>.Failure("Practice BEWE not found");

        // Add the doctor comment to the practice BEWE form
        beweForm.AddComment(request.DoctorComment);

        //Return success
        return OperationResult<Unit>.Success(Unit.Value);
    }
}

