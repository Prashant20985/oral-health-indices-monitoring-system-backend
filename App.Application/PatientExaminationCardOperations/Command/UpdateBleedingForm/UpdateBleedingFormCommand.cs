using App.Application.Core;
using App.Domain.Models.Common.APIBleeding;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateBleedingForm;

/// <summary>
/// Command to update bleeding form
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdateBleedingFormCommand(Guid CardId, APIBleedingAssessmentModel AssessmentModel)
    : IRequest<OperationResult<Unit>>;
