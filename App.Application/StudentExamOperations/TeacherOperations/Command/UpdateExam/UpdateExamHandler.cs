using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.UpdateExam;

/// <summary>
/// Handles the command to update an exam.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class UpdateExamHandler(IStudentExamRepository studentExamRepository)
        : IRequestHandler<UpdateExamCommand, OperationResult<ExamDto>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the command to update an exam.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with Unit as result.</returns>
    public async Task<OperationResult<ExamDto>> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
    {
        // Get the exam by id
        var exam = await _studentExamRepository.GetExamById(request.ExamId);

        // Check if the exam exists
        if (exam is null)
            return OperationResult<ExamDto>.Failure("Exam not found");

        // Update the exam
        exam.UpdateExam(request.UpdateExam);

        var updatedExam = new ExamDto
        {
            Id = exam.Id,
            DateOfExamination = exam.DateOfExamination,
            ExamTitle = exam.ExamTitle,
            Description = exam.Description,
            PublishDate = exam.PublishDate,
            EndTime = exam.EndTime,
            StartTime = exam.StartTime,
            DurationInterval = exam.DurationInterval,
            MaxMark = exam.MaxMark,
            ExamStatus = exam.ExamStatus.ToString(),
        };

        return OperationResult<ExamDto>.Success(updatedExam);
    }
}
