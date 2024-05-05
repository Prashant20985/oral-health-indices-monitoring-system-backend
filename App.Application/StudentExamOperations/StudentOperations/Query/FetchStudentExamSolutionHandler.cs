using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Query;

/// <summary>
/// Fetches a practice patient examination card by student ID and exam ID.
/// </summary>
/// <param name="studentExamRepository"></param>
internal sealed class FetchStudentExamSolutionHandler(IStudentExamRepository studentExamRepository)
        : IRequestHandler<FetchStudentExamSolutionQuery, OperationResult<PracticePatientExaminationCardDto>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the request to fetch a practice patient examination card by student ID and exam ID.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<OperationResult<PracticePatientExaminationCardDto>> Handle(FetchStudentExamSolutionQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the practice patient examination card by exam ID and student ID.
        var practicePatientExaminationCard = await _studentExamRepository
            .GetPracticePatientExaminationCardByExamIdAndStudentId(request.ExamId, request.StudentId);

        // If the practice patient examination card is not found, return an error message.
        if (practicePatientExaminationCard is null)
            return OperationResult<PracticePatientExaminationCardDto>.Failure("Examination card not found");

        // Return the practice patient examination card.
        return OperationResult<PracticePatientExaminationCardDto>.Success(practicePatientExaminationCard);
    }
}
