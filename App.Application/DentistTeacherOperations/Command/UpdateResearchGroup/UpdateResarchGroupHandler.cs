using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.UpdateResearchGroup;

/// <summary>
/// Handler for updating the name of a research group.
/// </summary>
internal sealed class UpdateResearchGroupNameHandler : IRequestHandler<UpdateResearchGroupCommand, OperationResult<Unit>>
{
    private readonly IResearchGroupRepository _researchGroupRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateResearchGroupNameHandler"/> class.
    /// </summary>
    /// <param name="researchGroupRepository">The repository for research group-related operations.</param>
    public UpdateResearchGroupNameHandler(IResearchGroupRepository researchGroupRepository)
    {
        _researchGroupRepository = researchGroupRepository;
    }

    /// <summary>
    /// Handles the command to update the name of a research group.
    /// </summary>
    /// <param name="request">The command to update the name of a research group.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(UpdateResearchGroupCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the research group based on the provided identifier.
        var researchGroup = await _researchGroupRepository.GetResearchGroupById(request.ResearchGroupId);

        // Check if the research group exists.
        if (researchGroup is null)
            return OperationResult<Unit>.Failure("Group Not Found.");

        // Update the name of the research group.
        researchGroup.UpdateGroup(request.UpdateResearchGroup.GroupName,
            request.UpdateResearchGroup.Description);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}
