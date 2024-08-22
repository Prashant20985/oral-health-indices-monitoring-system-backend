using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateRiskFactorAssessmentForm;

/// <summary>
/// Command to update risk factor assessment form
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
internal sealed class UpdateRiskFactorAssessmentFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<UpdateRiskFactorAssessmentFormCommand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the update risk factor assessment form command
    /// </summary>
    /// <param name="request">The update risk factor assessment form command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The operation result</returns>
    public async Task<OperationResult<Unit>> Handle(UpdateRiskFactorAssessmentFormCommand request, CancellationToken cancellationToken)
    {
        // Get the risk factor assessment form by card id
        var riskFactorAssessmentForm = await _patientExaminationCardRepository.GetRiskFactorAssessmentByCardId(request.CardId);

        // Check if the risk factor assessment form is null, if so return failure
        if (riskFactorAssessmentForm is null)
            return OperationResult<Unit>.Failure("Risk factor assessment form not found");

        // Set the risk factor assessment model
        riskFactorAssessmentForm.SetRiskFactorAssessmentModel(request.AssessmentModel);

        // Return success
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
