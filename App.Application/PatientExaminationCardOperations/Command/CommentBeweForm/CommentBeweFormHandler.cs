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
        // Getting the BEWE form by the card id
        var beweForm = await _patientExaminationCardRepository.GetBeweByCardId(request.CardId);

        // If the BEWE form is not found, return an error
        if (beweForm is null)
            return OperationResult<Unit>.Failure("Bewe form not found"); 
        // If the BEWE form is found, add the comment
        // If the request is from a student, add the comment as a student comment
        // If the request is from a doctor, add the comment as a doctor comment
        if (request.IsStudent)
            beweForm.AddStudentComment(request.Comment);
        else
            beweForm.AddDoctorComment(request.Comment);

        // Returning a successful operation result with a unit value
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
