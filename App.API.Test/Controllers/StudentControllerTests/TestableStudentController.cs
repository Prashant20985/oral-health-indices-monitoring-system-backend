using App.API.Controllers;
using MediatR;

namespace App.API.Test.Controllers.StudentControllerTests;

public class TestableStudentController : StudentController
{
    public void ExposeSetMediator(IMediator mediator)
    {
        SetMediator(mediator);
    }
}