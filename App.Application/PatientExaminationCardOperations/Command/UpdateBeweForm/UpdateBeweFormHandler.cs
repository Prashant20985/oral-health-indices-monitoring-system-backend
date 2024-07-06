using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateBeweForm;

/// <summary>
/// Handles the Update Bewe form command
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
internal sealed class UpdateBeweFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<UpdateBeweFormCommand, OperationResult<decimal>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the Update Bewe form command
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the operation</returns>
    public async Task<OperationResult<decimal>> Handle(UpdateBeweFormCommand request, CancellationToken cancellationToken)
    {
        // Get the BEWE form by card id
        var beweForm = await _patientExaminationCardRepository.GetBeweByCardId(request.CardId);

        // If the BEWE form is not found, return an error
        if (beweForm is null)
            return OperationResult<decimal>.Failure("Bewe form not found");

        // Set the assessment model
        beweForm.SetAssessmentModel(request.AssessmentModel);

        // Calculate the BEWE result
        beweForm.CalculateBeweResult();

        return OperationResult<decimal>.Success(beweForm.BeweResult);
    }
}
