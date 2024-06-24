using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;

/// <summary>
/// Handles the Update DMFT/DMFS form command
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
internal sealed class UpdateDMFT_DMFSFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<UpdateDMFT_DMFSFormCommand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the Update DMFT/DMFS form command
    /// </summary>
    /// <param name="request">The request to update DMFT/DMFS form</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the operation</returns>
    public async Task<OperationResult<Unit>> Handle(UpdateDMFT_DMFSFormCommand request, CancellationToken cancellationToken)
    {
        // Get the DMFT/DMFS form by card id
        var dmft_dmfsForm = await _patientExaminationCardRepository.GetDMFT_DMFSByCardId(request.CardId);

        // If the DMFT/DMFS form is not found, return an error
        if (dmft_dmfsForm is null)
            return OperationResult<Unit>.Failure("DMFT/DMFS form not found");

        // Update the DMFT/DMFS form AssessmentModel
        dmft_dmfsForm.SetDMFT_DMFSAssessmentModel(request.AssessmentModel);

        // Calculate the DMFT/DMFS form result
        dmft_dmfsForm.CalculateDMFTResult();
        dmft_dmfsForm.CalculateDMFSResult();

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
