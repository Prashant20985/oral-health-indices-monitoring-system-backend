using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.DeleteExam;

/// <summary>
/// Handler to delete an exam
/// </summary>
/// <param name="studentExamRepositor">The student exam repository</param>
internal sealed class DeleteExamHandler(IStudentExamRepository studentExamRepositor) : IRequestHandler<DeleteExamCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepositor = studentExamRepositor;

    /// <summary>
    /// Handle the delete exam command
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
    {
        // Delete the exam
        await _studentExamRepositor.DeleteExam(request.ExamId);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}

