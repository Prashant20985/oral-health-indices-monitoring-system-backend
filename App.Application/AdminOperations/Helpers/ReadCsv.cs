using App.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using CsvHelper;


namespace App.Application.AdminOperations.Helpers;

/// <summary>
/// Helper class for reading users from a CSV file.
/// </summary>
internal static class ReadCsv
{
    /// <summary>
    /// Reads users from a CSV file.
    /// </summary>
    /// <param name="file">The CSV file containing user data.</param>
    /// <returns>A list of CreateUserDto objects representing the users.</returns>
    public static List<CreateApplicationUserDto> ReadUsersFromCsv(IFormFile file)
    {
        List<CreateApplicationUserDto> users = new List<CreateApplicationUserDto>();

        // Read the CSV file using CsvHelper library
        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Parse the CSV file and convert each record to CreateUserDto object
            users = csv.GetRecords<CreateApplicationUserDto>().ToList();
        }

        return users;
    }
}