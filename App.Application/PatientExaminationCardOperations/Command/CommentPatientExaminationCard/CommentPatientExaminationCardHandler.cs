using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentPatientExaminationCard;

/// <summary>
/// Handles the command to comment on a patient examination card
/// </summary>
/// <param name="patientExaminationCardRepository">Repository for patient examination card</param>
internal sealed class CommentPatientExaminationCardHandler(IPatientExaminationCardRepository patientExaminationCardRepository)
        : IRequestHandler<CommentPatientExaminationCardCommand, OperationResult<Unit>>
{
    private readonly IPatientExaminationCardRepository _patientExaminationCardRepository = patientExaminationCardRepository;

    /// <summary>
    /// Handles the command to comment on a patient examination card
    /// </summary>
    /// <param name="request">CommentPatientExaminationCardCommand</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>OperationResult of Unit</returns>
    public async Task<OperationResult<Unit>> Handle(CommentPatientExaminationCardCommand request, CancellationToken cancellationToken)
    {
        //  Getting the patient examination card by the card id
        var patientExaminationCard = await _patientExaminationCardRepository.GetPatientExaminationCard(request.Cardid);

        //  If the patient examination card is not found, return an error
        if (patientExaminationCard is null)
            return OperationResult<Unit>.Failure("Patient examination card not found");

        //  If the patient examination card is found, add the comment
        //  If the request is from a student, add the comment as a student comment
        //  If the request is from a doctor, add the comment as a doctor comment
        if (request.IsStudent)
            patientExaminationCard.AddStudentComment(request.Comment);
        else
            patientExaminationCard.AddDoctorComment(request.Comment);

        //  Returning a successful operation result with a unit value
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
