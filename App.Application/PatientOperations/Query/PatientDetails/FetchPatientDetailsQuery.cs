using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using MediatR;

namespace App.Application.PatientOperations.Query.PatientDetails;

/// <summary>
/// Represents a query to fetch patient details by patient ID.
/// </summary>
public record FetchPatientDetailsQuery(Guid PatientId) 
    : IRequest<OperationResult<PatientExaminationDto>>;
