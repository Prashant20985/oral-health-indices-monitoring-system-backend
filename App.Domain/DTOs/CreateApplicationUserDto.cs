﻿namespace App.Domain.DTOs;

/// <summary>
/// Data transfer object (DTO) for creating a user.
/// </summary>
public class CreateApplicationUserDto
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the user.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the comment for the user.
    /// Default value is null.
    /// </summary>
    public string GuestUserComment { get; set; }
}