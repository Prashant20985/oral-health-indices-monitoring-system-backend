using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardDetails;

/// <summary>
/// Query to fetch patient examination card details
/// </summary>
public record FetchPatientExaminationCardDetailsQuery(Guid CardId)
    : IRequest<OperationResult<PatientExaminationCardDto>>;
