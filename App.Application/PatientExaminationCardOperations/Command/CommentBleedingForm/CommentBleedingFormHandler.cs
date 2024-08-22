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
        // Getting the bleeding form by the card id
        var bleedingForm = await _patientExaminationCardRepository.GetBleedingByCardId(request.CardId);

        //  If the bleeding form is not found, return an error
        if (bleedingForm is null)
            return OperationResult<Unit>.Failure("Bleeding form not found");

        // If the bleeding form is found, add the comment
        // If the request is from a student, add the comment as a student comment
        // If the request is from a doctor, add the comment as a doctor comment
        if (request.IsStudent)
            bleedingForm.AddStudentComment(request.Comment);
        else
            bleedingForm.AddDoctorComment(request.Comment);

        // Returning a successful operation result with a unit value
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
