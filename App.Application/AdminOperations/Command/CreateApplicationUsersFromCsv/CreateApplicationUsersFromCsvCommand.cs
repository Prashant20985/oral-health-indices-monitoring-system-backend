using App.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace App.Application.AdminOperations.Command.CreateApplicationUsersFromCsv;

/// <summary>
/// Represents a command to create application users from a CSV file.
/// </summary>
public record CreateApplicationUsersFromCsvCommand(IFormFile File) : IRequest<OperationResult<string>>;