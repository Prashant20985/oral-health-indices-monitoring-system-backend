using App.Application.Core;
using MediatR;

namespace App.API.Controllers
{
    internal class FetchResearchGroupDetailsQuery : IRequest<OperationResult<object>>
    {
    }
}