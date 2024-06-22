using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using App.Persistence.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

public class PatientExaminationRepository(OralEhrContext context, IMapper mapper)
    : IPatientExaminationCardRepository
{
    private readonly OralEhrContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task AddAPI(API api) => 
        await _context.APIs.AddAsync(api);

    public async Task AddBewe(Bewe bewe) => 
        await _context.Bewes.AddAsync(bewe);

    public async Task AddBleeding(Bleeding bleeding) =>
        await _context.Bleedings.AddAsync(bleeding);

    public async Task AddDMFT_DMFS(DMFT_DMFS dmft_dmfs) =>
        await _context.DMFT_DMFSs.AddAsync(dmft_dmfs);

    public async Task AddPatientExaminationResult(PatientExaminationResult patientExaminationResult) =>
        await _context.PatientExaminationResults.AddAsync(patientExaminationResult);

    public async Task AddPracticePatientExaminationCard(PatientExaminationCard patientExaminationCard) =>
        await _context.PatientExaminationCards.AddAsync(patientExaminationCard);

    public async Task AddRiskFactorAssessment(RiskFactorAssessment riskFactorAssessment) =>
        await _context.RiskFactorAssessments.AddAsync(riskFactorAssessment);

    public async Task DeletePatientExaminationCard(Guid cardId)
    {
        var card = await _context.PatientExaminationCards
            .Include(c => c.RiskFactorAssessment)
            .Include(c => c.PatientExaminationResult)
            .ThenInclude(c => c.API)
            .Include(c => c.PatientExaminationResult)
            .ThenInclude(c => c.Bewe)
            .Include(c => c.PatientExaminationResult)
            .ThenInclude(c => c.Bleeding)
            .Include(c => c.PatientExaminationResult)
            .ThenInclude(c => c.DMFT_DMFS)
            .FirstOrDefaultAsync(c => c.Id == cardId);

        if (card is not null)
        {
            _context.RiskFactorAssessments.Remove(card.RiskFactorAssessment);
            _context.APIs.Remove(card.PatientExaminationResult.API);
            _context.Bewes.Remove(card.PatientExaminationResult.Bewe);
            _context.Bleedings.Remove(card.PatientExaminationResult.Bleeding);
            _context.DMFT_DMFSs.Remove(card.PatientExaminationResult.DMFT_DMFS);
            _context.PatientExaminationResults.Remove(card.PatientExaminationResult);
            _context.PatientExaminationCards.Remove(card);
        }
    }

    public async Task<API> GetAPIByCardId(Guid cardId) => 
        await _context.PatientExaminationCards
            .Where(c => c.Id == cardId)
            .Select(c => c.PatientExaminationResult.API)
            .FirstOrDefaultAsync();

    public async Task<Bewe> GetBeweByCardId(Guid cardId) =>
        await _context.PatientExaminationCards
            .Where(c => c.Id == cardId)
            .Select(c => c.PatientExaminationResult.Bewe)
            .FirstOrDefaultAsync();

    public async Task<Bleeding> GetBleedingByCardId(Guid cardId) =>
        await _context.PatientExaminationCards
            .Where(c => c.Id == cardId)
            .Select(c => c.PatientExaminationResult.Bleeding)
            .FirstOrDefaultAsync();

    public async Task<DMFT_DMFS> GetDMFT_DMFSByCardId(Guid cardId) => 
        await _context.PatientExaminationCards
            .Where(c => c.Id == cardId)
            .Select(c => c.PatientExaminationResult.DMFT_DMFS)
            .FirstOrDefaultAsync();

    public async Task<PatientExaminationCardDto> GetPatientExaminationCardDto(Guid cardId) =>
        await _context.PatientExaminationCards
            .ProjectTo<PatientExaminationCardDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == cardId);
}
