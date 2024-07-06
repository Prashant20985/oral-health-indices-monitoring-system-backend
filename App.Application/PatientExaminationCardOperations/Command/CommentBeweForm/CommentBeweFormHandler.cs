using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentBeweForm;

/// <summary>
/// Handler for Commment Bewe Form Command
/// </summary>
/// <param name="patientExaminationCardRepository">Patient Examination Card Repository</param>
internal sealed class CommentBeweFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<CommentBeweFormCommand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handle Comment Bewe Form Command
    /// </summary>
    /// <param name="request">Comment Bewe Form Command</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Operation Result</returns>
    public async Task<OperationResult<Unit>> Handle(CommentBeweFormCommand request, CancellationToken cancellationToken)
    {
        var beweForm = await _patientExaminationCardRepository.GetBeweByCardId(request.CardId);

        if (beweForm is null)
            return OperationResult<Unit>.Failure("Bewe form not found");

        beweForm.AddDoctorComment(request.DoctorComment);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
