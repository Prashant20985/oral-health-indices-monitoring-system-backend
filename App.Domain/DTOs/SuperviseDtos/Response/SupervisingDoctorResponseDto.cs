namespace App.Domain.DTOs.SuperviseDtos.Response;

/// <summary>
/// Represents a response DTO for a supervising doctor.
/// </summary>
public class SupervisingDoctorResponseDto
{
    /// <summary>
    /// Gets or initializes the ID of the supervising doctor.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or initializes the name of the supervising doctor.
    /// </summary>
    public string DoctorName { get; init; }
}
