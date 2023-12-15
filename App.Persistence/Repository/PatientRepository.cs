using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using App.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly OralEhrContext _oralEhrContext;

    public PatientRepository(OralEhrContext oralEhrContext) =>
        _oralEhrContext = oralEhrContext;

    public async Task CreatePatient(Patient patient) => await 
        _oralEhrContext.Patients.AddAsync(patient);

    public void DeletePatient(Patient patient) =>
        _oralEhrContext.Patients.Remove(patient);

    public IQueryable<Patient> GetAllActivePatients() => _oralEhrContext.Patients
        .Where(patient => patient.IsArchived == false)
        .OrderBy(x => x.CreatedAt)
        .AsQueryable();

    public IQueryable<Patient> GetAllArchivedPatients() => _oralEhrContext.Patients
        .Where(patient => patient.IsArchived == true)
        .OrderBy(x => x.CreatedAt)
        .AsQueryable();

    public IQueryable<Patient> GetAllActivePatientsByDoctorId(string doctorId) => _oralEhrContext.Patients
        .Where(patient => patient.IsArchived == false && patient.DoctorId.Equals(doctorId))
        .Include(p => p.PatientGroup.GroupName)
        .Include(p => p.PatientExaminationCards.Select(x => new
        {
            x.RiskFactorAssessment,
            x.PatientExaminationResult.DMFT_DMFS,
            x.PatientExaminationResult.Bewe,
            x.PatientExaminationResult.APIBleeding
        }))
        .OrderBy(x => x.CreatedAt).AsQueryable();

    public IQueryable<Patient> GetAllArchivedPatientsByDoctorId(string doctorId) => _oralEhrContext.Patients
        .Where(patient => patient.IsArchived == true && patient.DoctorId.Equals(doctorId))
        .Include(p => p.PatientGroup.GroupName)
        .Include(p => p.PatientExaminationCards.Select(x => new 
        { 
            x.RiskFactorAssessment, 
            x.PatientExaminationResult.DMFT_DMFS, 
            x.PatientExaminationResult.Bewe, 
            x.PatientExaminationResult.APIBleeding 
        }))
        .OrderBy(x => x.CreatedAt).AsQueryable();

    public async Task<Patient> GetPatientById(Guid id) => await _oralEhrContext.Patients
        .FirstOrDefaultAsync(patient => patient.Id.Equals(id));

    public async Task<Patient> GetPatientByEmail(string email) => await _oralEhrContext.Patients
        .FirstOrDefaultAsync(patient => patient.Email.Equals(email));
}
