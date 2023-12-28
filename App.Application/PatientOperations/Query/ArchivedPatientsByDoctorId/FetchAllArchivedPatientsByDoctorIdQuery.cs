using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using MediatR;

namespace App.Application.PatientOperations.Query.ArchivedPatientsByDoctorId;


/// <summary>
/// Represents a query to fetch all archived patients by doctor ID, with optional name and email filters.
/// </summary>
public record FetchAllArchivedPatientsByDoctorIdQuery(string DoctorId, string Name, string Email)
    : IRequest<OperationResult<List<PatientExaminationDto>>>;
