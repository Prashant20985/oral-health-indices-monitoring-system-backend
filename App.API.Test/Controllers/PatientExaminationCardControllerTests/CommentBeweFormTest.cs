﻿using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.CommentBeweForm;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class CommentBeweFormTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public CommentBeweFormTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
                new Claim(ClaimTypes.Role, "Student")
           }, "mock"));

        _patientExaminationCardController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    [Fact]
    public async Task CommentBeweForm_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var APIForm = new Domain.Models.OralHealthExamination.API();
        var beweForm = new Bewe();
        var dMFT_dMFSForm = new DMFT_DMFS();
        var bleedingForm = new Bleeding();
        var patientExaminationResult = new PatientExaminationResult(beweForm.Id, dMFT_dMFSForm.Id, APIForm.Id, bleedingForm.Id)
        {
            API = APIForm,
            Bewe = beweForm,
            DMFT_DMFS = dMFT_dMFSForm,
            Bleeding = bleedingForm
        };

        var patientExaminationCard = new PatientExaminationCard(patientId)
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var comment = "This is a test comment.";

        _mediator.Setup(x => x.Send(It.IsAny<CommentBeweFormCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _patientExaminationCardController.CommentBeweForm(patientExaminationCard.Id, comment);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CommentBeweForm_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var APIForm = new Domain.Models.OralHealthExamination.API();
        var beweForm = new Bewe();
        var dMFT_dMFSForm = new DMFT_DMFS();
        var bleedingForm = new Bleeding();
        var patientExaminationResult = new PatientExaminationResult(beweForm.Id, dMFT_dMFSForm.Id, APIForm.Id, bleedingForm.Id)
        {
            API = APIForm,
            Bewe = beweForm,
            DMFT_DMFS = dMFT_dMFSForm,
            Bleeding = bleedingForm
        };

        var patientExaminationCard = new PatientExaminationCard(patientId)
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var comment = "";

        _mediator.Setup(x => x.Send(It.IsAny<CommentBeweFormCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Comment cannot be empty."));

        // Act
        var result = await _patientExaminationCardController.CommentBeweForm(patientExaminationCard.Id, comment);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task CommentBeweForm_WithInvalidPatientExaminationCard_ShouldReturnBadRequest()
    {
        // Arrange
        var patientExaminationCardId = Guid.NewGuid();

        _mediator.Setup(x => x.Send(It.IsAny<CommentBeweFormCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Patient examination card not found."));

        // Act
        var result = await _patientExaminationCardController.CommentBeweForm(patientExaminationCardId, "This is a test comment.");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}
