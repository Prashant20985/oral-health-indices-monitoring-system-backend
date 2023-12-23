using App.Application.Core;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.CreateResearchGroup;

/// <summary>
/// Handles the command to create a new research group.
/// </summary>
internal sealed class CreateResearchGroupHandler : IRequestHandler<CreateResearchGroupCommand, OperationResult<Unit>>
{
    private readonly IResearchGroupRepository _researchGroupRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateResearchGroupHandler"/> class.
    /// </summary>
    /// <param name="researchGroupRepository">The repository for research group-related operations.</param>
    public CreateResearchGroupHandler(IResearchGroupRepository researchGroupRepository)
    {
        _researchGroupRepository = researchGroupRepository;
    }

    /// <summary>
    /// Handles the command to create a new research group.
    /// </summary>
    /// <param name="request">The command to create a new research group.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(CreateResearchGroupCommand request, CancellationToken cancellationToken)
    {
        // Check if the group with the same name already exists.
        var checkGroupAlreadyExists = await _researchGroupRepository.GetResearchGroupByName(request.CreateResearchGroup.GroupName);

        if (checkGroupAlreadyExists is not null)
            return OperationResult<Unit>.Failure("Group already exists.");

        // Create a new ResearchGroup entity based on the command data.
        var description = string.IsNullOrEmpty(request.CreateResearchGroup.Description) ? null : request.CreateResearchGroup.Description;
        var researchGroup = new ResearchGroup(request.CreateResearchGroup.GroupName, description, request.DoctorId);

        // Save the new research group to the repository.
        await _researchGroupRepository.CreateResearchGroup(researchGroup);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}

