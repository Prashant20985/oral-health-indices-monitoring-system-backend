using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.OralHealthExamination;
using App.Persistence.Contexts;
using App.Persistence.Repository;
using AutoMapper;
using MockQueryable.Moq;
using Moq;

namespace App.Persistence.Test.Repository;

public class PatientExaminationCardRepositoryTests
{
    private readonly Mock<OralEhrContext> _mockOralEhrContext;
    private readonly PatientExaminationCardRepository _patientExaminationCardRepository;

    public PatientExaminationCardRepositoryTests()
    {
        _mockOralEhrContext = new Mock<OralEhrContext>();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PatientExaminationCard, PatientExaminationCardDto>();
            cfg.CreateMap<RiskFactorAssessment, RiskFactorAssessmentDto>();
            cfg.CreateMap<Bewe, BeweResponseDto>();
            cfg.CreateMap<API, APIResponseDto>();
            cfg.CreateMap<Bleeding, BleedingResponseDto>();
            cfg.CreateMap<DMFT_DMFS, DMFT_DMFSResponseDto>();
            cfg.CreateMap<PatientExaminationResult, PatientExaminationResultDto>();
        });
        var mapper = mapperConfig.CreateMapper();
        _patientExaminationCardRepository = new PatientExaminationCardRepository(_mockOralEhrContext.Object, mapper);
    }

    [Fact]
    public async Task GetAPIByCardId_WhenAPIExists_ShouldReturnAPI()
    {
        // Arrange
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id)
        {
            API = api,
        };

        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid())
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);
        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);
        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);
        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);


        // Act
        var result = await _patientExaminationCardRepository.GetAPIByCardId(patientExaminationCard.Id);

        // Assert
        Assert.IsType<API>(result);
        Assert.NotNull(result);
        Assert.Equal(api.Id, result.Id);
        Assert.Equal(api.APIResult, result.APIResult);
        Assert.Equal(api.Maxilla, result.Maxilla);
        Assert.Equal(api.Mandible, result.Mandible);
        Assert.Equal(api.DoctorComment, result.DoctorComment);
        Assert.Equal(api.AssessmentModel, result.AssessmentModel);
        Assert.Equal(api.PatientExaminationResult, result.PatientExaminationResult);
    }

    [Fact]
    public async Task GetAPIByCardId_WhenCardDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id)
        {
            API = api,
        };

        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid())
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);
        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);
        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);
        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetAPIByCardId(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBeweByCardId_WhenBeweExists_ShouldReturnBewe()
    {
        // Arrange
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id)
        {
            Bewe = bewe,
        };

        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid())
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);
        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);
        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);
        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetBeweByCardId(patientExaminationCard.Id);

        // Assert
        Assert.IsType<Bewe>(result);
        Assert.NotNull(result);
        Assert.Equal(bewe.Id, result.Id);
    }

    [Fact]
    public async Task GetBeweByCardId_WhenCardDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id)
        {
            Bewe = bewe,
        };

        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid())
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);
        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);
        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);
        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetBeweByCardId(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetDMFT_DMFSByCardId_WhenDMFT_DMFSExists_ShouldReturnDMFT_DMFS()
    {
        // Arrange
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id)
        {
            DMFT_DMFS = dmft_dmfs,
        };

        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid())
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);
        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);
        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);
        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetDMFT_DMFSByCardId(patientExaminationCard.Id);

        // Assert
        Assert.IsType<DMFT_DMFS>(result);
        Assert.NotNull(result);
        Assert.Equal(dmft_dmfs.Id, result.Id);
    }

    [Fact]
    public async Task GetDMFT_DMFSByCardId_WhenCardDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id)
        {
            DMFT_DMFS = dmft_dmfs,
        };

        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid())
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);
        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);
        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);
        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetDMFT_DMFSByCardId(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBleedingByCardId_WhenBleedingExists_ShouldReturnBleeding()
    {
        // Arrange
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id)
        {
            Bleeding = bleeding,
        };

        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid())
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);
        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);
        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);
        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetBleedingByCardId(patientExaminationCard.Id);

        // Assert
        Assert.IsType<Bleeding>(result);
        Assert.NotNull(result);
        Assert.Equal(bleeding.Id, result.Id);
    }

    [Fact]
    public async Task GetBleedingByCardId_WhenCardDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id)
        {
            Bleeding = bleeding,
        };

        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid())
        {
            PatientExaminationResult = patientExaminationResult
        };

        patientExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);
        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);
        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);
        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetBleedingByCardId(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPatientExaminationCard_WhenCardExists_ShouldReturnCard()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetPatientExaminationCard(patientExaminationCard.Id);

        // Assert
        Assert.IsType<PatientExaminationCard>(result);
        Assert.NotNull(result);
        Assert.Equal(patientId, result.PatientId);
    }

    [Fact]
    public async Task GetPatientExaminationCard_WhenCardDoesNotExist_ShouldReturnNull()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetPatientExaminationCard(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPatientExaminationCardDto_WhenCardExists_ShouldReturnCardDto()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        var patientExaminationCardDto = new PatientExaminationCardDto
        {
            Id = patientExaminationCard.Id,
        };

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCardDtos = new List<PatientExaminationCardDto> { patientExaminationCardDto }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetPatientExaminationCardDto(patientExaminationCard.Id);

        // Assert
        Assert.IsType<PatientExaminationCardDto>(result);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetPatientExaminationCardDto_WhenCardDoesNotExist_ShouldReturnNull()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        var patientExaminationCardDto = new PatientExaminationCardDto
        {
            Id = patientExaminationCard.Id,
        };

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCardDtos = new List<PatientExaminationCardDto> { patientExaminationCardDto }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetPatientExaminationCardDto(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPatientExaminationCardDtosInRegularModeByPatientId_WhenCardExists_ShouldReturnCardDtos()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        patientExaminationCard.SetRegularMode();

        var patientExaminationCardDto = new PatientExaminationCardDto
        {
            Id = patientExaminationCard.Id,
        };

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCardDtos = new List<PatientExaminationCardDto> { patientExaminationCardDto }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetPatientExaminationCardDtosInRegularModeByPatientId(patientId);

        // Assert
        Assert.IsType<List<PatientExaminationCardDto>>(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetPatientExaminationCardDtosInRegularModeByPatientId_WhenCardDoesNotExist_ShouldReturnEmptyList()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        patientExaminationCard.SetRegularMode();

        var patientExaminationCardDto = new PatientExaminationCardDto
        {
            Id = patientExaminationCard.Id,
        };

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCardDtos = new List<PatientExaminationCardDto> { patientExaminationCardDto }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetPatientExaminationCardDtosInRegularModeByPatientId(Guid.NewGuid());

        // Assert
        Assert.IsType<List<PatientExaminationCardDto>>(result);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPatientExminationCardDtosInTestModeByPatientId_WhenCardExists_ShouldReturnCardDtos()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        patientExaminationCard.SetTestMode();

        var patientExaminationCardDto = new PatientExaminationCardDto
        {
            Id = patientExaminationCard.Id,
        };

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCardDtos = new List<PatientExaminationCardDto> { patientExaminationCardDto }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetPatientExminationCardDtosInTestModeByPatientId(patientId);

        // Assert
        Assert.IsType<List<PatientExaminationCardDto>>(result);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task GetPatientExminationCardDtosInTestModeByPatientId_WhenCardDoesNotExist_ShouldReturnEmptyList()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        patientExaminationCard.SetTestMode();

        var patientExaminationCardDto = new PatientExaminationCardDto
        {
            Id = patientExaminationCard.Id,
        };

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationCardDtos = new List<PatientExaminationCardDto> { patientExaminationCardDto }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetPatientExminationCardDtosInTestModeByPatientId(Guid.NewGuid());

        // Assert
        Assert.IsType<List<PatientExaminationCardDto>>(result);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetRiskFactorAssessmentByCardId_WhenCardExists_ShouldReturnRiskFactorAssessment()
    {
        // Arrange
        var riskFactorAssessment = new RiskFactorAssessment();
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId)
        {
            RiskFactorAssessment = riskFactorAssessment
        };

        patientExaminationCard.SetRiskFactorAssesmentId(riskFactorAssessment.Id);

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        var riskFactorAssessments = new List<RiskFactorAssessment> { riskFactorAssessment }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);
        _mockOralEhrContext.Setup(x => x.RiskFactorAssessments).Returns(riskFactorAssessments.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetRiskFactorAssessmentByCardId(patientExaminationCard.Id);

        // Assert
        Assert.IsType<RiskFactorAssessment>(result);
        Assert.NotNull(result);
        Assert.Equal(riskFactorAssessment.Id, result.Id);
    }

    [Fact]
    public async Task GetRiskFactorAssessmentByCardId_WhenCardDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var riskFactorAssessment = new RiskFactorAssessment();
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId)
        {
            RiskFactorAssessment = riskFactorAssessment
        };

        patientExaminationCard.SetRiskFactorAssesmentId(riskFactorAssessment.Id);

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        var riskFactorAssessments = new List<RiskFactorAssessment> { riskFactorAssessment }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);
        _mockOralEhrContext.Setup(x => x.RiskFactorAssessments).Returns(riskFactorAssessments.Object);

        // Act
        var result = await _patientExaminationCardRepository.GetRiskFactorAssessmentByCardId(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAPI_ShouldAddAPI()
    {
        var api = new API();

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);

        // Act
        await _patientExaminationCardRepository.AddAPI(api);

        // Assert
        _mockOralEhrContext.Verify(x => x.APIs.AddAsync(api, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddBewe_ShouldAddBewe()
    {
        var bewe = new Bewe();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);

        // Act
        await _patientExaminationCardRepository.AddBewe(bewe);

        // Assert
        _mockOralEhrContext.Verify(x => x.Bewes.AddAsync(bewe, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddDMFT_DMFS_ShouldAddDMFT_DMFS()
    {
        var dmft_dmfs = new DMFT_DMFS();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);

        // Act
        await _patientExaminationCardRepository.AddDMFT_DMFS(dmft_dmfs);

        // Assert
        _mockOralEhrContext.Verify(x => x.DMFT_DMFSs.AddAsync(dmft_dmfs, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddBleeding_ShouldAddBleeding()
    {
        var bleeding = new Bleeding();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);

        // Act
        await _patientExaminationCardRepository.AddBleeding(bleeding);

        // Assert
        _mockOralEhrContext.Verify(x => x.Bleedings.AddAsync(bleeding, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddPatientExaminationCard_ShouldAddPatientExaminationCard()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        await _patientExaminationCardRepository.AddPatientExaminationCard(patientExaminationCard);

        // Assert
        _mockOralEhrContext.Verify(x => x.PatientExaminationCards.AddAsync(patientExaminationCard, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddPatientExaminationResult_ShouldAddPatientExaminationResult()
    {
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id);

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);

        // Act
        await _patientExaminationCardRepository.AddPatientExaminationResult(patientExaminationResult);

        // Assert
        _mockOralEhrContext.Verify(x => x.PatientExaminationResults.AddAsync(patientExaminationResult, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task AddRiskFactorAssessment_ShouldAddRiskFactorAssessment()
    {
        var riskFactorAssessment = new RiskFactorAssessment();

        var riskFactorAssessments = new List<RiskFactorAssessment> { riskFactorAssessment }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.RiskFactorAssessments).Returns(riskFactorAssessments.Object);

        // Act
        await _patientExaminationCardRepository.AddRiskFactorAssessment(riskFactorAssessment);

        // Assert
        _mockOralEhrContext.Verify(x => x.RiskFactorAssessments.AddAsync(riskFactorAssessment, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task DeletePatientExaminationCard_ShouldDeleteCard()
    {
        var patientId = Guid.NewGuid();
        var api = new API();
        var bewe = new Bewe();
        var dmft_dmfs = new DMFT_DMFS();
        var bleeding = new Bleeding();
        var riskFActorAssessmentModel = new RiskFactorAssessmentModel();

        var riskFactorAssessment = new RiskFactorAssessment();

        riskFactorAssessment.SetRiskFactorAssessmentModel(riskFActorAssessmentModel);
        var patientExaminationResult = new PatientExaminationResult(bewe.Id, dmft_dmfs.Id, api.Id, bleeding.Id)
        {
            Bewe = bewe,
            DMFT_DMFS = dmft_dmfs,
            Bleeding = bleeding,
            API = api
        };

        var patientExaminationCard = new PatientExaminationCard(patientId)
        {
            PatientExaminationResult = patientExaminationResult,
            RiskFactorAssessment = riskFactorAssessment
        };

        var patientExaminationCards = new List<PatientExaminationCard> { patientExaminationCard }
        .AsQueryable()
        .BuildMockDbSet();

        var apis = new List<API> { api }
        .AsQueryable()
        .BuildMockDbSet();

        var bewes = new List<Bewe> { bewe }
        .AsQueryable()
        .BuildMockDbSet();

        var dmft_dmfses = new List<DMFT_DMFS> { dmft_dmfs }
        .AsQueryable()
        .BuildMockDbSet();

        var bleedings = new List<Bleeding> { bleeding }
        .AsQueryable()
        .BuildMockDbSet();

        var patientExaminationResults = new List<PatientExaminationResult> { patientExaminationResult }
        .AsQueryable()
        .BuildMockDbSet();

        var riskFactorAssessments = new List<RiskFactorAssessment> { riskFactorAssessment }
        .AsQueryable()
        .BuildMockDbSet();

        _mockOralEhrContext.Setup(x => x.Bewes).Returns(bewes.Object);
        _mockOralEhrContext.Setup(x => x.Bleedings).Returns(bleedings.Object);
        _mockOralEhrContext.Setup(x => x.DMFT_DMFSs).Returns(dmft_dmfses.Object);
        _mockOralEhrContext.Setup(x => x.APIs).Returns(apis.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationResults).Returns(patientExaminationResults.Object);
        _mockOralEhrContext.Setup(x => x.RiskFactorAssessments).Returns(riskFactorAssessments.Object);
        _mockOralEhrContext.Setup(x => x.PatientExaminationCards).Returns(patientExaminationCards.Object);

        // Act
        await _patientExaminationCardRepository.DeletePatientExaminationCard(patientExaminationCard.Id);

        // Assert
        _mockOralEhrContext.Verify(x => x.PatientExaminationCards.Remove(patientExaminationCard), Times.Once);
    }
}
