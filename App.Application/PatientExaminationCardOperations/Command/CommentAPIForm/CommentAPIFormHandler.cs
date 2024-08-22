using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentAPIForm;

/// <summary>
/// Hnadler for adding comment to the patient examination card
/// </summary>
/// <param name="patientExaminationCardRepository">Repository for patient examination card</param>
internal sealed class CommentAPIFormHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
    : IRequestHandler<CommentAPIFormCommnand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the command for adding comment to the patient examination card
    /// </summary>
    /// <param name="request">Command for adding comment to the patient examination card</param>
    /// <param name="cancellationToken">The Cancellation Token</param>
    /// <returns>Result of the operation</returns>
    public async Task<OperationResult<Unit>> Handle(CommentAPIFormCommnand request, CancellationToken cancellationToken)
    {
        // Getting the API form by the card id
        var apiForm = await _patientExaminationCardRepository.GetAPIByCardId(request.CardId);

        // If the API form is not found, return an error
        if (apiForm is null)
            return OperationResult<Unit>.Failure("API Form not found");
        // If the API form is found, add the comment
        // If the request is from a student, add the comment as a student comment
        // If the request is from a doctor, add the comment as a doctor comment
        if (request.IsStudent)
            apiForm.AddStudentComment(request.Comment);
        else
            apiForm.AddDoctorComment(request.Comment);
        
        // Returning a successful operation result with a unit value
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
