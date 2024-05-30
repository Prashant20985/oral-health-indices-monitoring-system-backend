using App.API.Controllers;
using MediatR;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class TestableStudentExamController : StudentExamController
{
    public void ExposeSetMediator(IMediator mediator)
    {
        SetMediator(mediator);
    }
}
