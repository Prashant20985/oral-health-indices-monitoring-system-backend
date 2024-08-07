﻿using App.Domain.DTOs.ResearchGroupDtos.Response;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;

public interface IResearchGroupRepository
{
    IQueryable<ResearchGroupPatientResponseDto> GetAllPatientsNotInAnyResearchGroup();
    IQueryable<ResearchGroupResponseDto> GetAllResearchGroups();
    Task<ResearchGroup> GetResearchGroupByName(string groupName);
    void DeleteResearchGroup(ResearchGroup researchGroup);
    Task CreateResearchGroup(ResearchGroup researchGroup);
    Task<ResearchGroup> GetResearchGroupById(Guid researchGroupId);
    Task<ResearchGroupResponseDto> GetResearchGroupDetailsById(Guid researchGroupId);
}
