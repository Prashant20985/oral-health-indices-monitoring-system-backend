using App.Domain.DTOs.ResearchGroupDtos.Response;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Repository;


/// <summary>
/// Repository interface for managing research groups and their associated patients.
/// </summary>
public interface IResearchGroupRepository
{
    /// <summary>
    /// Retrieves all patients who are not part of any research group.
    /// </summary>
    /// <returns>An IQueryable collection of ResearchGroupPatientResponseDto representing the patients.</returns>
    IQueryable<ResearchGroupPatientResponseDto> GetAllPatientsNotInAnyResearchGroup();

    /// <summary>
    /// Retrieves all research groups.
    /// </summary>
    /// <returns>An IQueryable collection of ResearchGroupResponseDto representing the research groups.</returns>
    IQueryable<ResearchGroupResponseDto> GetAllResearchGroups();

    /// <summary>
    /// Retrieves a research group by its name.
    /// </summary>
    /// <param name="groupName">The name of the research group.</param>
    /// <returns>A Task representing the asynchronous operation, with a ResearchGroup as the result.</returns>
    Task<ResearchGroup> GetResearchGroupByName(string groupName);

    /// <summary>
    /// Deletes a specified research group.
    /// </summary>
    /// <param name="researchGroup">The research group to delete.</param>
    void DeleteResearchGroup(ResearchGroup researchGroup);

    /// <summary>
    /// Creates a new research group.
    /// </summary>
    /// <param name="researchGroup">The research group to create.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    Task CreateResearchGroup(ResearchGroup researchGroup);

    /// <summary>
    /// Retrieves a research group by its unique identifier.
    /// </summary>
    /// <param name="researchGroupId">The unique identifier of the research group.</param>
    /// <returns>A Task representing the asynchronous operation, with a ResearchGroup as the result.</returns>
    Task<ResearchGroup> GetResearchGroupById(Guid researchGroupId);

    /// <summary>
    /// Retrieves detailed information about a research group by its unique identifier.
    /// </summary>
    /// <param name="researchGroupId">The unique identifier of the research group.</param>
    /// <returns>A Task representing the asynchronous operation, with a ResearchGroupResponseDto as the result.</returns>
    Task<ResearchGroupResponseDto> GetResearchGroupDetailsById(Guid researchGroupId);
}