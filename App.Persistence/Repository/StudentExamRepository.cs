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
            .ThenInclude(x => x.API)
            .Include(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.Bleeding)
            .Include(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.DMFT_DMFS)
            .Include(x => x.PracticePatientExaminationCards)
            .ThenInclude(x => x.PracticePatientExaminationResult)
            .ThenInclude(x => x.Bewe)
            .FirstOrDefaultAsync(x => x.Id == examId);

        if (exam != null)
        {
            _context.Exams.Remove(exam);

            if (exam.PracticePatientExaminationCards.Count != 0)
            {
                _context.PracticePatientExaminationCards.RemoveRange(exam.PracticePatientExaminationCards);

                foreach (var card in exam.PracticePatientExaminationCards)
                {
                    _context.PracticePatients.Remove(card.PracticePatient);
                    _context.PracticeRiskFactorAssessments.Remove(card.PracticeRiskFactorAssessment);
                    _context.PracticeBleedings.Remove(card.PracticePatientExaminationResult.Bleeding);
                    _context.PracticeAPIs.Remove(card.PracticePatientExaminationResult.API);
                    _context.PracticeDMFT_DMFSs.Remove(card.PracticePatientExaminationResult.DMFT_DMFS);
                    _context.PracticeBewes.Remove(card.PracticePatientExaminationResult.Bewe);
                }
            }
        }
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
    public async Task<PracticeAPI> GetPracticeAPIByCardId(Guid practicePatientExaminationCardId) =>
        await _context.PracticePatientExaminationCards
            .Where(x => x.Id == practicePatientExaminationCardId)
            .Select(x => x.PracticePatientExaminationResult.API)
            .FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<PracticeBleeding> GetPracticeBleedingByCardId(Guid practicePatientExaminationCardId) =>
        await _context.PracticePatientExaminationCards
            .Where(x => x.Id == practicePatientExaminationCardId)
            .Select(x => x.PracticePatientExaminationResult.Bleeding)
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

    public async Task AddPracticeAPI(PracticeAPI practiceAPI) =>
        await _context.PracticeAPIs.AddAsync(practiceAPI);

    public async Task AddPracticeBleeding(PracticeBleeding practiceBleeding) =>
        await _context.PracticeBleedings.AddAsync(practiceBleeding);

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
            .AsNoTracking()
            .FirstOrDefaultAsync();

    /// <inheritdoc/>
    public async Task<List<ExamDto>> UpcomingExams(string studentId) => await _context.Exams
        .Where(x => x.Group.StudentGroups.Any(sg => sg.StudentId == studentId)
            && (x.DateOfExamination.Date >= DateTime.UtcNow.Date ||
                (x.DateOfExamination.Date == DateTime.UtcNow.Date && x.EndTime > TimeOnly.FromDateTime(DateTime.UtcNow))))
        .ProjectTo<ExamDto>(_mapper.ConfigurationProvider)
        .OrderBy(x => x.DateOfExamination)
        .ThenBy(x => x.StartTime)
        .Take(3)
        .ToListAsync();

}
