using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.GradePatientExaminationCard;

/// <summary>
/// Handles the command to grade a patient examination card.
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository.</param>
internal sealed class GradePatientExaminationCardHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<GradePatientExaminationCardCommand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the command to grade a patient examination card.
    /// </summary>
    /// <param name="request">The command to grade a patient examination card.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(GradePatientExaminationCardCommand request, CancellationToken cancellationToken)
    {
        // Get the patient examination card
        var card = await _patientExaminationCardRepository.GetPatientExaminationCard(request.CardId);

        // If the patient examination card does not exist, return failure
        if (card is null)
            return OperationResult<Unit>.Failure("Patient examination card not found");

        // Set the total score of the patient examination card
        card.SetTotalScore(request.TotalScore);

        //Return success
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
