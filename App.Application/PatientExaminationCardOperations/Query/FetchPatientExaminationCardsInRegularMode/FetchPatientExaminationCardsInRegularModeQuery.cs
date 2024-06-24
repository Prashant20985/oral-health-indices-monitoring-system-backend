using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Query.FetchAllPatientExaminationCardsInRegualrMode;

/// <summary>
/// Query to fetch all patient examination cards in regular mode
/// </summary>
public record FetchPatientExaminationCardsInRegularModeQuery(Guid PatientId)
    : IRequest<OperationResult<List<PatientExaminationCardDto>>>;
