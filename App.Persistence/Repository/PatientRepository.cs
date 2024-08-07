﻿using App.Domain.DTOs.Common.Response;
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

    public void DeletePatient(Patient patient) =>
        _oralEhrContext.Patients.Remove(patient);

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
