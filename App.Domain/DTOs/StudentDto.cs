namespace App.Domain.DTOs;

/// <summary>
/// Data transfer object representing a student.
/// </summary>
public class StudentDto
{
    /// <summary>
    /// Gets or sets the Id of the user.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the user name of the user.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets a list of group names in which the student is a member.
    /// </summary>
    public List<string> Groups { get; set; }

}
