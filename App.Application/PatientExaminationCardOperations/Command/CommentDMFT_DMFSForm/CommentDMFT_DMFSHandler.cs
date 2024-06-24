using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentDMFT_DMFSForm;

/// <summary>
/// Handler to add a comment to the DMFT_DMFS form
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
internal sealed class CommentDMFT_DMFSHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<CommentDMFT_DMFSCommand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the CommentDMFT_DMFSCommand
    /// </summary>
    /// <param name="request">The CommentDMFT_DMFSCommand</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the operation</returns>
    public async Task<OperationResult<Unit>> Handle(CommentDMFT_DMFSCommand request, CancellationToken cancellationToken)
    {
        var dmft_dmfsForm = await _patientExaminationCardRepository.GetDMFT_DMFSByCardId(request.CardId);

        if (dmft_dmfsForm is null)
            return OperationResult<Unit>.Failure("DMFT/DMFS form not found");

        dmft_dmfsForm.AddComment(request.DoctorComment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
