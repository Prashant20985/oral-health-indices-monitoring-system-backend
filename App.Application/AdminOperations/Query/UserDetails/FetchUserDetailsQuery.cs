﻿using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.AdminOperations.Query.UserDetails;

/// <summary>
/// Represents a request to fetch the details of a specific user.
/// </summary>
public record FetchUserDetailsQuery(string UserName) : IRequest<OperationResult<ApplicationUserDto>>;
