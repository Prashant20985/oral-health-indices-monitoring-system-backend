using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.UnsuperviseStudent;

/// <summary>
/// Handles the Unspervise Student Command
/// </summary>
/// <param name="superviseRepository"></param>
internal sealed class UnsuperviseStudentHandler(ISuperviseRepository superviseRepository)
    : IRequestHandler<UnsuperviseStudentCommand, OperationResult<Unit>>
{
    private readonly ISuperviseRepository _superviseRepository = superviseRepository;

    public async Task<OperationResult<Unit>> Handle(UnsuperviseStudentCommand request, CancellationToken cancellationToken)
    {
        // Get the supervise object
        var supervise = await _superviseRepository.GetSuperviseByDoctorIdAndStudentId(request.DoctorId, request.StudentId);

        // If the supervise object is null, return an error
        if (supervise is null)
            return OperationResult<Unit>.Failure("Student not in supervison");

        // Remove the supervise object
        _superviseRepository.RemoveSupervise(supervise);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
