using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardsList;

/// <summary>
/// Handler to fetch the practice patient examination cards by exam id.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class FetchPracticePatientExaminationCardsByExamIdHandler(IStudentExamRepository studentExamRepository)
        : IRequestHandler<FetchPracticePatientExaminationCardsByExamIdQuery, OperationResult<List<PracticePatientExaminationCardDto>>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the query to fetch the practice patient examination cards by exam id.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result with a list of practice patient examination cards.</returns>
    public async Task<OperationResult<List<PracticePatientExaminationCardDto>>> Handle(FetchPracticePatientExaminationCardsByExamIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get the practice patient examination cards by exam id
        var examinationCards = await _studentExamRepository.GetPracticePatientExaminationCardsByExamId(request.ExamId);

        return OperationResult<List<PracticePatientExaminationCardDto>>.Success(examinationCards);
    }
}
