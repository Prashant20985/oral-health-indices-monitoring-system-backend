using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCards;

/// <summary>
/// Query to fetch all patient examination cards in regular mode
/// </summary>
public record FetchPatientExaminationCardsQuery(Guid PatientId)
    : IRequest<OperationResult<List<PatientExaminationCardDto>>>;
