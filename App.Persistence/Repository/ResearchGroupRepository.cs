using App.Domain.DTOs;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using App.Persistence.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

public class ResearchGroupRepository : IResearchGroupRepository
{
    private readonly OralEhrContext _oralEhrContext;
    private readonly IMapper _mapper;

    public ResearchGroupRepository(OralEhrContext oralEhrContext, IMapper mapper)
    {
        _oralEhrContext = oralEhrContext;
        _mapper = mapper;
    }

    public async Task CreateResearchGroup(ResearchGroup researchGroup) =>
        await _oralEhrContext.ResearchGroups.AddAsync(researchGroup);

    public void DeleteResearchGroup(ResearchGroup research) =>
        _oralEhrContext.ResearchGroups.Remove(research);

    public IQueryable<ResearchGroupPatientDto> GetAllPatientsNotInAnyResearchGroup() =>
        _oralEhrContext.Patients
            .Where(rg => rg.ResearchGroupId.Equals(Guid.Empty))
            .OrderByDescending(rg => rg.CreatedAt)
            .ProjectTo<ResearchGroupPatientDto>(_mapper.ConfigurationProvider)
            .AsQueryable();

    public IQueryable<ResearchGroupDto> GetAllResearchGroups() =>
        _oralEhrContext.ResearchGroups
            .OrderByDescending(rg => rg.CreatedAt)
            .ProjectTo<ResearchGroupDto>(_mapper.ConfigurationProvider)
            .AsQueryable();

    public async Task<ResearchGroup> GetResearchGroupById(Guid researchGroupId) =>
        await _oralEhrContext.ResearchGroups
            .FirstOrDefaultAsync(rg => rg.Id.Equals(researchGroupId));

    public async Task<ResearchGroup> GetResearchGroupByName(string groupName) =>
        await _oralEhrContext.ResearchGroups
            .FirstOrDefaultAsync(rg => rg.GroupName.Equals(groupName));

    public async Task<ResearchGroupDto> GetResearchGroupDetailsById(Guid researchGroupId) => await
        _oralEhrContext.ResearchGroups
        .ProjectTo<ResearchGroupDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(rg => rg.Id.Equals(researchGroupId));
}
