﻿using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.AdminOperations.Query.DeletedApplicationUsersList;

/// <summary>
/// Represents a request to fetch a paged list of deleted application users.
/// </summary>
public record FetchDeletedApplicationUsersListQuery(string SearchTerm,
    int PageNumber,
    int PageSize) : IRequest<OperationResult<PagedList<ApplicationUserDto>>>;