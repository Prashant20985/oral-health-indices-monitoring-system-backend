namespace App.Domain.DTOs.ExamDtos.Response;

/// <summary>
/// Represents the student exam result response DTO.
/// </summary>
public class StudentExamResultResponseDto
{
    /// <summary>
    /// Gets or initializes the user name.
    /// </summary>
    public string UserName { get; init; }

    /// <summary>
    /// Gets or initializes the first name.
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Gets or initializes the last name.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Gets or initializes the email.
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Gets or initializes the student mark.
    /// </summary>
    public decimal StudentMark { get; init; }
}
