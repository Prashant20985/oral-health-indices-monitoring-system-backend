using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using App.Persistence.Contexts;
using App.Persistence.Repository;
using AutoMapper;
using MockQueryable.Moq;
using Moq;

namespace App.Persistence.Test.Repository;

public class PatientRepositoryTests
{
    private readonly Mock<OralEhrContext> _mockOralEhrContext;
    private readonly PatientRepository _patientRepository;

    public PatientRepositoryTests()
    {
        _mockOralEhrContext = new Mock<OralEhrContext>();
        var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<Patient, PatientResponseDto>(); });
        var mapper = mapperConfig.CreateMapper();
        _patientRepository = new PatientRepository(_mockOralEhrContext.Object, mapper);
    }

    [Fact]
    public async Task GetPatientById_WhenPatientExists_ShouldReturnPatient()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");
        var patient2 = new Patient("test1", "test1", "test1@test.com", Gender.Male, "test1", "test1", 19, "test1",
            "test1", "test1", "test1", 1, "test1");

        var patients = new List<Patient> { patient1, patient2 }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = await _patientRepository.GetPatientById(patient1.Id);

        //Assert
        Assert.Equal(patient1, result);
        Assert.Equal(patient1.Id, result.Id);
        Assert.Equal(patient1.FirstName, result.FirstName);
        Assert.Equal(patient1.LastName, result.LastName);
        Assert.Equal(patient1.Email, result.Email);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetPatientById_WhenPatientDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");
        var patient2 = new Patient("test1", "test1", "test1@test.com", Gender.Male, "test1", "test1", 19, "test1",
            "test1", "test1", "test1", 1, "test1");

        var patients = new List<Patient> { patient1, patient2 }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = await _patientRepository.GetPatientById(Guid.NewGuid());

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPatientByEmail_WhenPatientExists_ShouldReturnPatient()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");
        var patient2 = new Patient("test1", "test1", "test1@test.com", Gender.Male, "test1", "test1", 19, "test1",
            "test1", "test1", "test1", 1, "test1");

        var patients = new List<Patient> { patient1, patient2 }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = await _patientRepository.GetPatientByEmail(patient1.Email);

        //Assert
        Assert.Equal(patient1, result);
        Assert.Equal(patient1.Id, result.Id);
        Assert.Equal(patient1.FirstName, result.FirstName);
        Assert.Equal(patient1.LastName, result.LastName);
    }

    [Fact]
    public async Task GetPatientByEmail_WhenPatientDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");
        var patient2 = new Patient("test1", "test1", "test1@test.com", Gender.Male, "test1", "test1", 19, "test1",
            "test1", "test1", "test1", 1, "test1");

        var patients = new List<Patient> { patient1, patient2 }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = await _patientRepository.GetPatientByEmail("test123@test.com");

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllActivePatients_WhenPatientsExist_ShouldReturnAllActivePatients()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");
        var patient2 = new Patient("test1", "test1", "test1@test.com", Gender.Male, "test1", "test1", 19, "test1",
            "test1", "test1", "test1", 1, "test1");

        var patients = new List<Patient> { patient1, patient2 }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = _patientRepository.GetAllActivePatients();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<PatientResponseDto>>(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(patient2.Id, result.First().Id);
        Assert.Equal(patient2.FirstName, result.First().FirstName);
        Assert.Equal(patient2.LastName, result.First().LastName);
        Assert.Equal(patient2.Email, result.First().Email);
    }

    [Fact]
    public void GetAllActivePatients_WhenNoPatientsExist_ShouldReturnEmptyList()
    {
        // Arrange
        var patients = new List<Patient>()
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = _patientRepository.GetAllActivePatients();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<PatientResponseDto>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetAllArchivedPatients_WhenPatientsExist_ShouldReturnAllArchivedPatients()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");
        var patient2 = new Patient("test1", "test1", "test1@test.com", Gender.Male, "test1", "test1", 19, "test1",
            "test1", "test1", "test1", 1, "test1");
        var patient3 = new Patient("test2", "test2", "test2@test.com", Gender.Male, "test2", "test2", 19, "test2",
            "test2", "test2", "test2", 1, "test2");

        patient1.ArchivePatient("test");
        patient2.ArchivePatient("test1");

        var patients = new List<Patient> { patient1, patient2, patient3 }
            .AsQueryable()
            .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = _patientRepository.GetAllArchivedPatients();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<PatientResponseDto>>(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(patient1.Id, result.First().Id);
        Assert.Equal(patient1.FirstName, result.First().FirstName);
        Assert.Equal(patient1.LastName, result.First().LastName);
        Assert.Equal(patient1.Email, result.First().Email);
    }

    [Fact]
    public void GetAllArchivedPatients_WhenNoPatientsExist_ShouldReturnEmptyList()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");
        var patient2 = new Patient("test1", "test1", "test1@test.com", Gender.Male, "test1", "test1", 19, "test1",
            "test1", "test1", "test1", 1, "test1");
        var patient3 = new Patient("test2", "test2", "test2@test.com", Gender.Male, "test2", "test2", 19, "test2",
            "test2", "test2", "test2", 1, "test2");


        var patients = new List<Patient>()
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = _patientRepository.GetAllArchivedPatients();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<PatientResponseDto>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task CreatePatient_WhenPatientIsCreated_ShouldCreatePatient()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");

        var patients = new List<Patient> { patient1 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        await _patientRepository.CreatePatient(patient1);

        //Assert
        _mockOralEhrContext.Verify(x => x.Patients.AddAsync(patient1, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeletePatient_WhenPatientIsDeleted_ShouldDeletePatient()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");

        var patients = new List<Patient> { patient1 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        await _patientRepository.DeletePatient(patient1.Id);

        //Assert
        _mockOralEhrContext.Verify(x => x.Patients.Remove(patient1), Times.Once);
    }

    [Fact]
    public async Task GetPatientDetails_ShoudlReturn_PatientResponseDto()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");

        var patients = new List<Patient> { patient1 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = await _patientRepository.GetPatientDetails(patient1.Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<PatientResponseDto>(result);
        Assert.Equal(patient1.Id, result.Id);
        Assert.Equal(patient1.FirstName, result.FirstName);
        Assert.Equal(patient1.LastName, result.LastName);
        Assert.Equal(patient1.Email, result.Email);
    }

    [Fact]
    public async Task GetPatientDetails_WhenPatientDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");

        var patients = new List<Patient> { patient1 }
            .AsQueryable().BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Patients).Returns(patients.Object);

        //Act
        var result = await _patientRepository.GetPatientDetails(Guid.NewGuid());

        //Assert
        Assert.Null(result);
    }
}