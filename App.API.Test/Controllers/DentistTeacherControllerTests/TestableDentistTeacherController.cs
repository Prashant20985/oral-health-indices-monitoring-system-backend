using App.API.Controllers;
using MediatR;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class TestableDentistTeacherController : DentistTeacherController
{
    public void ExposeSetMediator(IMediator mediator)
    {
        SetMediator(mediator);
    }
}
