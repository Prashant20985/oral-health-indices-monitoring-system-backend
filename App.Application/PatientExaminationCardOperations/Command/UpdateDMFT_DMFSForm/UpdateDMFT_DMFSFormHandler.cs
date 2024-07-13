using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;

/// <summary>
/// Handles the Update DMFT/DMFS form command
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
internal sealed class UpdateDMFT_DMFSFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<UpdateDMFT_DMFSFormCommand, OperationResult<DMFT_DMFSResultResponseDto>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the Update DMFT/DMFS form command
    /// </summary>
    /// <param name="request">The request to update DMFT/DMFS form</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the operation</returns>
    public async Task<OperationResult<DMFT_DMFSResultResponseDto>> Handle(UpdateDMFT_DMFSFormCommand request, CancellationToken cancellationToken)
    {
        // Get the DMFT/DMFS form by card id
        var dmft_dmfsForm = await _patientExaminationCardRepository.GetDMFT_DMFSByCardId(request.CardId);

        // If the DMFT/DMFS form is not found, return an error
        if (dmft_dmfsForm is null)
            return OperationResult<DMFT_DMFSResultResponseDto>.Failure("DMFT/DMFS form not found");

        // Update the DMFT/DMFS form AssessmentModel
        dmft_dmfsForm.SetDMFT_DMFSAssessmentModel(request.UpdateDMFT_DMFS.AssessmentModel);

        // Add the DMFT/DMFS form to the repository
        dmft_dmfsForm.SetDMFSResult(request.UpdateDMFT_DMFS.DMFSResult);
        dmft_dmfsForm.SetDMFTResult(request.UpdateDMFT_DMFS.DMFTResult);

        // Update the DMFT/DMFS form ProstheticStatus
        dmft_dmfsForm.SetProstheticStatus(request.UpdateDMFT_DMFS.ProstheticStatus);

        return OperationResult<DMFT_DMFSResultResponseDto>.Success(new DMFT_DMFSResultResponseDto
        {
            DMFSResult = dmft_dmfsForm.DMFSResult,
            DMFTResult = dmft_dmfsForm.DMFTResult,
        });
    }
}
