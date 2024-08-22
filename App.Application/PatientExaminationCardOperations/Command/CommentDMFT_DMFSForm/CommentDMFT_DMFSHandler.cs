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
        // Getting the DMFT_DMFS form by the card id
        var dmft_dmfsForm = await _patientExaminationCardRepository.GetDMFT_DMFSByCardId(request.CardId);

        // If the DMFT_DMFS form is not found, return an error
        if (dmft_dmfsForm is null)
            return OperationResult<Unit>.Failure("DMFT/DMFS form not found");

        // If the DMFT_DMFS form is found, add the comment
        // If the request is from a student, add the comment as a student comment
        // If the request is from a doctor, add the comment as a doctor comment
        if (request.IsStudent)
            dmft_dmfsForm.AddStudentComment(request.Comment);
        else
            dmft_dmfsForm.AddDoctorComment(request.Comment);

        // Returning a successful operation result with a unit value
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
