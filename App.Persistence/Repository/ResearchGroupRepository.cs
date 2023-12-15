using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using App.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

public class ResearchGroupRepository : IResearchGroupRepository
{
    private readonly OralEhrContext _oralEhrContext;

    public ResearchGroupRepository(OralEhrContext oralEhrContext) =>
        _oralEhrContext = oralEhrContext;

    public async Task CreateResearchGroup(ResearchGroup researchGroup)
    {
        await _oralEhrContext.ResearchGroups.AddAsync(researchGroup);
    }

    public void DeleteResearchGroup(ResearchGroup research) => 
        _oralEhrContext.ResearchGroups.Remove(research);

    public IQueryable<Patient> GetAllPatientsInResearchGroup(Guid researchGroupId) => _oralEhrContext.Patients
            .Where(rg => rg.PatientGroupId.Equals(researchGroupId))
            .AsQueryable();

    public IQueryable<Patient> GetAllPatientsNotInAnyResearchGroup() => _oralEhrContext.Patients
            .Where(rg => rg.PatientGroupId.Equals(Guid.Empty))
            .AsQueryable();

    public IQueryable<ResearchGroup> GetAllResearchGroups() => 
        _oralEhrContext.ResearchGroups.AsQueryable();

    public async Task<ResearchGroup> GetResearchGroupById(Guid researchGroupId) => await _oralEhrContext.ResearchGroups
            .FirstOrDefaultAsync(rg => rg.Id.Equals(researchGroupId));

    public async Task<ResearchGroup> GetResearchGroupByName(string groupName) => await _oralEhrContext.ResearchGroups
            .FirstOrDefaultAsync(rg => rg.GroupName.Equals(groupName));
}
