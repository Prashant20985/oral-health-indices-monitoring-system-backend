using App.Application.AdminOperations.Command.DeleteApplicationUser;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AdminControllerTests
{
    public class DeleteUserTest
    {
        private readonly TestableAdminController _adminController;
        private readonly Mock<IMediator> _mediatorMock;

        public DeleteUserTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _adminController = new TestableAdminController();
            _adminController.ExposeSetMediator(_mediatorMock.Object);
        }

        [Fact]
        public async Task DeleteUser_Returns_OkResult()
        {
            //Arrange
            var user = new ApplicationUserResponseDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                UserType = "RegularUser",
                Role = "Admin",
                UserName = "test",
                PhoneNumber = "123456789",
                IsAccountActive = false,
                DeleteUserComment = "test"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteApplicationUserCommand>(), default))
                .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

            // Act
            var result = await _adminController.DeleteUser(user.UserName, user.DeleteUserComment);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
