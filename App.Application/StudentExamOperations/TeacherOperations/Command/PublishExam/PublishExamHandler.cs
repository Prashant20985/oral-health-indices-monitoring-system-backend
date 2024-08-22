using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.PublishExam;

/// <summary>
/// Handles the command to publish an exam.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class PublishExamHandler(IStudentExamRepository studentExamRepository) : IRequestHandler<PublishExamCommand, OperationResult<ExamDto>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the command to publish an exam.
    /// </summary>
    /// <param name="request">The command request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>An operation result with the published exam.</returns>
    public async Task<OperationResult<ExamDto>> Handle(PublishExamCommand request, CancellationToken cancellationToken)
    {
        // Create the exam object
        Exam exam = new(request.PublishExam.DateOfExamination,
                        request.PublishExam.ExamTitle,
                        request.PublishExam.Description,
                        request.PublishExam.StartTime,
                        request.PublishExam.EndTime,
                        request.PublishExam.DurationInterval,
                        request.PublishExam.MaxMark,
                        request.PublishExam.GroupId);

        // Publish the exam
        var result = await _studentExamRepository.PublishExam(exam);

        // Return the published exam
        return OperationResult<ExamDto>.Success(result);
    }
}
