﻿using App.Domain.DTOs.ApplicationUserDtos.Request;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.OralHealthExamination;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Models.Users;

/// <summary>
/// Class representing a user in the application. It inherits from the IdentityUser class provided by Microsoft.AspNetCore.Identity.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Initializes a new instance of the ApplicationUser class.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="phoneNumber">The phone number of the user.</param>
    /// <param name="guestUserComment">The comment for the guest user.</param>
    public ApplicationUser(string email,
        string firstName,
        string lastName,
        string phoneNumber,
        string guestUserComment)
    {
        UserName = ExtractUserNameFromEmail(email);
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        GuestUserComment = guestUserComment;
    }

    /// <summary>
    /// Gets the first name of the user.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Gets the last name of the user.
    /// </summary>
    public string LastName { get; private set; } = null;

    /// <summary>
    /// Gets a value indicating whether the user's account is active or not.
    /// </summary>
    public bool IsAccountActive { get; private set; } = true;

    /// <summary>
    /// Gets the date and time when the user's account was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time when the user's account was deleted.
    /// This property is nullable, meaning it can have a null value.
    /// </summary>
    public DateTime? DeletedAt { get; private set; } = null;

    /// <summary>
    /// Gets the comment of account was deletion.
    /// </summary>
    public string DeleteUserComment { get; private set; } = null;

    /// <summary>
    /// Gets an comment associated with the guest user.
    /// The initial value of this property is set to null.
    /// </summary>
    public string GuestUserComment { get; private set; } = null;

    /// <summary>
    /// Toggles the activation status of the user's account.
    /// </summary>
    public void ActivationStatusToggle() => IsAccountActive = !IsAccountActive;

    /// <summary>
    /// Extracts the username portion from the user's email address.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    /// <returns>The extracted username.</returns>
    private static string ExtractUserNameFromEmail(string email) => email[..email.IndexOf("@")];

    /// <summary>
    /// Marks the user as deleted and sets deletion-related properties.
    /// </summary>
    /// <param name="deleteComment">The comment or reason for deletion.</param>
    public void DeleteUser(string deleteComment)
    {
        DeletedAt = DateTime.UtcNow;
        DeleteUserComment = deleteComment;
    }

    /// <summary>
    /// Updates user properties based on the provided DTO.
    /// </summary>
    /// <param name="updateApplicationUser">DTO containing updated user information.</param>
    public void UpdateUser(UpdateApplicationUserRequestDto updateApplicationUser)
    {
        FirstName = updateApplicationUser.FirstName;
        LastName = updateApplicationUser.LastName;
        PhoneNumber = updateApplicationUser.PhoneNumber;
        GuestUserComment = updateApplicationUser.GuestUserComment;
    }

    /// <summary>
    /// Gets or sets the collection of roles associated with the user.
    /// </summary>
    public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = new List<ApplicationUserRole>();

    /// <summary>
    /// Gets or sets the collection of refresh tokens associated with the user.
    /// </summary>
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    /// <summary>
    /// Gets or sets the collection of groups the user is a part of.
    /// </summary>
    public ICollection<Group> Groups { get; set; } = new List<Group>();

    /// <summary>
    /// Gets or sets the collection of student-group relationships associated with the user.
    /// </summary>
    public ICollection<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();

    /// <summary>
    /// Gets or sets the collection of user requests associated with the user.
    /// </summary>
    public ICollection<UserRequest> UserRequests { get; set; } = new List<UserRequest>();

    /// <summary>
    /// Gets or sets the collection of patients associated with the user.
    /// </summary>
    public ICollection<Patient> Patients { get; set; } = new List<Patient>();

    /// <summary>
    /// Gets or sets the collection of Resarch groups associated with the user.
    /// </summary>
    public ICollection<ResearchGroup> PatientGroups { get; set; } = new List<ResearchGroup>();

    /// <summary>
    /// Gets or sets the collection of patient examination cards associated with the Student.
    /// </summary>
    public ICollection<PatientExaminationCard> PatientExaminationCardStudentNavigation { get; set; } = new List<PatientExaminationCard>();

    /// <summary>
    /// Gets or sets the collection of patient examination cards associated with the Doctor.
    /// </summary>
    public ICollection<PatientExaminationCard> PatientExaminationCardDoctorNavigation { get; set; } = new List<PatientExaminationCard>();

    /// <summary>
    /// Gets or sets the collection of practice patient examination cards associated with the user.
    /// </summary>
    public ICollection<PracticePatientExaminationCard> PracticePatientExaminationCards { get; set; } = new List<PracticePatientExaminationCard>();

    /// <summary>
    /// Gets or sets the collection of Supervise relationships where the user is the doctor.
    /// </summary>
    public ICollection<Supervise> SuperviseDoctorNavigation { get; set; } = new List<Supervise>();

    /// <summary>
    /// Gets or sets the collection of Supervise relationships where the user is the student.
    /// </summary>
    public ICollection<Supervise> SuperviseStudentNavigation { get; set; } = new List<Supervise>();
}

