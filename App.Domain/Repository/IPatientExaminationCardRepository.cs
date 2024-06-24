using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

/// <summary>
/// Repository for managing patient examination cards.
/// </summary>
public interface IPatientExaminationCardRepository
{
    /// <summary>
    /// Gets a patient examination card data transfer object by its unique identifier.
    /// </summary>
    /// <param name="cardId">The unique identifier of the patient examination card.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the patient examination card data transfer object.</returns>
    Task<PatientExaminationCardDto> GetPatientExaminationCardDto(Guid cardId);

    /// <summary>
    /// Gets a patient examination card by its unique identifier.
    /// </summary>
    /// <param name="cardId">The unique identifier of the patient examination card.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the patient examination card.</returns>
    Task<Bewe> GetBeweByCardId(Guid cardId);

    /// <summary>
    /// Gets the Risk Factor Assessment associated with a patient examination card.
    /// </summary>
    /// <param name="cardId">The unique identifier of the patient examination card.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the Risk Factor Assessment.</returns>
    Task<RiskFactorAssessment> GetRiskFactorAssessmentByCardId(Guid cardId);

    /// <summary>
    /// Gets the API data associated with a patient examination card.
    /// </summary>
    /// <param name="cardId">The unique identifier of the patient examination card.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API data.</returns>
    Task<API> GetAPIByCardId(Guid cardId);

    /// <summary>
    /// Gets the bleeding data associated with a patient examination card.
    /// </summary>
    /// <param name="cardId">The unique identifier of the patient examination card.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the bleeding data.</returns>
    Task<Bleeding> GetBleedingByCardId(Guid cardId);

    /// <summary>
    /// Gets the DMFT/DMFS data associated with a patient examination card.
    /// </summary>
    /// <param name="cardId">The unique identifier of the patient examination card.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the DMFT/DMFS data.</returns>
    Task<DMFT_DMFS> GetDMFT_DMFSByCardId(Guid cardId);

    /// <summary>
    /// Gets a patient examination card by its unique identifier.
    /// </summary>
    /// <param name="cardId">The unique identifier of the patient examination card.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the patient examination card.</returns>
    Task<PatientExaminationCard> GetPatientExaminationCard(Guid cardId);

    /// <summary>
    /// Adds a patient examination card to the repository.
    /// </summary>
    /// <param name="patientExaminationCard">The patient examination card to add.</param>
    Task AddPatientExaminationCard(PatientExaminationCard patientExaminationCard);

    /// <summary>
    /// Adds a risk factor assessment to the repository.
    /// </summary>
    /// <param name="riskFactorAssessment">The risk factor assessment to add.</param>
    Task AddRiskFactorAssessment(RiskFactorAssessment riskFactorAssessment);

    /// <summary>
    /// Adds a patient examination result to the repository.
    /// </summary>
    /// <param name="patientExaminationResult">The patient examination result to add.</param>
    Task AddPatientExaminationResult(PatientExaminationResult patientExaminationResult);

    /// <summary>
    /// Adds an Bewe to the repository.
    /// </summary>
    /// <param name="bewe">The Bewe to add.</param>
    Task AddBewe(Bewe bewe);

    /// <summary>
    /// Adds an API to the repository.
    /// </summary>
    /// <param name="api">The API to add.</param>
    Task AddAPI(API api);

    /// <summary>
    /// Adds a bleeding to the repository.
    /// </summary>
    /// <param name="bleeding">The bleeding to add.</param>
    Task AddBleeding(Bleeding bleeding);

    /// <summary>
    /// Adds a DMFT/DMFS to the repository.
    /// </summary>
    /// <param name="dmft_dmfs">The DMFT/DMFS to add.</param>
    Task AddDMFT_DMFS(DMFT_DMFS dmft_dmfs);

    /// <summary>
    /// Deletes a patient examination card from the repository.
    /// </summary>
    /// <param name="cardId">The unique identifier of the patient examination card to delete.</param>
    Task DeletePatientExaminationCard(Guid cardId);

    /// <summary>
    /// Fetches all patient examination card data transfer objects in regular mode.
    /// </summary>
    /// <param name="patientId">The unique identifier of the patient.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of patient examination card data transfer objects.</returns>
    Task<List<PatientExaminationCardDto>> GetPatientExaminationCardDtosInRegularModeByPatientId(Guid patientId);

    /// <summary>
    /// Fetches all patient examination card data transfer objects in test mode.
    /// </summary>
    /// <param name="patientId">The unique identifier of the patient.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of patient examination card data transfer objects.</returns>
    Task<List<PatientExaminationCardDto>> GetPatientExminationCardDtosInTestModeByPatientId(Guid patientId);
}
