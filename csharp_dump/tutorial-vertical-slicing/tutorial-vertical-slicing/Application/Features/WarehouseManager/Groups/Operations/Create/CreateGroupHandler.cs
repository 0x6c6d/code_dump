using Application.Features.WarehouseManager.Groups.Repositories;

namespace Application.Features.WarehouseManager.Groups.Operations.Create;
public class CreateGroupHandler : IRequestHandler<CreateGroupRequest, Guid>
{
    private readonly IGroupRepository _groupRepository;

    public CreateGroupHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Guid> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateGroupValidator(_groupRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var group = GroupMapper.CreateGroupRequestToGroup(request);
        group.CreatedDate = DateTime.Now;
        group.LastModifiedDate = DateTime.Now;
        group = await _groupRepository.AddAsync(group);

        return group.Id;
    }
}
