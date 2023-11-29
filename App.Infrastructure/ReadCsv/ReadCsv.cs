using App.Application.Interfaces;
using App.Domain.DTOs;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace App.Infrastructure.ReadCsv;

public class ReadCsv : IReadCsv
{
    /// <summary>
    /// Reads users from a CSV file.
    /// </summary>
    /// <param name="file">The CSV file containing user data.</param>
    /// <returns>A list of CreateUserDto objects representing the users.</returns>
    public List<CreateApplicationUserFromCsvDto> ReadApplicationUsersFromCsv(IFormFile file)
    {
        List<CreateApplicationUserFromCsvDto> users = new();

        // Read the CSV file using CsvHelper library
        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Parse the CSV file and convert each record to CreateUserDto object
            users = csv.GetRecords<CreateApplicationUserFromCsvDto>().ToList();
        }

        return users;
    }
}
