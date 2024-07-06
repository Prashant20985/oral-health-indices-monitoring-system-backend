using App.Domain.DTOs.ResearchGroupDtos.Response;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using App.Persistence.Contexts;
using App.Persistence.Repository;
using AutoMapper;
using MockQueryable.Moq;
using Moq;

namespace App.Persistence.Test.Repository;

public class ResearchGroupRepositoryTests
{
    private readonly Mock<OralEhrContext> _mockOralEhrContext;
    private readonly ResearchGroupRepository _researchGroupRepository;

    public ResearchGroupRepositoryTests()
    {
        _mockOralEhrContext = new Mock<OralEhrContext>();
        var mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<ResearchGroup, ResearchGroupResponseDto>();
                cfg.CreateMap<Patient, ResearchGroupPatientResponseDto>();
            }
         );
        var mapper = mapperConfig.CreateMapper();
        _researchGroupRepository = new ResearchGroupRepository(_mockOralEhrContext.Object, mapper);
    }

    [Fact]
    public void GetAllResearchGroups_WhenCalled_ReturnsAllResearchGroups()
    {
        // Arrange
        var researchGroup = new ResearchGroup("TestGroup", "TestDescription", "TestDoctorId");
        var researchGroup1 = new ResearchGroup("TestGroup1", "TestDescription1", "TestDoctorId1");

        var researchGroups = new List<ResearchGroup>
        {
            researchGroup,
            researchGroup1
        };

        var mockResearchGroups = researchGroups.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(mockResearchGroups.Object);

        // Act
        var result = _researchGroupRepository.GetAllResearchGroups();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(researchGroups.Count, result.Count());
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetAllResearchGroups_WhenCalled_ReturnsEmptyList()
    {
        // Arrange
        var researchGroups = new List<ResearchGroup>();

        var mockResearchGroups = researchGroups.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(mockResearchGroups.Object);

        // Act
        var result = _researchGroupRepository.GetAllResearchGroups();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetAllPatientsNotInAnyResearchGroup_WhenCalled_ReturnsAllPatientsNotInAnyResearchGroup()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test", "test", "test", 1, "test");
        var patient2 = new Patient("test1", "test1", "test1@test.com", Gender.Male, "test1", "test1", 19, "test1", "test1", "test1", "test1", 1, "test1");
        var patient3 = new Patient("test2", "test2", "test2@test.com", Gender.Male, "test2", "test2", 19, "test2", "test2", "test2", "test2", 1, "test2");

        patient1.ResearchGroupId = Guid.Empty;
        patient2.ResearchGroupId = Guid.Empty;
        patient3.ResearchGroupId = Guid.NewGuid();

        var patients = new List<Patient>
        {
            patient1,
            patient2,
            patient3
        };

        var mockPatients = patients.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients)
            .Returns(mockPatients.Object);

        // Act
        var result = _researchGroupRepository.GetAllPatientsNotInAnyResearchGroup();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("test1", result.First().FirstName);
        Assert.Equal("test", result.Last().FirstName);
    }

    [Fact]
    public void GetAllPatientsNotInAnyResearchGroup_WhenCalled_ReturnsEmptyList()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test", "test", "test", 1, "test");
        var patient2 = new Patient("test1", "test1", "test1@test.com", Gender.Male, "test1", "test1", 19, "test1", "test1", "test1", "test1", 1, "test1");
        var patient3 = new Patient("test2", "test2", "test2@test.com", Gender.Male, "test2", "test2", 19, "test2", "test2", "test2", "test2", 1, "test2");

        patient1.ResearchGroupId = Guid.NewGuid();
        patient2.ResearchGroupId = Guid.NewGuid();
        patient3.ResearchGroupId = Guid.NewGuid();

        var patients = new List<Patient>
        {
            patient1,
            patient2,
            patient3
        };

        var mockPatients = patients.AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients)
            .Returns(mockPatients.Object);

        // Act
        var result = _researchGroupRepository.GetAllPatientsNotInAnyResearchGroup();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetResearchGroupById_WhenCalled_ReturnsResearchGroup()
    {
        // Arrange
        var researchGroup = new ResearchGroup("TestGroup", "TestDescription", "TestDoctorId");
        var researchGroup1 = new ResearchGroup("TestGroup1", "TestDescription1", "TestDoctorId1");
        var researchGroup2 = new ResearchGroup("TestGroup2", "TestDescription2", "TestDoctorId2");

        var mockResearchGroups = new List<ResearchGroup>
        {
            researchGroup,
            researchGroup1,
            researchGroup2
        }.AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(mockResearchGroups.Object);

        // Act
        var result = await _researchGroupRepository.GetResearchGroupById(researchGroup.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(researchGroup.Id, result.Id);
        Assert.Equal(researchGroup.GroupName, result.GroupName);
        Assert.Equal(researchGroup.Description, result.Description);
    }

    [Fact]
    public async Task GetResearchGroupById_WhenCalled_ReturnsNull()
    {
        // Arrange
        var researchGroup = new ResearchGroup("TestGroup", "TestDescription", "TestDoctorId");
        var researchGroup1 = new ResearchGroup("TestGroup1", "TestDescription1", "TestDoctorId1");
        var researchGroup2 = new ResearchGroup("TestGroup2", "TestDescription2", "TestDoctorId2");

        var mockResearchGroups = new List<ResearchGroup>
        {
            researchGroup,
            researchGroup1,
            researchGroup2
        }.AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(mockResearchGroups.Object);

        // Act
        var result = await _researchGroupRepository.GetResearchGroupById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetResearchGroupByName_WhenCalled_ReturnsResearchGroup()
    {
        // Arrange
        var researchGroup = new ResearchGroup("TestGroup", "TestDescription", "TestDoctorId");
        var researchGroup1 = new ResearchGroup("TestGroup1", "TestDescription1", "TestDoctorId1");
        var researchGroup2 = new ResearchGroup("TestGroup2", "TestDescription2", "TestDoctorId2");

        var mockResearchGroups = new List<ResearchGroup>
        {
            researchGroup,
            researchGroup1,
            researchGroup2
        }.AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(mockResearchGroups.Object);

        // Act
        var result = await _researchGroupRepository.GetResearchGroupByName(researchGroup.GroupName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(researchGroup.Id, result.Id);
        Assert.Equal(researchGroup.GroupName, result.GroupName);
        Assert.Equal(researchGroup.Description, result.Description);
    }

    [Fact]
    public async Task GetResearchGroupByName_WhenCalled_ReturnsNull()
    {
        // Arrange
        var researchGroup = new ResearchGroup("TestGroup", "TestDescription", "TestDoctorId");
        var researchGroup1 = new ResearchGroup("TestGroup1", "TestDescription1", "TestDoctorId1");
        var researchGroup2 = new ResearchGroup("TestGroup2", "TestDescription2", "TestDoctorId2");

        var mockResearchGroups = new List<ResearchGroup>
        {
            researchGroup,
            researchGroup1,
            researchGroup2
        }.AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(mockResearchGroups.Object);

        // Act
        var result = await _researchGroupRepository.GetResearchGroupByName("TestGroup3");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetResearchGroupDetailsById_WhenCalled_ReturnsResearchGroup()
    {
        // Arrange
        var researchGroup = new ResearchGroup("TestGroup", "TestDescription", "TestDoctorId");
        var researchGroup1 = new ResearchGroup("TestGroup1", "TestDescription1", "TestDoctorId1");
        var researchGroup2 = new ResearchGroup("TestGroup2", "TestDescription2", "TestDoctorId2");

        var mockResearchGroups = new List<ResearchGroup>
        {
            researchGroup,
            researchGroup1,
            researchGroup2
        }.AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(mockResearchGroups.Object);

        // Act
        var result = await _researchGroupRepository.GetResearchGroupDetailsById(researchGroup.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(researchGroup.Id, result.Id);
        Assert.Equal(researchGroup.GroupName, result.GroupName);
        Assert.Equal(researchGroup.Description, result.Description);
    }

    [Fact]
    public async Task GetResearchGroupDetailsById_WhenCalled_ReturnsNull()
    {
        // Arrange
        var researchGroup = new ResearchGroup("TestGroup", "TestDescription", "TestDoctorId");
        var researchGroup1 = new ResearchGroup("TestGroup1", "TestDescription1", "TestDoctorId1");
        var researchGroup2 = new ResearchGroup("TestGroup2", "TestDescription2", "TestDoctorId2");

        var mockResearchGroups = new List<ResearchGroup>
        {
            researchGroup,
            researchGroup1,
            researchGroup2
        }.AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(mockResearchGroups.Object);

        // Act
        var result = await _researchGroupRepository.GetResearchGroupDetailsById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateResearchGroup_WhenCalled_AddsResearchGroup()
    {
        // Arrange
        var researchGroup = new ResearchGroup("TestGroup", "TestDescription", "TestDoctorId");

        var researchGroups = new List<ResearchGroup>
        {
            researchGroup
        }.AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(researchGroups.Object);

        // Act
        await _researchGroupRepository.CreateResearchGroup(researchGroup);

        // Assert
        _mockOralEhrContext.Verify(x => x.ResearchGroups.AddAsync(researchGroup, default), Times.Once);
    }

    [Fact]
    public void DeleteResearchGroup_WhenCalled_DeletesResearchGroup()
    {
        // Arrange
        var researchGroup = new ResearchGroup("TestGroup", "TestDescription", "TestDoctorId");

        var researchGroups = new List<ResearchGroup>
        {
            researchGroup
        }.AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.ResearchGroups)
            .Returns(researchGroups.Object);

        // Act
        _researchGroupRepository.DeleteResearchGroup(researchGroup);

        // Assert
        _mockOralEhrContext.Verify(x => x.ResearchGroups.Remove(researchGroup), Times.Once);
    }
}
