using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateBleedingForm;

/// <summary>
/// Handles the Update Bleeding Form Command
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
internal sealed class UpdateBleedingFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<UpdateBleedingFormCommand, OperationResult<BleedingResultResponseDto>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the Update Bleeding Form Command
    /// </summary>
    /// <param name="request">The Update Bleeding Form Command</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the operation</returns>
    public async Task<OperationResult<BleedingResultResponseDto>> Handle(UpdateBleedingFormCommand request, CancellationToken cancellationToken)
    {
        // Get the bleeding form by card id
        var bleedingForm = await _patientExaminationCardRepository.GetBleedingByCardId(request.CardId);

        // If the bleeding form is not found, return an error
        if (bleedingForm is null)
            return OperationResult<BleedingResultResponseDto>.Failure("Bleeding form not found.");

        // Set the assessment model
        bleedingForm.SetAssessmentModel(request.AssessmentModel);

        // Calculate the bleeding result
        bleedingForm.CalculateBleedingResult();

        return OperationResult<BleedingResultResponseDto>.Success(new BleedingResultResponseDto
        {
            BleedingResult = bleedingForm.BleedingResult,
            Maxilla = bleedingForm.Maxilla,
            Mandible = bleedingForm.Mandible
        });
    }
}
