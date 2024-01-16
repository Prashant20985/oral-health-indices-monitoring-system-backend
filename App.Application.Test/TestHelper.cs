using App.Application.Interfaces;
using App.Domain.Models.Users;
using App.Domain.Repository;
using App.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace App.Application.Test;

/// <summary>
/// Helper class to set up common test dependencies and provide convenient mock instances for test classes.
/// </summary>
public class TestHelper
{
    /// <summary>
    /// Mock instance for the ITokenService interface, used to simulate token service behavior in tests.
    /// </summary>
    protected Mock<ITokenService> tokenServiceMock;

    /// <summary>
    /// Mock instance for the IUserRepository interface, used to simulate user repository behavior in tests.
    /// </summary>
    protected Mock<IUserRepository> userRepositoryMock;

    /// <summary>
    /// Mock instance for the IHttpContextAccessorService interface, used to simulate HTTP context behavior in tests.
    /// </summary>
    protected Mock<IHttpContextAccessorService> httpContextAccessorServiceMock;

    /// <summary>
    /// PasswordHasher instance for hashing user passwords, used in tests that involve password hashing.
    /// </summary>
    protected PasswordHasher<ApplicationUser> passwordHasher;

    /// <summary>
    /// Mock instance for the IMediator interface, used to simulate MediatR behavior in tests.
    /// </summary>
    protected Mock<IMediator> mediatorMock;

    /// <summary>
    /// Mock instance for the IEmailService interface, used to simulate email service behavior in tests.
    /// </summary>
    protected Mock<IEmailService> emailServiceMock;

    /// <summary>
    /// Mock instance for the IUserContextUnitOfWork interface, used to simulate UserContextUnitOfWork service behavior in tests.
    /// </summary>
    protected Mock<IUnitOfWork> unitOfWork;

    /// <summary>
    /// Mock instance for the IQueryFilter interface, used to simulate Query Filter service behavior in tests.
    /// </summary>
    protected Mock<IQueryFilter> queryFilterMock;

    /// <summary>
    /// Mock instance for the IGroupRepository interface, used to simulate groupRepository service behavior in tests.
    /// </summary>
    protected Mock<IGroupRepository> groupRepositoryMock;

    /// <summary>
    /// Mock instance for the IUserRequestRepository interface, used to simulate userRequestRepository service behavior in tests.
    /// </summary>
    protected Mock<IUserRequestRepository> userRequestRepositoryMock;

    /// <summary>
    /// Mock instance for the IPatientRepository interface, used to simulate patientRepository service behavior in tests.
    /// </summary>
    protected Mock<IPatientRepository> patientRepositoryMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestHelper"/> class with default mocked dependencies.
    /// </summary>
    public TestHelper()
    {
        tokenServiceMock = new Mock<ITokenService>();
        userRepositoryMock = new Mock<IUserRepository>();
        httpContextAccessorServiceMock = new Mock<IHttpContextAccessorService>();
        passwordHasher = new PasswordHasher<ApplicationUser>();
        mediatorMock = new Mock<IMediator>();
        emailServiceMock = new Mock<IEmailService>();
        unitOfWork = new Mock<IUnitOfWork>();
        queryFilterMock = new Mock<IQueryFilter>();
        groupRepositoryMock = new Mock<IGroupRepository>();
        userRequestRepositoryMock = new Mock<IUserRequestRepository>();
        patientRepositoryMock = new Mock<IPatientRepository>();
    }
}

