using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardInTestMode;

/// <summary>
/// Query to fetch patient examination cards in test mode
/// </summary>
public record FetchPatientExaminationCardsInTestModeQuery(Guid PatientId) 
    : IRequest<OperationResult<List<PatientExaminationCardDto>>>;
