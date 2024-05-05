using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentDMFT_DMFSForm;

/// <summary>
/// Handles the command to add a comment to the DMFT/DMFS form of a practice examination card.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class CommentDMFT_DMFSFormHandler(IStudentExamRepository studentExamRepository)
        : IRequestHandler<CommentDMFT_DMFSFormCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the command to add a comment to the DMFT/DMFS form of a practice examination card.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(CommentDMFT_DMFSFormCommand request, CancellationToken cancellationToken)
    {
        // Get the practice DMFT/DMFS form by card id
        var dmftDmfsForm = await _studentExamRepository.GetPracticeDMFT_DMFSByCardId(request.PracticeExaminationCardId);

        // Check if the practice DMFT/DMFS form exists
        if (dmftDmfsForm is null)
            return OperationResult<Unit>.Failure("Practice DMFT/DMFS not found");

        // Add the doctor comment to the practice DMFT/DMFS form
        dmftDmfsForm.AddComment(request.DoctorComment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
