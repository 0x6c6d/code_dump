using Application.Features.WarehouseManager.Groups.Repositories;

namespace Application.Features.WarehouseManager.Groups.Operations.Read.All;
public class GetGroupsHandler : IRequestHandler<GetGroupsRequest, List<GetGroupsReturn>>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupsHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<List<GetGroupsReturn>> Handle(GetGroupsRequest request, CancellationToken cancellationToken)
    {
        var groups = (await _groupRepository.GetAllAsync()).OrderBy(u => u.Name);

        var groupsVm = GroupMapper.GroupsToGetGroupsReturn(groups);

        return groupsVm;
    }
}
