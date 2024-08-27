using App.API.Controllers;
using MediatR;

namespace App.API.Test.Controllers.PatientControllerTests;

public class TestablePatientController : PatientController
{
    public void ExposeSetMediator(IMediator mediator)
    {
        SetMediator(mediator);
    }
}
