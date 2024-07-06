using App.Application.AdminOperations.Query.DeletedApplicationUsersList;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AdminControllerTests
{
    public class GetDeletedUsersTest
    {
        private readonly TestableAdminController _adminController;
        private readonly Mock<IMediator> _mediatorMock;

        public GetDeletedUsersTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _adminController = new TestableAdminController();
            _adminController.ExposeSetMediator(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetDeletedUsers_Returns_OkResult()
        {
            //Arrange
            var deletedUsers = new List<ApplicationUserResponseDto>()
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
                DeletedAt = DateTime.Now,
                DeleteUserComment = "Testing"
            },
            new ApplicationUserResponseDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test2@test.com",
                UserType = "RegularUser",
                Role = "Admin",
                UserName = "test2",
                IsAccountActive = false,
                DeletedAt = DateTime.Now,
                DeleteUserComment = "Testing"
            }
        };

            var searchParams = new SearchParams();

            _mediatorMock.Setup(m => m.Send(It.IsAny<FetchDeletedApplicationUsersListQuery>(), default))
                .ReturnsAsync(OperationResult<List<ApplicationUserResponseDto>>.Success(deletedUsers));

            // Act
            var result = await _adminController.GetDeletedUsers(searchParams);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<ApplicationUserResponseDto>>>(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsType<List<ApplicationUserResponseDto>>(okObjectResult.Value);
            Assert.Equal(deletedUsers.Count, model.Count);
        }
    }
}
