using App.Application.Core;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateRiskFactorAssessmentForm;

/// <summary>
/// Command to update risk factor assessment form
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdateRiskFactorAssessmentFormCommand(Guid CardId, RiskFactorAssessmentModel AssessmentModel)
    : IRequest<OperationResult<Unit>>;
