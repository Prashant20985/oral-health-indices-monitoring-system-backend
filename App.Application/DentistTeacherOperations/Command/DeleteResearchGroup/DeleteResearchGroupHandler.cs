using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.DeleteResearchGroup;

/// <summary>
/// Handles the command to delete a research group.
/// </summary>
internal sealed class DeleteResearchGroupHandler : IRequestHandler<DeleteResearchGroupCommand, OperationResult<Unit>>
{
    private readonly IResearchGroupRepository _researchGroupRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteResearchGroupHandler"/> class.
    /// </summary>
    /// <param name="researchGroupRepository">The repository for research group-related operations.</param>
    public DeleteResearchGroupHandler(IResearchGroupRepository researchGroupRepository)
    {
        _researchGroupRepository = researchGroupRepository;
    }

    /// <summary>
    /// Handles the command to delete a research group.
    /// </summary>
    /// <param name="request">The command to delete a research group.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(DeleteResearchGroupCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the research group based on the provided identifier.
        var researchGroup = await _researchGroupRepository.GetResearchGroupById(request.ResearchGroupId);

        // Check if the research group exists.
        if (researchGroup is null)
            return OperationResult<Unit>.Failure("Research group not found.");

        // Delete the research group.
        _researchGroupRepository.DeleteResearchGroup(researchGroup);

        //Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
}

