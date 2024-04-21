namespace Application.Features.Groups.Queries.GetGroups;
public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, List<GetGroupsVm>>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupsQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<List<GetGroupsVm>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = (await _groupRepository.GetAllAsync()).OrderBy(u => u.Name);

        var groupsVm = Mappers.GroupsToGetGroupsVm(groups);

        return groupsVm;
    }
}
