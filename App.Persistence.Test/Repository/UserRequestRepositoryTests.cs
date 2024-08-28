using App.Domain.DTOs.UserRequestDtos.Response;
using App.Domain.Models.Enums;
using App.Domain.Models.Users;
using App.Persistence.Contexts;
using App.Persistence.Repository;
using AutoMapper;
using MockQueryable.Moq;
using Moq;

namespace App.Persistence.Test.Repository;

public class UserRequestRepositoryTests
{
    private readonly Mock<OralEhrContext> _mockOralEhrContext;
    private readonly UserRequestRepository _userRequestRepository;

    public UserRequestRepositoryTests()
    {
        _mockOralEhrContext = new Mock<OralEhrContext>();
        var mapper = new MapperConfiguration(cfg =>
            cfg.CreateMap<UserRequest, UserRequestResponseDto>()
                .ForMember(x => x.UserName, o => o.MapFrom(s => s.ApplicationUser.UserName))).CreateMapper();
        _userRequestRepository = new UserRequestRepository(mapper, _mockOralEhrContext.Object);
    }

    [Fact]
    public async Task CreateRequest_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var request = new UserRequest("CreatedById", "Request Title", "Request Description");
        var requests = new List<UserRequest>().AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(requests.Object);

        // Act
        await _userRequestRepository.CreateRequest(request);

        // Assert
        _mockOralEhrContext.Verify(x => x.UserRequests.AddAsync(request, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void DeleteRequest_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var request = new UserRequest("CreatedById", "Request Title", "Request Description");
        var requests = new List<UserRequest> { request }.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(requests.Object);

        // Act
        _userRequestRepository.DeleteRequest(request);

        // Assert
        _mockOralEhrContext.Verify(x => x.UserRequests.Remove(request), Times.Once);
    }

    [Fact]
    public void GetAllRequestsByStatus_ShouldReturnQueryable_SubmittedRequests()
    {
        // Arrange
        var applicationUser = new ApplicationUser("test@test.com", "FirstName", "LastName", "7418529", null);
        var request1 = new UserRequest("CreatedById1", "Request Title1", "Request Description")
            { ApplicationUser = applicationUser };
        var request2 = new UserRequest("CreatedById2", "Request Title2", "Request Description")
            { ApplicationUser = applicationUser };

        var requests = new List<UserRequest> { request1, request2 }.AsQueryable();

        var mockDbSet = requests.BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(mockDbSet.Object);

        // Act
        var result = _userRequestRepository.GetAllRequestsByStatus(RequestStatus.Submitted);
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<UserRequestResponseDto>>(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Submitted", result.ToList()[0].RequestStatus);
        Assert.Equal("Submitted", result.ToList()[1].RequestStatus);
    }

    [Fact]
    public void GetAllRequestsByStatus_ShouldReturnQueryable_InProgressRequests()
    {
        // Arrange
        var applicationUser = new ApplicationUser("test@test.com", "FirstName", "LastName", "7418529", null);
        var request1 = new UserRequest("CreatedById1", "Request Title1", "Request Description")
            { ApplicationUser = applicationUser };
        var request2 = new UserRequest("CreatedById2", "Request Title2", "Request Description")
            { ApplicationUser = applicationUser };
        request1.SetRequestToInProgress();

        var requests = new List<UserRequest> { request1, request2 }.AsQueryable();

        var mockDbSet = requests.BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(mockDbSet.Object);

        // Act
        var result = _userRequestRepository.GetAllRequestsByStatus(RequestStatus.In_Progress);
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<UserRequestResponseDto>>(result);
        Assert.Equal(1, result.Count());
        Assert.Equal("In_Progress", result.ToList()[0].RequestStatus);
    }

    [Fact]
    public void GetAllRequestsByStatus_ShouldReturnQueryable_CpmpletedRequests()
    {
        // Arrange
        var applicationUser = new ApplicationUser("test@test.com", "FirstName", "LastName", "7418529", null);
        var request1 = new UserRequest("CreatedById1", "Request Title1", "Request Description")
            { ApplicationUser = applicationUser };
        var request2 = new UserRequest("CreatedById2", "Request Title2", "Request Description")
            { ApplicationUser = applicationUser };
        request1.SetRequestToCompleted("Completed");

        var requests = new List<UserRequest> { request1, request2 }.AsQueryable();

        var mockDbSet = requests.BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(mockDbSet.Object);

        // Act
        var result = _userRequestRepository.GetAllRequestsByStatus(RequestStatus.Completed);
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<UserRequestResponseDto>>(result);
        Assert.Equal(1, result.Count());
        Assert.Equal("Completed", result.ToList()[0].RequestStatus);
        Assert.Equal("Completed", result.ToList()[0].AdminComment);
    }

    [Fact]
    public void GetRequestsByUserIdAndStatus_ShouldReturnQueryable_SubmittedRequests()
    {
        // Arrange
        var applicationUser = new ApplicationUser("test@test.com", "FirstName", "LastName", "7418529", null);
        var request1 = new UserRequest(applicationUser.Id, "Request Title1", "Request Description")
            { ApplicationUser = applicationUser };
        var request2 = new UserRequest(applicationUser.Id, "Request Title2", "Request Description")
            { ApplicationUser = applicationUser };

        var requests = new List<UserRequest> { request1, request2 }.AsQueryable();

        var mockuserRequestDbSet = requests.BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(mockuserRequestDbSet.Object);

        // Act
        var result = _userRequestRepository.GetRequestsByUserIdAndStatus(applicationUser.Id, RequestStatus.Submitted);
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<UserRequestResponseDto>>(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Submitted", result.ToList()[0].RequestStatus);
        Assert.Equal("Submitted", result.ToList()[1].RequestStatus);
        Assert.Equal(applicationUser.UserName, result.ToList()[0].UserName);
        Assert.Equal(applicationUser.UserName, result.ToList()[1].UserName);
    }

    [Fact]
    public void GetRequestsByUserIdAndStatus_ShouldReturnQueryable_InProgressRequests()
    {
        // Arrange
        var applicationUser = new ApplicationUser("test@test.com", "FirstName", "LastName", "7418529", null);
        var request1 = new UserRequest(applicationUser.Id, "Request Title1", "Request Description")
            { ApplicationUser = applicationUser };
        var request2 = new UserRequest(applicationUser.Id, "Request Title2", "Request Description")
            { ApplicationUser = applicationUser };
        request1.SetRequestToInProgress();

        var requests = new List<UserRequest> { request1, request2 }.AsQueryable();

        var mockuserRequestDbSet = requests.BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(mockuserRequestDbSet.Object);

        // Act
        var result = _userRequestRepository.GetRequestsByUserIdAndStatus(applicationUser.Id, RequestStatus.In_Progress);
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<UserRequestResponseDto>>(result);
        Assert.Equal(1, result.Count());
        Assert.Equal("In_Progress", result.ToList()[0].RequestStatus);
        Assert.Equal(applicationUser.UserName, result.ToList()[0].UserName);
    }

    [Fact]
    public void GetRequestsByUserIdAndStatus_ShouldReturnQueryable_CompletedRequests()
    {
        // Arrange
        var applicationUser = new ApplicationUser("test@test.com", "FirstName", "LastName", "7418529", null);
        var request1 = new UserRequest(applicationUser.Id, "Request Title1", "Request Description")
            { ApplicationUser = applicationUser };
        var request2 = new UserRequest(applicationUser.Id, "Request Title2", "Request Description")
            { ApplicationUser = applicationUser };
        request1.SetRequestToCompleted("Completed");

        var requests = new List<UserRequest> { request1, request2 }.AsQueryable();

        var mockuserRequestDbSet = requests.BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(mockuserRequestDbSet.Object);

        // Act
        var result = _userRequestRepository.GetRequestsByUserIdAndStatus(applicationUser.Id, RequestStatus.Completed);
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<UserRequestResponseDto>>(result);
        Assert.Equal(1, result.Count());
        Assert.Equal("Completed", result.ToList()[0].RequestStatus);
        Assert.Equal("Completed", result.ToList()[0].AdminComment);
        Assert.Equal(applicationUser.UserName, result.ToList()[0].UserName);
    }

    [Fact]
    public async Task GetUserRequestById_ShouldReturnUserRequest_WhenUserRequestExists()
    {
        // Arrange
        var request = new UserRequest("CreatedById", "Request Title", "Request Description");

        var requests = new List<UserRequest> { request }.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(requests.Object);

        // Act
        var result = await _userRequestRepository.GetUserRequestById(request.Id);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<UserRequest>(result);
        Assert.Equal(request.Id, result.Id);
        Assert.Equal(request.CreatedBy, result.CreatedBy);
        Assert.Equal(request.RequestTitle, result.RequestTitle);
        Assert.Equal(request.Description, result.Description);
        Assert.Equal(RequestStatus.Submitted, result.RequestStatus);
    }

    [Fact]
    public async Task GetUserRequestById_ShouldReturnNull_WhenUserRequestDoesntExists()
    {
        // Arrange
        var requests = new List<UserRequest>().AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.UserRequests).Returns(requests.Object);

        // Act
        var result = await _userRequestRepository.GetUserRequestById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}