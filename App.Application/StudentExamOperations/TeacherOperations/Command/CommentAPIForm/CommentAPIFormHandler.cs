using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIForm;

/// <summary>
/// Hadles the command for adding a comment to the API form of a practice examination card.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class CommentAPIFormHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<CommentAPIFormCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the <see cref="CommentAPIFormCommand"/> command.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(CommentAPIFormCommand request, CancellationToken cancellationToken)
    {
        // Get the practice API form by card id
        var apiForm = await _studentExamRepository.GetPracticeAPIByCardId(request.PracticeExaminationCardId);

        // Check if the practice API form exists
        if (apiForm is null)
            return OperationResult<Unit>.Failure("Practice API not found");

        // Add the doctor comment to the practice API form
        apiForm.AddComment(request.DoctorComment);

        //Return success
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
