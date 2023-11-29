using App.Application.AdminOperations.Command.UpdateApplicationUser;
using App.Application.Core;
using App.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AdminControllerTests
{
    public class UpdateUserTest
    {
        private readonly TestableAdminController _adminController;
        private readonly Mock<IMediator> _mediatorMock;

        public UpdateUserTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _adminController = new TestableAdminController();
            _adminController.ExposeSetMediator(_mediatorMock.Object);
        }

        [Fact]
        public async Task UpdateUser_Returns_OkResult()
        {
            // Arrange
            var userName = "testuser";
            var updateApplicationUserDto = new UpdateApplicationUserDto
            {
                FirstName = "Updated",
                LastName = "User",
                Role = "Admin",
                PhoneNumber = "987654321",
                GuestUserComment = "test"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateApplicationUserCommand>(), default))
                .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

            // Act
            var result = await _adminController.UpdateUser(userName, updateApplicationUserDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}