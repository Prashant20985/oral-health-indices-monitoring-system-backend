using App.Domain.DTOs.Common.Response;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

/// <summary>
/// Repository for managing patient examination cards.
/// </summary>
public interface IPatientRepository
{
    /// <summary>
    ///  Gets all active patients.
    /// </summary>
    /// <returns></returns>
    IQueryable<PatientResponseDto> GetAllActivePatients();

    /// <summary>
    ///  Gets all archived patients.
    /// </summary>
    /// <returns></returns>
    IQueryable<PatientResponseDto> GetAllArchivedPatients();

    /// <summary>
    /// Gets all patients.
    /// </summary>
    /// <param name="patient"></param>
    /// <returns></returns>
    Task CreatePatient(Patient patient);
    
    /// <summary>
    ///  Gets all patients by their unique identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Patient> GetPatientById(Guid id);
    
    /// <summary>
    ///  Deletes a patient by their unique identifier.
    /// </summary>
    /// <param name="patientId"></param>
    /// <returns></returns>
    Task DeletePatient(Guid patientId);
    
    /// <summary>
    ///  Gets a patient by their email.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<Patient> GetPatientByEmail(string email);
    
    /// <summary>
    ///  Gets patoent details by their unique identifier.
    /// </summary>
    /// <param name="patientId"></param>
    /// <returns></returns>
    Task<PatientResponseDto> GetPatientDetails(Guid patientId);
}
