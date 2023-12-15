using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

public interface IResearchGroupRepository
{
    IQueryable<Patient> GetAllPatientsInResearchGroup(Guid researchGroupId);
    IQueryable<Patient> GetAllPatientsNotInAnyResearchGroup();
    IQueryable<ResearchGroup> GetAllResearchGroups();
    Task<ResearchGroup> GetResearchGroupByName(string groupName);
    void DeleteResearchGroup(ResearchGroup researchGroup);
    Task CreateResearchGroup(ResearchGroup researchGroup);
    Task<ResearchGroup> GetResearchGroupById(Guid researchGroupId);
}
