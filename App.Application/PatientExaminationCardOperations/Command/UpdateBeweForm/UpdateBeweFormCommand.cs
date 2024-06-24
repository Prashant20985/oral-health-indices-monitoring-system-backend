using App.Application.Core;
using App.Domain.Models.Common.Bewe;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateBeweForm;

/// <summary>
/// Command to update BEWE form
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdateBeweFormCommand(Guid CardId, BeweAssessmentModel AssessmentModel)
    : IRequest<OperationResult<Unit>>;

