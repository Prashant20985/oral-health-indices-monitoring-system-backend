using App.API.Controllers;
using MediatR;

namespace App.API.Test.Controllers.AdminControllerTests;

public class TestableAdminController : AdminController
{
    public void ExposeSetMediator(IMediator mediator)
    {
        SetMediator(mediator);
    }
}
