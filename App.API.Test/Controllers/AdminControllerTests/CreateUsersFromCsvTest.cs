using App.Application.AdminOperations.Command.CreateApplicationUsersFromCsv;
using App.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AdminControllerTests
{
    public class CreateUsersFromCsvTest
    {
        private readonly TestableAdminController _adminController;
        private readonly Mock<IMediator> _mediatorMock;

        public CreateUsersFromCsvTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _adminController = new TestableAdminController();
            _adminController.ExposeSetMediator(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateUsersFromCsvTest_Returns_OkResult()
        {
            // Arrange
            var csvFileMock = new Mock<IFormFile>();
            csvFileMock.Setup(f => f.FileName).Returns("test.csv");

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateApplicationUsersFromCsvCommand>(), default))
                .ReturnsAsync(OperationResult<string>.Success(""));

            // Act
            var result = await _adminController.CreateUsersFromCsv(csvFileMock.Object);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            Assert.IsType<OkObjectResult>(actionResult);
        }
    }
}
