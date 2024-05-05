using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.UpdateExam;

/// <summary>
/// Handles the command to update an exam.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class UpdateExamHandler(IStudentExamRepository studentExamRepository)
        : IRequestHandler<UpdateExamCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the command to update an exam.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
    {
        // Get the exam by id
        var exam = await _studentExamRepository.GetExamById(request.ExamId);

        // Check if the exam exists
        if (exam is null)
            return OperationResult<Unit>.Failure("Exam not found");

        // Update the exam
        exam.UpdateExam(request.UpdateExam);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
