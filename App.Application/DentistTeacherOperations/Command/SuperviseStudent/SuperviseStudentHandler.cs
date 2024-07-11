using App.Application.Core;
using App.Domain.Models.Users;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.SuperviseStudent;

/// <summary>
/// Handles the command to supervise a student by a doctor.
/// </summary>
/// <param name="superviseRepository">Repository to handle the supervise operations.</param>
/// <param name="userRepository">Repository to handle the user operations.</param>
internal sealed class SuperviseStudentHandler(ISuperviseRepository superviseRepository, IUserRepository userRepository)
    : IRequestHandler<SuperviseStudentCommand, OperationResult<Unit>>
{
    private readonly ISuperviseRepository _superviseRepository = superviseRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<OperationResult<Unit>> Handle(SuperviseStudentCommand request, CancellationToken cancellationToken)
    {
        // Check if the student exists.
        var student = await _userRepository.GetApplicationUserWithRolesById(request.StudentId);
        if (student is null)
            return OperationResult<Unit>.Failure("Student Not Found.");

        // Check if the user is a student.
        if (!student.ApplicationUserRoles.Any(r => r.ApplicationRole.Name == "Student"))
            return OperationResult<Unit>.Failure("User is not a student.");

        // Check if the doctor exists.
        var teacher = await _userRepository.GetApplicationUserWithRolesById(request.DoctorId);
        if (teacher is null)
            return OperationResult<Unit>.Failure("Doctor Not Found.");

        // Check if the user is a doctor.
        if (!teacher.ApplicationUserRoles.Any(r => r.ApplicationRole.Name == "Dentist_Teacher_Examiner" ||
                r.ApplicationRole.Name == "Dentist_Teacher_Researcher"))
            return OperationResult<Unit>.Failure("User is not a Doctor.");

        // Check if the student is already under supervision.
        var supervisonCheck = await _superviseRepository.CheckStudentAlreadyUnderDoctorSupervison(request.StudentId, request.DoctorId);

        // If the student is already under supervision, return an error.
        if (supervisonCheck)
            return OperationResult<Unit>.Failure("Student is already under supervision.");

        // Create a new supervise object.
        var supervise = new Supervise(request.DoctorId, request.StudentId);

        // Add the supervise object to the database.
        await _superviseRepository.AddSupervise(supervise);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
