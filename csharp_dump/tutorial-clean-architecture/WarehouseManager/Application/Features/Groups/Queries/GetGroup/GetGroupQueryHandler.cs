namespace Application.Features.Groups.Queries.GetGroup;
public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GetGroupVm>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<GetGroupVm> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId);

        if (group == null)
            throw new NotFoundException(nameof(Group), request.GroupId);

        var groupVm = Mappers.GroupToGetGroupVm(group);

        return groupVm;
    }
}
