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
    public async Task<ExamDto> PublishExam(Exam exam)
    {
        await _context.Exams.AddAsync(exam);
        return _mapper.Map<ExamDto>(exam);
    }

    /// <inheritdoc/>
    public async Task DeleteExam(Guid examId)
    {
        var exam = await _context.Exams
            .Include(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticeRiskFactorAssessment)
            .Include(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatient)
            .Include(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.APIBleeding)
            .Include(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.DMFT_DMFS)
            .Include(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.Bewe)
            .FirstOrDefaultAsync(x => x.Id == examId);

        _context.Exams.Remove(exam);
        _context.PracticePatients.RemoveRange(exam.PracticePatientExaminationCards.Select(x => x.PracticePatient));
        _context.PracticeRiskFactorAssessments.RemoveRange(exam.PracticePatientExaminationCards.Select(x => x.PracticeRiskFactorAssessment));
        _context.PracticeAPIBleedings.RemoveRange(exam.PracticePatientExaminationCards.Select(x => x.PracticePatientExaminationResult.APIBleeding));
        _context.PracticeDMFT_DMFSs.RemoveRange(exam.PracticePatientExaminationCards.Select(x => x.PracticePatientExaminationResult.DMFT_DMFS));
        _context.PracticeBewes.RemoveRange(exam.PracticePatientExaminationCards.Select(x => x.PracticePatientExaminationResult.Bewe));

    }

    /// <inheritdoc/>
    public async Task<Exam> GetExamById(Guid examId) =>
        await _context.Exams.FindAsync(examId);

    /// <inheritdoc/>
    public async Task<List<ExamDto>> GetExamDtosByGroupId(Guid groupId) => await _context.Exams
        .Where(x => x.GroupId == groupId)
        .ProjectTo<ExamDto>(_mapper.ConfigurationProvider)
        .OrderByDescending(x => x.DateOfExamination)
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
            .OrderBy(x => x.Student.UserName)
            .ProjectTo<PracticePatientExaminationCardDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();

    /// <inheritdoc/>
    public async Task<PracticePatientExaminationCardDto> GetPracticePatientExaminationCardDtoById(Guid practicePatientExaminationCardId) => 
        await _context.PracticePatientExaminationCards
            .Where(x => x.Id == practicePatientExaminationCardId)
            .ProjectTo<PracticePatientExaminationCardDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<PracticePatientExaminationCard> GetPracticePatientExaminationCardById(Guid practicePatientExaminationCardId) => 
        await _context.PracticePatientExaminationCards
        .Include(x => x.Exam)
        .FirstOrDefaultAsync(x => x.Id == practicePatientExaminationCardId);

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
    public async Task AddPracticePatientExaminationCard(PracticePatientExaminationCard practicePatientExaminationCard) =>
        await _context.PracticePatientExaminationCards.AddAsync(practicePatientExaminationCard);

    /// <inheritdoc/>
    public async Task AddPracticePatient(PracticePatient practicePatient) =>
        await _context.PracticePatients.AddAsync(practicePatient);

    /// <inheritdoc/>
    public async Task AddPracticeRiskFactorAssessment(PracticeRiskFactorAssessment practiceRiskFactorAssessment) =>
        await _context.PracticeRiskFactorAssessments.AddAsync(practiceRiskFactorAssessment);

    /// <inheritdoc/>
    public async Task AddPracticeDMFT_DMFS(PracticeDMFT_DMFS practiceDMFT_DMFS) =>
        await _context.PracticeDMFT_DMFSs.AddAsync(practiceDMFT_DMFS);

    /// <inheritdoc/>
    public async Task AddPracticeAPIBleeding(PracticeAPIBleeding practiceAPIBleeding) =>
        await _context.PracticeAPIBleedings.AddAsync(practiceAPIBleeding);

    /// <inheritdoc/>
    public async Task AddPracticeBewe(PracticeBewe practiceBewe) =>
        await _context.PracticeBewes.AddAsync(practiceBewe);

    /// <inheritdoc/>
    public async Task AddPracticePatientExaminationResult(PracticePatientExaminationResult practicePatientExaminationResult) =>
        await _context.PracticePatientExaminationResults.AddAsync(practicePatientExaminationResult);

    /// <inheritdoc/>
    public async Task<bool> CheckIfStudentHasAlreadyTakenTheExam(Guid examId, string studentId) =>
        await _context.PracticePatientExaminationCards.AnyAsync(x => x.StudentId == studentId && x.ExamId == examId);

    /// <inheritdoc/>
    public async Task<PracticePatientExaminationCardDto> GetPracticePatientExaminationCardByExamIdAndStudentId(Guid examId, string studentId) =>
        await _context.PracticePatientExaminationCards
            .Where(x => x.ExamId == examId && x.StudentId == studentId)
            .ProjectTo<PracticePatientExaminationCardDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
}
