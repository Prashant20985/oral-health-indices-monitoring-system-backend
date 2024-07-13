using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdatePatientExaminationCardSummary;

/// <summary>
/// Handler for updating patient examination card summary.
/// </summary>
/// <param name="patientExaminationCardRepository"></param>
internal sealed class UpdatePatientExaminationCardSummaryHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<UpdatePatientExaminationCardSummaryCommand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    public async Task<OperationResult<Unit>> Handle(UpdatePatientExaminationCardSummaryCommand request, CancellationToken cancellationToken)
    {
        // Get patient examination card by id.
        var card = await _patientExaminationCardRepository.GetPatientExaminationCard(request.CardId);

        // Check if patient examination card exists.
        if (card is null)
            return OperationResult<Unit>.Failure("Patient examination card not found.");

        // Update patient examination card summary.
        card.SetDescription(request.Summary.Description);
        card.SetNeedForDentalInterventions(request.Summary.NeedForDentalInterventions);
        card.SetPatientRecommendations(request.Summary.PatientRecommendations);
        card.SetProposedTreatment(request.Summary.ProposedTreatment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
