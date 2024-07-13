using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardsAssignedToDoctor;

/// <summary>
/// Query for fetching patient examination cards assigned to a doctor.
/// </summary>
public record FetchPatientExaminationCardsAssignedToDoctorQuery(
 string DoctorId,
 string StudentId,
 int Year,
 int Month) : IRequest<OperationResult<List<PatientDetailsWithExaminationCards>>>;
