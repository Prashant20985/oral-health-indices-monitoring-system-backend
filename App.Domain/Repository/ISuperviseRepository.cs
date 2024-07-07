using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.SuperviseDtos.Response;
using App.Domain.Models.Users;

namespace App.Domain.Repository;

/// <summary>
/// Represents a repository for supervise-related operations.
/// </summary>
public interface ISuperviseRepository
{
    /// <summary>
    /// Adds a new supervise relationship.
    /// </summary>
    /// <param name="supervise">The supervise relationship to add.</param>
    /// <returns>A task representing the operation.</returns>
    Task AddSupervise(Supervise supervise);

    /// <summary>
    /// Removes a supervise relationship.
    /// </summary>
    /// <param name="supervise">The supervise relationship to remove.</param>
    void RemoveSupervise(Supervise supervise);

    /// <summary>
    /// Checks if a student is already under the supervision of a doctor.
    /// </summary>
    /// <param name="doctorId">The ID of the doctor.</param>
    /// <param name="studentId">The ID of the student.</param>
    /// <returns>Aa boolean indicating whether the student is already under the supervision of the doctor.</returns>
    Task<bool> CheckStudentAlreadyUnderDoctorSupervison(string studentId, string doctorId);

    /// <summary>
    /// Retrieves all students under the supervision of a doctor.
    /// </summary>
    /// <param name="doctorId">The ID of the doctor.</param>
    /// <returns>A list of students under the supervision of the doctor.</returns>
    Task<List<StudentResponseDto>> GetAllStudentsUnderSupervisionByDoctorId(string doctorId);

    /// <summary>
    /// Retrieves all doctors supervising a student.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <returns>A list of doctors supervising the student.</returns>
    Task<List<SupervisingDoctorResponseDto>> GetAllSupervisingDoctorsByStudentId(string studentId);
}
