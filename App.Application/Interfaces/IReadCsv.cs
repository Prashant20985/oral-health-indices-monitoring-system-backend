using App.Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace App.Application.Interfaces;

/// <summary>
/// Service class to read columns from a .csv file.
/// </summary>
public interface IReadCsv
{
    List<CreateApplicationUserFromCsvDto> ReadApplicationUsersFromCsv(IFormFile file);
}
