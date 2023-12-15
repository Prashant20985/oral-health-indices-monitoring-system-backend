using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

public interface IPatientRepository
{
    IQueryable<Patient> GetAllActivePatients();

    IQueryable<Patient> GetAllArchivedPatients();

    IQueryable<Patient> GetAllActivePatientsByDoctorId(string doctorId);

    IQueryable<Patient> GetAllArchivedPatientsByDoctorId(string doctorId);

    Task CreatePatient (Patient patient);

    Task<Patient> GetPatientById(Guid id);

    void DeletePatient (Patient patient);

    Task<Patient> GetPatientByEmail(string email);
}
