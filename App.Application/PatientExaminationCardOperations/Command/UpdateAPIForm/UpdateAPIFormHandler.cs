using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateAPIForm;

/// <summary>
/// Handles the update of API form
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
internal sealed class UpdateAPIFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<UpdateAPIFormCommand, OperationResult<APIResultResponseDto>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the update of API form
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the operation</returns>
    public async Task<OperationResult<APIResultResponseDto>> Handle(UpdateAPIFormCommand request, CancellationToken cancellationToken)
    {
        // Get the API form by card id
        var apiForm = await _patientExaminationCardRepository.GetAPIByCardId(request.CardId);

        // If the API form is not found, return failure
        if (apiForm is null)
            return OperationResult<APIResultResponseDto>.Failure("API form not found");

        // Set the assessment model
        apiForm.SetAssessmentModel(request.AssessmentModel);

        // Calculate the API result
        apiForm.CalculateAPIResult();

        return OperationResult<APIResultResponseDto>.Success(new APIResultResponseDto
        {
            APIResult = apiForm.APIResult,
            Maxilla = apiForm.Maxilla,
            Mandible = apiForm.Mandible,
        });
    }
}
