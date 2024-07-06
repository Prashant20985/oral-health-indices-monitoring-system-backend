using App.Application.AdminOperations.Query.DeactivatedApplicationUsersList;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AdminControllerTests
{
    public class GetDeactivatedUsersTest
    {
        private readonly TestableAdminController _adminController;
        private readonly Mock<IMediator> _mediatorMock;

        public GetDeactivatedUsersTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _adminController = new TestableAdminController();
            _adminController.ExposeSetMediator(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetDeactivatedUsers_Returns_OkResult()
        {
            //Arrange
            var deactivatedUsers = new List<ApplicationUserResponseDto>()
        {
            new ApplicationUserResponseDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                UserType = "RegularUser",
                Role = "Admin",
                UserName = "test",
                IsAccountActive = false,
            },
            new ApplicationUserResponseDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test2@test.com",
                UserType = "RegularUser",
                Role = "Admin",
                UserName = "test2",
                IsAccountActive = false
            }
        };

            var searchParams = new SearchParams();

            _mediatorMock.Setup(m => m.Send(It.IsAny<FetchDeactivatedApplicationUsersListQuery>(), default))
                .ReturnsAsync(OperationResult<List<ApplicationUserResponseDto>>.Success(deactivatedUsers));

            // Act
            var result = await _adminController.GetDeactivatedUsers(searchParams);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<ApplicationUserResponseDto>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsType<List<ApplicationUserResponseDto>>(okObjectResult.Value);
            Assert.Equal(deactivatedUsers.Count, model.Count);
        }

    }
}
