using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.Groups.Repositories;

namespace Application.Features.WarehouseManager.Groups.Operations.Read.One;
public class GetGroupHandler : IRequestHandler<GetGroupRequest, GetGroupReturn>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<GetGroupReturn> Handle(GetGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.Id);

        if (group == null)
            throw new NotFoundException(nameof(Group), request.Id);

        var groupVm = GroupMapper.GroupToGetGroupReturn(group);

        return groupVm;
    }
}
