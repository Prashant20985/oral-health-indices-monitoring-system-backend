namespace App.Application.Interfaces;

/// <summary>
/// Service class to generate a random password
/// </summary>
public interface IGeneratePassword
{
    /// <summary>
    /// Generates a random password
    /// </summary>
    /// <returns></returns>
    string GenerateRandomPassword();
}