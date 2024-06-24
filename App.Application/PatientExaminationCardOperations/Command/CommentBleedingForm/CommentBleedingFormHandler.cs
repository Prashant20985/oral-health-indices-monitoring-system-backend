using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentBleedingForm;

/// <summary>
/// Handler to add a comment to the bleeding form
/// </summary>
/// <param name="patientExaminationCardRepository">The patient examination card repository</param>
internal sealed class CommentBleedingFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<CommentBleedingFormCommand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handler to add a comment to the bleeding form
    /// </summary>
    /// <param name="request">The command to add a comment to the bleeding form</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The result of the operation</returns>
    public async Task<OperationResult<Unit>> Handle(CommentBleedingFormCommand request, CancellationToken cancellationToken)
    {
        var bleedingForm = await _patientExaminationCardRepository.GetBleedingByCardId(request.CardId);

        if (bleedingForm is null)
            return OperationResult<Unit>.Failure("Bleeding form not found");

        bleedingForm.AddComment(request.DoctorComment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
