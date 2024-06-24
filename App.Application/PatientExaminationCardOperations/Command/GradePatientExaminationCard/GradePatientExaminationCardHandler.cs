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
        var card = await _patientExaminationCardRepository.GetPatientExaminationCard(request.CardId);

        if (card is null)
            return OperationResult<Unit>.Failure("Patient examination card not found");

        card.SetTotalScore(request.TotalScore);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
