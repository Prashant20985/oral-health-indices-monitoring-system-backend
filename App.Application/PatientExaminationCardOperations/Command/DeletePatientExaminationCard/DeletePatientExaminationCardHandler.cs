using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.DeletePatientExaminationCard;

/// <summary>
/// Handles the Delete Patient Examination Card Command
/// </summary>
/// <param name="patientExaminationCardRepository">The repository for the patient examination card</param>
internal sealed class DeletePatientExaminationCardHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<DeletePatientExaminationCardCommand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the Delete Patient Examination Card Command
    /// </summary>
    /// <param name="request">The Delete Patient Examination Card Command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the operation</returns>
    public async Task<OperationResult<Unit>> Handle(DeletePatientExaminationCardCommand request, CancellationToken cancellationToken)
    {
        var patientExamiantionCard = await _patientExaminationCardRepository.GetPatientExaminationCard(request.CardId);

        if (patientExamiantionCard is null)
            return OperationResult<Unit>.Failure("Examination Card not forund");

        await _patientExaminationCardRepository.DeletePatientExaminationCard(request.CardId);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
