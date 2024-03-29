﻿using App.Domain.DTOs.Common.Response;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

public interface IPatientRepository
{
    IQueryable<PatientDto> GetAllActivePatients();

    IQueryable<PatientDto> GetAllArchivedPatients();

    IQueryable<PatientDto> GetAllActivePatientsByDoctorId(string doctorId);

    IQueryable<PatientDto> GetAllArchivedPatientsByDoctorId(string doctorId);

    Task CreatePatient(Patient patient);

    Task<Patient> GetPatientById(Guid id);

    void DeletePatient(Patient patient);

    Task<Patient> GetPatientByEmail(string email);

    Task<PatientDto> GetPatientDetails(Guid patientId);
}
