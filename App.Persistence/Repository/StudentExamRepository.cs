using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using App.Domain.Repository;
using App.Persistence.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

public class StudentExamRepository(OralEhrContext context, IMapper mapper) : IStudentExamRepository
{
    private readonly OralEhrContext _context = context;
    private readonly IMapper _mapper = mapper;

    /// <inheritdoc/>
    public async Task<Exam> PublishExam(Exam exam)
    {
        await _context.Exams.AddAsync(exam);
        return exam;
    }

    /// <inheritdoc/>
    public async void DeleteExam(Guid examId)
    {
        var exam = await GetExamById(examId);
        _context.Exams.Remove(exam);
    }

    /// <inheritdoc/>
    public async Task<Exam> GetExamById(Guid examId) =>
        await _context.Exams.FindAsync(examId);

    /// <inheritdoc/>
    public async Task<List<ExamDto>> GetExamDtosByGroupId(Guid groupId) => await _context.Exams
        .Where(x => x.GroupId == groupId)
        .ProjectTo<ExamDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

    /// <inheritdoc/>
    public async Task<ExamDto> GetExamDtoById(Guid examId) => await _context.Exams
        .Where(x => x.Id == examId)
        .ProjectTo<ExamDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<List<PracticePatientExaminationCardDto>> GetPracticePatientExaminationCardsByExamId(Guid examId) => 
        await _context.PracticePatientExaminationCards
            .Where(x => x.ExamId == examId)
            .ProjectTo<PracticePatientExaminationCardDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

    /// <inheritdoc/>
    public async Task<PracticePatientExaminationCardDto> GetPracticePatientExaminationCardDtoById(Guid practicePatientExaminationCardId) => 
        await _context.PracticePatientExaminationCards
            .Where(x => x.Id == practicePatientExaminationCardId)
            .ProjectTo<PracticePatientExaminationCardDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<PracticePatientExaminationCard> GetPracticePatientExaminationCardById(Guid practicePatientExaminationCardId) => 
        await _context.PracticePatientExaminationCards.FindAsync(practicePatientExaminationCardId);

    /// <inheritdoc/>
    public async Task<PracticeRiskFactorAssessment> GetPracticeRiskFactorAssessment(Guid practicePatientExaminationCardId) =>
    await _context.PracticePatientExaminationCards
        .Where(x => x.Id == practicePatientExaminationCardId)
        .Select(x => x.PracticeRiskFactorAssessment)
        .FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<PracticeAPIBleeding> GetPracticeAPIBleedingByCardId(Guid practicePatientExaminationCardId) =>
        await _context.PracticePatientExaminationCards
            .Where(x => x.Id == practicePatientExaminationCardId)
            .Select(x => x.PracticePatientExaminationResult.APIBleeding)
            .FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<PracticeDMFT_DMFS> GetPracticeDMFT_DMFSByCardId(Guid practicePatientExaminationCardId) =>
        await _context.PracticePatientExaminationCards
            .Where(x => x.Id == practicePatientExaminationCardId)
            .Select(x => x.PracticePatientExaminationResult.DMFT_DMFS)
            .FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<PracticeBewe> GetPracticeBeweByCardId(Guid practicePatientExaminationCardId) => 
        await _context.PracticePatientExaminationCards
            .Where(x => x.Id == practicePatientExaminationCardId)
            .Select(x => x.PracticePatientExaminationResult.Bewe)
            .FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<List<ExamDto>> GetExamDtosByStudentId(string studentId) => await _context.Exams
        .Where(x => x.Group.StudentGroups.Any(x => x.StudentId == studentId))
        .ProjectTo<ExamDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

    /// <inheritdoc/>
    public async Task<PracticePatientExaminationCard> AddPracticePatientExaminationCard(PracticePatientExaminationCard practicePatientExaminationCard)
    {
        await _context.PracticePatientExaminationCards.AddAsync(practicePatientExaminationCard);
        return practicePatientExaminationCard;
    }

    /// <inheritdoc/>
    public async Task<PatientDto> AddPracticePatient(PracticePatient practicePatient, Guid practicePatientExaminationCardId)
    {
        await _context.PracticePatients.AddAsync(practicePatient);
        var practicePatientExaminationCard = await GetPracticePatientExaminationCardById(practicePatientExaminationCardId);
        practicePatientExaminationCard.SetPatientId(practicePatient.Id);
        return _mapper.Map<PatientDto>(practicePatient);
    }

    /// <inheritdoc/>
    public async Task<PracticeRiskFactorAssessment> AddPracticeRiskFactorAssessment(PracticeRiskFactorAssessment practiceRiskFactorAssessment)
    {
        await _context.PracticeRiskFactorAssessments.AddAsync(practiceRiskFactorAssessment);
        return practiceRiskFactorAssessment;
    }

    /// <inheritdoc/>
    public async Task<PracticeDMFT_DMFS> AddPracticeDMFT_DMFS(PracticeDMFT_DMFS practiceDMFT_DMFS)
    {
        await _context.PracticeDMFT_DMFSs.AddAsync(practiceDMFT_DMFS);
        return practiceDMFT_DMFS;
    }

    /// <inheritdoc/>
    public async Task<PracticeAPIBleeding> AddPracticeAPIBleeding(PracticeAPIBleeding practiceAPIBleeding)
    {
        await _context.PracticeAPIBleedings.AddAsync(practiceAPIBleeding);
        return practiceAPIBleeding;
    }

    /// <inheritdoc/>
    public async Task<PracticeBewe> AddPracticeBewe(PracticeBewe practiceBewe)
    {
        await _context.PracticeBewes.AddAsync(practiceBewe);
        return practiceBewe;
    }

    /// <inheritdoc/>
    public async Task<PracticePatientExaminationResult> AddPracticePatientExaminationResult(PracticePatientExaminationResult practicePatientExaminationResult)
    {
        await _context.PracticePatientExaminationResults.AddAsync(practicePatientExaminationResult);
        return practicePatientExaminationResult;
    }
}
