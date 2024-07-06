using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentAPIForm;

/// <summary>
/// Hnadler for adding comment to the patient examination card
/// </summary>
/// <param name="patientExaminationCardRepository">Repository for patient examination card</param>
internal sealed class CommentAPIFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<CommentAPIFormCommnand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the command for adding comment to the patient examination card
    /// </summary>
    /// <param name="request">Command for adding comment to the patient examination card</param>
    /// <param name="cancellationToken">The Cancellation Token</param>
    /// <returns>Result of the operation</returns>
    public async Task<OperationResult<Unit>> Handle(CommentAPIFormCommnand request, CancellationToken cancellationToken)
    {
        var apiForm = await _patientExaminationCardRepository.GetAPIByCardId(request.CardId);

        if (apiForm is null)
            return OperationResult<Unit>.Failure("API Form not found");

        apiForm.AddDoctorComment(request.DoctorComment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
