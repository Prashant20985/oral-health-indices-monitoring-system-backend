using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Query.FetchAllPatientExaminationCardsInRegualrMode;

/// <summary>
/// Handles query to fetch all patient examination cards in regular mode
/// </summary>
/// <param name="patientExaminationCardRepository">Repository to handle patient examination card operations</param>
/// <param name="patientRepository">Repository to handle patient operations</param>
internal sealed class FetchPatientExamiantionCardsInRegularModeHandler(
    IPatientExaminationCardRepository patientExaminationCardRepository,
    IPatientRepository patientRepository)
    : IRequestHandler<FetchPatientExaminationCardsInRegularModeQuery, OperationResult<List<PatientExaminationCardDto>>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles query to fetch all patient examination cards in regular mode
    /// </summary>
    /// <param name="request">Query to fetch all patient examination cards in regular mode</param>
    /// <param name="cancellationToken">The CancellationToken</param>
    /// <returns>Operation result containing list of patient examination cards</returns>
    public async Task<OperationResult<List<PatientExaminationCardDto>>> Handle(FetchPatientExaminationCardsInRegularModeQuery request, CancellationToken cancellationToken)
    {
        // Check if patient exists
        var checkPatientExists = await _patientRepository.GetPatientById(request.PatientId);

        // If patient does not exist, return failure result
        if (checkPatientExists is null) 
            return OperationResult<List<PatientExaminationCardDto>>.Failure("Patient Not Found");

        // Fetch patient examination cards in regular mode
        var patientExaminationCards = await _patientExaminationCardRepository
            .GetPatientExaminationCardDtosInRegularModeByPatientId(request.PatientId);

        return OperationResult<List<PatientExaminationCardDto>>.Success(patientExaminationCards);
    }
}
