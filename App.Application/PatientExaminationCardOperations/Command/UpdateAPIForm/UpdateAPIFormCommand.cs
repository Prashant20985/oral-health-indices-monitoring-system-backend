using App.Application.Core;
using App.Domain.Models.Common.APIBleeding;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateAPIForm;

/// <summary>
/// Command to update API form
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdateAPIFormCommand(Guid CardId, APIBleedingAssessmentModel AssessmentModel)
    : IRequest<OperationResult<Unit>>;
