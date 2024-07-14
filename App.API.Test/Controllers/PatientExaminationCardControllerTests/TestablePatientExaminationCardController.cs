using App.API.Controllers;
using MediatR;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class TestablePatientExaminationCardController : PatientExaminationCardController
{
    public void ExposeSetMediator(IMediator mediator)
    {
        SetMediator(mediator);
    }
}