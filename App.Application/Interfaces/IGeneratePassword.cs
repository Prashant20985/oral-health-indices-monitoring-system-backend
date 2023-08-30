namespace App.Application.Interfaces;

/// <summary>
/// Service class to generate a random password
/// </summary>
public interface IGeneratePassword
{
    string GenerateRandomPassword();
}