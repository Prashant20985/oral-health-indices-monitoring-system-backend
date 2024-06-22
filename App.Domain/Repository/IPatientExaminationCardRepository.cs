using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

public interface IPatientExaminationCardRepository
{
    Task<PatientExaminationCardDto> GetPatientExaminationCardDto(Guid cardId);
    Task<Bewe> GetBeweByCardId(Guid cardId);
    Task<API> GetAPIByCardId(Guid cardId);
    Task<Bleeding> GetBleedingByCardId(Guid cardId);
    Task<DMFT_DMFS> GetDMFT_DMFSByCardId(Guid cardId);
    Task AddPracticePatientExaminationCard(PatientExaminationCard patientExaminationCard);
    Task AddRiskFactorAssessment(RiskFactorAssessment riskFactorAssessment);
    Task AddPatientExaminationResult(PatientExaminationResult patientExaminationResult);
    Task AddBewe(Bewe bewe);
    Task AddAPI(API api);
    Task AddBleeding(Bleeding bleeding);
    Task AddDMFT_DMFS(DMFT_DMFS dmft_dmfs);
    Task DeletePatientExaminationCard(Guid cardId);
}
