using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.Groups.Repositories;

namespace Application.Features.WarehouseManager.Groups.Operations.Update;
public class UpdateGroupHandler : IRequestHandler<UpdateGroupRequest, Unit>
{
    private readonly IGroupRepository _groupRepository;

    public UpdateGroupHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Unit> Handle(UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.Id);
        if (group == null)
            throw new NotFoundException(nameof(Group), request.Id);

        var validator = new UpdateGroupValidator(_groupRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        GroupMapper.UpdateGroupRequestToGroup(request, group);
        group.LastModifiedDate = DateTime.Now;
        await _groupRepository.UpdateAsync(group);

        return Unit.Value;
    }
}
