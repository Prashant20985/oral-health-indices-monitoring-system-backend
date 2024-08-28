using App.API.Controllers;
using MediatR;

namespace App.API.Test.Controllers.UserRequestControllerTests;

public class TestableUserRequestController : UserRequestController
{
    public void ExposeSetMediator(IMediator mediator)
    {
        SetMediator(mediator);
    }
}
