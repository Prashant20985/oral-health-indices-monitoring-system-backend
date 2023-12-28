using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

public interface IPatientRepository
{
    IQueryable<PatientDto> GetAllActivePatients();

    IQueryable<PatientDto> GetAllArchivedPatients();

    IQueryable<PatientExaminationDto> GetAllActivePatientsByDoctorId(string doctorId);

    IQueryable<PatientExaminationDto> GetAllArchivedPatientsByDoctorId(string doctorId);

    Task CreatePatient (Patient patient);

    Task<Patient> GetPatientById(Guid id);

    void DeletePatient (Patient patient);

    Task<Patient> GetPatientByEmail(string email);
}
