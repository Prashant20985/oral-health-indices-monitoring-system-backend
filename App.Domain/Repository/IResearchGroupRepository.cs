using App.Domain.DTOs;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

public interface IResearchGroupRepository
{
    IQueryable<ResearchGroupPatientDto> GetAllPatientsNotInAnyResearchGroup();
    IQueryable<ResearchGroupDto> GetAllResearchGroups();
    Task<ResearchGroup> GetResearchGroupByName(string groupName);
    void DeleteResearchGroup(ResearchGroup researchGroup);
    Task CreateResearchGroup(ResearchGroup researchGroup);
    Task<ResearchGroup> GetResearchGroupById(Guid researchGroupId);
}
