﻿using App.Domain.DTOs.Common.Response;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

public interface IPatientRepository
{
    IQueryable<PatientResponseDto> GetAllActivePatients();

    IQueryable<PatientResponseDto> GetAllArchivedPatients();

    Task CreatePatient(Patient patient);

    Task<Patient> GetPatientById(Guid id);

    void DeletePatient(Patient patient);

    Task<Patient> GetPatientByEmail(string email);

    Task<PatientResponseDto> GetPatientDetails(Guid patientId);
}
