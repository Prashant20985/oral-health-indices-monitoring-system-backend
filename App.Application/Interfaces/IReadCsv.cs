using App.Domain.DTOs.ApplicationUserDtos.Request;
using Microsoft.AspNetCore.Http;

namespace App.Application.Interfaces;

/// <summary>
/// Service class to read columns from a .csv file.
/// </summary>
public interface IReadCsv
{
    List<CreateApplicationUserFromCsvRequestDto> ReadApplicationUsersFromCsv(IFormFile file);
}
