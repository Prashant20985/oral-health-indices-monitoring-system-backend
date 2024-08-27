using App.API.Controllers;
using MediatR;

namespace App.API.Test.Controllers.AccountControllerTests;

public class TestableAccountController : AccountController
{
    public void ExposeSetMediator(IMediator mediator)
    {
        SetMediator(mediator);
    }
}