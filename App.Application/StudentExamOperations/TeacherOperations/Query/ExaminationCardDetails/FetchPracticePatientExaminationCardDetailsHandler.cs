using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardDetails;

/// <summary>
/// Handler for fetching the details of a practice patient examination card
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class FetchPracticePatientExaminationCardDetailsHandler(IStudentExamRepository studentExamRepository)
        : IRequestHandler<FetchPracticePatientExaminationCardDetailsQuery, OperationResult<PracticePatientExaminationCardDto>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the query to fetch the details of a practice patient examination card
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns the operation result of the practice patient examination card details</returns>
    public async Task<OperationResult<PracticePatientExaminationCardDto>> Handle(FetchPracticePatientExaminationCardDetailsQuery request,
        CancellationToken cancellationToken)
    {
        // Get the practice patient examination card by id
        var examinationCardDetails = await _studentExamRepository
            .GetPracticePatientExaminationCardDtoById(request.PracticePatientExaminationCardId);

        // Check if the practice patient examination card exists
        if (examinationCardDetails == null)
            return OperationResult<PracticePatientExaminationCardDto>.Failure("Examination Card not found");

        // Return the practice patient examination card details
        return OperationResult<PracticePatientExaminationCardDto>.Success(examinationCardDetails);
    }
}
