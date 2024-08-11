using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Models.Users;
using App.Persistence.Contexts;
using App.Persistence.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace App.Persistence.Test.Repository;

public class GroupRepositoryTests
{
    private readonly Mock<OralEhrContext> _mockOralEhrContext;
    private readonly GroupRepository _groupRepository;

    public GroupRepositoryTests()
    {
        _mockOralEhrContext = new Mock<OralEhrContext>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, StudentResponseDto>()
                .ForMember(x => x.Groups, o => o.MapFrom(s => s.StudentGroups
                .Select(x => x.Group.GroupName).ToList()));
        });
        var mapper = mapperConfig.CreateMapper();
        _groupRepository = new GroupRepository(_mockOralEhrContext.Object, mapper);
    }

    [Fact]
    public async Task GetGroupById_ShouldReturnGroup_WhenGroupExists()
    {
        // Arrange
        var group1 = new Group(Guid.NewGuid().ToString(), "Group 1");
        var group2 = new Group(Guid.NewGuid().ToString(), "Group 2");
        var groups = new List<Group> { group1, group2 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);

        // Act
        var result = await _groupRepository.GetGroupById(group1.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(group1.Id, result.Id);
        Assert.Equal("Group 1", result.GroupName);
        Assert.Equal(group1.TeacherId, result.TeacherId);
    }

    [Fact]
    public async Task GetGroupById_ShouldReturnNull_WhenGroupDoesntExists()
    {
        // Arrange
        var group1 = new Group(Guid.NewGuid().ToString(), "Group 1");
        var group2 = new Group(Guid.NewGuid().ToString(), "Group 2");
        var groups = new List<Group> { group1, group2 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);

        // Act
        var result = await _groupRepository.GetGroupById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetGroupByName_ShouldReturnGroup_WhenGroupExists()
    {
        // Arrange
        var group1 = new Group(Guid.NewGuid().ToString(), "Group 1");
        var group2 = new Group(Guid.NewGuid().ToString(), "Group 2");
        var groups = new List<Group> { group1, group2 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);

        // Act
        var result = await _groupRepository.GetGroupByName(group1.GroupName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(group1.Id, result.Id);
        Assert.Equal("Group 1", result.GroupName);
        Assert.Equal(group1.TeacherId, result.TeacherId);
    }

    [Fact]
    public async Task GetGroupByName_ShouldReturnNull_WhenGroupDoesntExists()
    {
        // Arrange
        var group1 = new Group(Guid.NewGuid().ToString(), "Group 1");
        var group2 = new Group(Guid.NewGuid().ToString(), "Group 2");
        var groups = new List<Group> { group1, group2 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);

        // Act
        var result = await _groupRepository.GetGroupByName("Group 3");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateGroup_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var group = new Group(Guid.NewGuid().ToString(), "Group");
        var groups = new List<Group> { }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);

        // Act
        await _groupRepository.CreateGroup(group);

        // Assert
        _mockOralEhrContext.Verify(x => x.Groups.AddAsync(group, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeleteGroup_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var group = new Group(Guid.NewGuid().ToString(), "Group");
        var groups = new List<Group> { group }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);

        // Act
        await _groupRepository.DeleteGroup(group.Id);

        // Assert
        _mockOralEhrContext.Verify(x => x.Groups.Remove(group), Times.Once);
    }

    [Fact]
    public async Task GetStudentGroup_ShouldReturnStudentGroup_WhenStudentGroupExists()
    {
        // Arrange
        var studentGroup1 = new StudentGroup(Guid.NewGuid(), Guid.NewGuid().ToString());
        var studentGroup2 = new StudentGroup(Guid.NewGuid(), Guid.NewGuid().ToString());

        var studentGroups = new List<StudentGroup> { studentGroup1, studentGroup2 }
        .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);

        // Act
        var result = await _groupRepository.GetStudentGroup(studentGroup1.StudentId, studentGroup1.GroupId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(studentGroup1.StudentId, result.StudentId);
        Assert.Equal(studentGroup1.GroupId, result.GroupId);
    }

    [Fact]
    public async Task GetStudentGroup_ShouldReturnNull_WhenStudentGroupDoesntExists()
    {
        // Arrange
        var studentGroup1 = new StudentGroup(Guid.NewGuid(), Guid.NewGuid().ToString());
        var studentGroup2 = new StudentGroup(Guid.NewGuid(), Guid.NewGuid().ToString());

        var studentGroups = new List<StudentGroup> { studentGroup1, studentGroup2 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);

        // Act
        var result = await _groupRepository.GetStudentGroup("studentId", Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddStudentToGroup_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        var studentGroup = new StudentGroup(Guid.NewGuid(), Guid.NewGuid().ToString());
        var studentGroups = new List<StudentGroup> { }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);

        // Act
        await _groupRepository.AddStudentToGroup(studentGroup);

        // Assert
        _mockOralEhrContext.Verify(x => x.StudentGroups.AddAsync(studentGroup, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void RemoveStudentFromGroup_StateUnderTest_ExpectedBehavior()
    {

        // Arrange
        var studentGroup = new StudentGroup(Guid.NewGuid(), Guid.NewGuid().ToString());
        var studentGroups = new List<StudentGroup> { studentGroup }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(studentGroups.Object);

        // Act
        _groupRepository.RemoveStudentFromGroup(studentGroup);

        // Assert
        _mockOralEhrContext.Verify(x => x.StudentGroups.Remove(studentGroup), Times.Once);
    }

    [Fact]
    public async Task GetAllStudentsNotInGroup_ShouldReturnListOfStudentDto()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var applicationUserRoles = new List<ApplicationUserRole>
        {
            new ApplicationUserRole { ApplicationRole =  new ApplicationRole { Name = "Student"}}
        };

        var student1 = new ApplicationUser("test@test.com", "Test", "User", "7418552", "comment")
        { ApplicationUserRoles = applicationUserRoles };

        student1.StudentGroups.Add(new StudentGroup(groupId, student1.Id.ToString()));

        var student2 = new ApplicationUser("test2@test.com", "Test2", "User", "85277441", "comment")
        { ApplicationUserRoles = applicationUserRoles };

        var users = new List<ApplicationUser> { student1, student2 };

        var mockUsers = users.AsQueryable().BuildMockDbSet();
        var mockApplicationUserRoles = applicationUserRoles.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Users).Returns(mockUsers.Object);
        _mockOralEhrContext.Setup(x => x.ApplicationUserRoles).Returns(mockApplicationUserRoles.Object);

        // Act
        var result = _groupRepository.GetAllStudentsNotInGroup(groupId);

        var resultList = await result.ToListAsync();

        // Assert
        Assert.IsAssignableFrom<IQueryable<StudentResponseDto>>(result);
        Assert.Single(resultList);
        Assert.Equal("test2", resultList[0].UserName);
        Assert.Equal("Test2", resultList[0].FirstName);
        Assert.Equal("User", resultList[0].LastName);
    }

    [Fact]
    public async Task GetAllGroupsCreatedByTeacher_ShouldReturnListOfGroups()
    {
        // Arrange
        var teacherId = Guid.NewGuid().ToString();
        var group1 = new Group(teacherId, "Group 1");
        var group2 = new Group(teacherId, "Group 2");

        var groups = new List<Group> { group1, group2 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Groups).Returns(groups.Object);

        // Act
        var result = await _groupRepository.GetAllGroupsCreatedByTeacher(teacherId);

        // Assert
        Assert.IsType<List<Group>>(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Group 1", result[0].GroupName);
        Assert.Equal("Group 2", result[1].GroupName);
    }

    [Fact]
    public async Task GetAllGroupsWithStudentsList_ShouldReturnListOfGroupDtos()
    {
        // Arrange
        var teacherId = Guid.NewGuid().ToString();
        var group = new Group(teacherId, "Group 1");

        var student1 = new ApplicationUser("test@test.com", "Test", "User", "7418552", "comment");
        var student2 = new ApplicationUser("test2@test.com", "Test2", "User", "85277441", "comment");

        var studentGroup1 = new StudentGroup(group.Id, student1.Id.ToString());
        var studentGroup2 = new StudentGroup(group.Id, student2.Id.ToString());

        studentGroup1.Student = student1;
        studentGroup2.Student = student2;

        group.StudentGroups.Add(studentGroup1);
        group.StudentGroups.Add(studentGroup2);

        var studentGroups = new List<StudentGroup> { studentGroup1, studentGroup2 };
        var groups = new List<Group> { group };

        var mockStudentGroups = studentGroups.AsQueryable().BuildMockDbSet();
        var mockGroups = groups.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.StudentGroups).Returns(mockStudentGroups.Object);
        _mockOralEhrContext.Setup(x => x.Groups).Returns(mockGroups.Object);

        // Act
        var result = await _groupRepository.GetAllGroupsWithStudentsList(teacherId);

        // Assert
        Assert.IsType<List<StudentGroupResponseDto>>(result);
        Assert.Single(result);
        Assert.Equal("Group 1", result[0].GroupName);
        Assert.Equal(2, result[0].Students.Count);
        Assert.Equal("test", result[0].Students[0].UserName);
        Assert.Equal("test2", result[0].Students[1].UserName);
    }
}
