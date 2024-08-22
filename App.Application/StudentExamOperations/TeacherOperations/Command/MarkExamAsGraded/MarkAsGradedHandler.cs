using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.MarkExamAsGraded;

/// <summary>
/// Handles the command to mark an exam as graded.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class MarkAsGradedHandler(IStudentExamRepository studentExamRepository)
        : IRequestHandler<MarkAsGradedCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the command to mark an exam as graded.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(MarkAsGradedCommand request, CancellationToken cancellationToken)
    {
        // Get the exam by id
        var exam = await _studentExamRepository.GetExamById(request.ExamId);

        // Check if the exam exists
        if (exam is null)
            return OperationResult<Unit>.Failure("Exam not found");

        // Mark the exam as graded
        exam.MarksAsGraded();

        // Return success
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
