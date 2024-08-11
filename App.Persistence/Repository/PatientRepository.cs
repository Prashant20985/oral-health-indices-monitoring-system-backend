using App.Domain.DTOs.Common.Response;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using App.Persistence.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly OralEhrContext _oralEhrContext;
    private readonly IMapper _mapper;

    public PatientRepository(OralEhrContext oralEhrContext, IMapper mapper)
    {
        _oralEhrContext = oralEhrContext;
        _mapper = mapper;
    }

    public async Task CreatePatient(Patient patient) => await
        _oralEhrContext.Patients.AddAsync(patient);

    public async Task DeletePatient(Guid patientId)
    {
        var patientToDelete = await _oralEhrContext.Patients
            .Include(patient => patient.PatientExaminationCards)
            .ThenInclude(patient => patient.PatientExaminationResult)
            .ThenInclude(patient => patient.API)
            .Include(patient => patient.PatientExaminationCards)
            .ThenInclude(patient => patient.PatientExaminationResult)
            .ThenInclude(patient => patient.Bewe)
            .Include(patient => patient.PatientExaminationCards)
            .ThenInclude(patient => patient.PatientExaminationResult)
            .ThenInclude(patient => patient.Bleeding)
            .Include(patient => patient.PatientExaminationCards)
            .ThenInclude(patient => patient.PatientExaminationResult)
            .ThenInclude(patient => patient.DMFT_DMFS)
            .Include(patient => patient.PatientExaminationCards)
            .ThenInclude(patient => patient.RiskFactorAssessment)
            .FirstOrDefaultAsync(patient => patient.Id.Equals(patientId));

        if (patientToDelete is not null)
        {
            _oralEhrContext.Patients.Remove(patientToDelete);

            if (patientToDelete.PatientExaminationCards.Count != 0)
            {
                _oralEhrContext.PatientExaminationCards.RemoveRange(patientToDelete.PatientExaminationCards);

                foreach (var card in patientToDelete.PatientExaminationCards)
                {
                    _oralEhrContext.RiskFactorAssessments.Remove(card.RiskFactorAssessment);
                    _oralEhrContext.DMFT_DMFSs.Remove(card.PatientExaminationResult.DMFT_DMFS);
                    _oralEhrContext.Bleedings.Remove(card.PatientExaminationResult.Bleeding);
                    _oralEhrContext.Bewes.Remove(card.PatientExaminationResult.Bewe);
                    _oralEhrContext.APIs.Remove(card.PatientExaminationResult.API);
                }
            }
        }
    }

    public IQueryable<PatientResponseDto> GetAllActivePatients() => _oralEhrContext.Patients
        .Where(patient => !patient.IsArchived)
        .ProjectTo<PatientResponseDto>(_mapper.ConfigurationProvider)
        .OrderByDescending(x => x.CreatedAt)
        .AsQueryable();

    public IQueryable<PatientResponseDto> GetAllArchivedPatients() => _oralEhrContext.Patients
        .Where(patient => patient.IsArchived)
        .ProjectTo<PatientResponseDto>(_mapper.ConfigurationProvider)
        .OrderBy(x => x.CreatedAt)
        .AsQueryable();

    public async Task<Patient> GetPatientById(Guid id) => await _oralEhrContext.Patients
        .FirstOrDefaultAsync(patient => patient.Id.Equals(id));

    public async Task<Patient> GetPatientByEmail(string email) => await _oralEhrContext.Patients
        .FirstOrDefaultAsync(patient => patient.Email.Equals(email));

    public async Task<PatientResponseDto> GetPatientDetails(Guid patientId) => await _oralEhrContext.Patients
        .Where(patient => patient.Id.Equals(patientId))
        .ProjectTo<PatientResponseDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();

}
