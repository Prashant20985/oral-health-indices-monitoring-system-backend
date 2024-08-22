using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardDetails;

/// <summary>
/// Handles query to fetch patient examination card details
/// </summary>
/// <param name="patientExaminationCardRepository">Repository to fetch patient examination card details</param>
internal sealed class FetchPatientExaminationCardDetailsHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<FetchPatientExaminationCardDetailsQuery, OperationResult<PatientExaminationCardDto>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles query to fetch patient examination card details
    /// </summary>
    /// <param name="request">Query to fetch patient examination card details</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled</param>
    /// <returns>Operation result with patient examination card details</returns>
    public async Task<OperationResult<PatientExaminationCardDto>> Handle(FetchPatientExaminationCardDetailsQuery request, CancellationToken cancellationToken)
    {
        // Fetch the patient examination card details
        var patientExaminationCard = await _patientExaminationCardRepository
            .GetPatientExaminationCardDto(request.CardId);

        // Check if the patient examination card exists, if not return failure
        if (patientExaminationCard is null)
            return OperationResult<PatientExaminationCardDto>.Failure("Examination Card Not Found");

        //Return success
        return OperationResult<PatientExaminationCardDto>.Success(patientExaminationCard);
    }
}
