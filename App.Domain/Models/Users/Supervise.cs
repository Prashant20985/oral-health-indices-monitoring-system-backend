namespace App.Domain.Models.Users;

/// <summary>
/// Class representing supervision between a doctor and a student.
/// </summary>
/// <param name="doctorId">Unique identifier of the doctor.</param>
/// <param name="studentId">Unique identifier of the student.</param>
public class Supervise(string doctorId, string studentId)
{
    /// <summary>
    /// Gets or sets the unique identifier of the supervision.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the unique identifier of the doctor.
    /// </summary>
    public string DoctorId { get; private set; } = doctorId;

    /// <summary>
    /// Gets or sets the doctor.
    /// </summary>
    public ApplicationUser Doctor { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the student.
    /// </summary>
    public string StudentId { get; private set; } = studentId;

    /// <summary>
    /// Gets or sets the student.
    /// </summary>
    public ApplicationUser Student { get; set; }
    
}
