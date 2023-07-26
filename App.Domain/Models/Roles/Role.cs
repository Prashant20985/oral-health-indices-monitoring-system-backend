namespace App.Domain.Models.Roles;

/// <summary>
/// Enum representing the roles that a user can have in the application.
/// </summary>
public enum Role
{
    Admin,                          // Represents the role of an Administrator.
    Dentist_Teacher_Researcher,     // Represents the role of a DENTIST_TEACHER_RESEARCHER.
    Dentist_Teacher_Examiner,       // Represents the role of a DENTIST_TEACHER_EXAMINER.
    Student                         // Represents the role of a student.
}
