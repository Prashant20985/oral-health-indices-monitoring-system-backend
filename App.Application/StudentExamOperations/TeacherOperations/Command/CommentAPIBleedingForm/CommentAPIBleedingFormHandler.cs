using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIBleedingForm;

/// <summary>
/// Handler to add a comment to the API Bleeding Form of a practice examination card.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class CommentAPIBleedingFormHandler(IStudentExamRepository studentExamRepository)
        : IRequestHandler<CommentAPIBleedingFormCommand, OperationResult<Unit>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the command to add a comment to the API Bleeding Form of a practice examination card.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<Unit>> Handle(CommentAPIBleedingFormCommand request, CancellationToken cancellationToken)
    {
        // Get the practice API Bleeding form by card id
        var apiBleedingForm = await _studentExamRepository.GetPracticeAPIBleedingByCardId(request.PracticeExaminationCardId);

        // Check if the practice API Bleeding form exists
        if (apiBleedingForm is null)
            return OperationResult<Unit>.Failure("Practice API Bleeding form not found");

        // Add the doctor comment to the practice API Bleeding form
        apiBleedingForm.AddComment(request.DoctorComment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
