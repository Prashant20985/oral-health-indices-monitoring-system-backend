using App.API.Extensions;
using App.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??=
        HttpContext.RequestServices.GetService<IMediator>();

    /// <summary>
    /// Handles the operation result of a request and returns an appropriate action result.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The operation result to handle.</param>
    /// <returns>An action result based on the operation result.</returns>
    protected ActionResult HandleOperationResult<T>(OperationResult<T> result)
    {
        if (result == null) return NotFound();

        if (result.IsSuccessful)
        {
            if (result.ResultValue != null)
                return Ok(result.ResultValue);
            else
                return NotFound();
        }
        else
        {
            if (result.IsUnauthorized)
                return Unauthorized(result.ErrorMessage);
            else
                return BadRequest(result.ErrorMessage);
        }
    }

    /// <summary>
    /// Handles the operation paged result of a request and returns an appropriate action result.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="result">The operation paged result to handle.</param>
    /// <returns>An action result based on the operation result.</returns>
    protected ActionResult HandleOperationPagedResult<T>(OperationResult<PagedList<T>> result)
    {
        if (result == null) return NotFound();

        if (result.IsSuccessful)
        {
            if (result.ResultValue != null)
            {
                Response.AddPaginationHeader(result.ResultValue.CurrentPage,
                    result.ResultValue.PageSize, result.ResultValue.TotalCount,
                    result.ResultValue.TotalPages);

                return Ok(result.ResultValue);
            }
            else
                return NotFound();
        }
        else
        {
            if (result.IsUnauthorized)
                return Unauthorized(result.ErrorMessage);
            else
                return BadRequest(result.ErrorMessage);
        }
    }
}
