using App.Application.AdminOperations.Command.CreateApplicationUser;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AdminControllerTests
{
    public class CreateUserTest
    {
        private readonly TestableAdminController _adminController;
        private readonly Mock<IMediator> _mediatorMock;

        public CreateUserTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _adminController = new TestableAdminController();
            _adminController.ExposeSetMediator(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateUser_Returns_OkResult()
        {
            //Arrange
            var createUser = new CreateApplicationUserRequestDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                PhoneNumber = "1234567890",
                GuestUserComment = "test",
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateApplicationUserCommand>(), default))
               .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

            // Act
            var result = await _adminController.CreateUser(createUser);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
