using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardInTestMode;

/// <summary>
/// Handler for fetching patient examination cards in test mode
/// </summary>
/// <param name="patientExaminationCardRepository">Repository for patient examination cards</param>
/// <param name="patientRepository">Repository for patients</param>
internal sealed class FetchPatientExaminationCardsInTestModeHandler(
    IPatientExaminationCardRepository patientExaminationCardRepository,
    IPatientRepository patientRepository)
    : IRequestHandler<FetchPatientExaminationCardsInTestModeQuery, OperationResult<List<PatientExaminationCardDto>>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles fetching patient examination cards in test mode
    /// </summary>
    /// <param name="request">Request containing patient id</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>Operation result containing list of patient examination cards</returns>
    public async Task<OperationResult<List<PatientExaminationCardDto>>> Handle(FetchPatientExaminationCardsInTestModeQuery request, CancellationToken cancellationToken)
    {
        // Check if patient exists
        var checkPatientExists = await _patientRepository.GetPatientById(request.PatientId);

        // If patient does not exist, return failure
        if (checkPatientExists is null)
            return OperationResult<List<PatientExaminationCardDto>>.Failure("Patient Not Found");

        // Fetch patient examination cards in test mode
        var patientExaminationCards = await _patientExaminationCardRepository
            .GetPatientExminationCardDtosInTestModeByPatientId(request.PatientId);

        return OperationResult<List<PatientExaminationCardDto>>.Success(patientExaminationCards);
    }
}
