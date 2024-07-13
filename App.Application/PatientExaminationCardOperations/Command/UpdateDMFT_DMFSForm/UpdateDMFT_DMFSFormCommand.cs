using App.Application.Core;
using App.Domain.DTOs.Common.Request;
using App.Domain.DTOs.Common.Response;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;

/// <summary>
/// Command to update DMFT/DMFS form
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdateDMFT_DMFSFormCommand(Guid CardId,
    UpdateDMFT_DMFSRequestDto UpdateDMFT_DMFS) : IRequest<OperationResult<DMFT_DMFSResultResponseDto>>;
