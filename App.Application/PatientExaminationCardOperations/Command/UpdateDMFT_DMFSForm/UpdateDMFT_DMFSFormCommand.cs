using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;

/// <summary>
/// Command to update DMFT/DMFS form
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdateDMFT_DMFSFormCommand(Guid CardId, DMFT_DMFSAssessmentModel AssessmentModel)
    : IRequest<OperationResult<DMFT_DMFSResultResponseDto>>;
