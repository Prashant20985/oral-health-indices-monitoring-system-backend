using App.Application.AdminOperations.Query.UserDetails;
using App.Application.Core;
using App.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AdminControllerTests
{
    public class GetUserDetailsTest
    {
        private readonly TestableAdminController _adminController;
        private readonly Mock<IMediator> _mediatorMock;

        public GetUserDetailsTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _adminController = new TestableAdminController();
            _adminController.ExposeSetMediator(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetUserDetails_Returns_OkResult()
        {
            //Arrange
            var user = new ApplicationUserDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                UserType = "RegularUser",
                Role = "Admin",
                UserName = "test",
                PhoneNumber = "123456789",
                IsAccountActive = true
            };

            var userName = "test";

            _mediatorMock.Setup(m => m.Send(It.IsAny<FetchUserDetailsQuery>(), default))
                .ReturnsAsync(OperationResult<ApplicationUserDto>.Success(user));

            // Act
            var result = await _adminController.GetUserDetails(userName);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ApplicationUserDto>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsType<ApplicationUserDto>(okObjectResult.Value);

            Assert.Equal(user.FirstName, model.FirstName);
            Assert.Equal(user.LastName, model.LastName);
            Assert.Equal(user.Email, model.Email);
            Assert.Equal(user.UserType, model.UserType);
            Assert.Equal(user.Role, model.Role);
            Assert.Equal(user.UserName, model.UserName);
            Assert.Equal(user.IsAccountActive, model.IsAccountActive);
            Assert.Equal(user.PhoneNumber, model.PhoneNumber);
        }
    }
}
