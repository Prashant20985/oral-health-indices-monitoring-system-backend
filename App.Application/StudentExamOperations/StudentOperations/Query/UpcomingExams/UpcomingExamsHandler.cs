using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Query.UpcomingExams;
/// <summary>
/// Initializes a new instance of the <see cref="UpcomingExamsHandler"/> class.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class UpcomingExamsHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<UpcominExamsQuery, OperationResult<List<ExamDto>>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the upcoming exams query.
    /// </summary>
    /// <param name="request">The upcoming exams query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An OperationResult containing a list of ExamDto.</returns>
    public async Task<OperationResult<List<ExamDto>>> Handle(UpcominExamsQuery request,
        CancellationToken cancellationToken) => OperationResult<List<ExamDto>>.Success(
                await _studentExamRepository.UpcomingExams(request.StudentId));
}
