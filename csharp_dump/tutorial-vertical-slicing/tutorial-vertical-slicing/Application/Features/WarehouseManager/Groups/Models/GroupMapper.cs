using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.Groups.Operations.Create;
using Application.Features.WarehouseManager.Groups.Operations.Read.All;
using Application.Features.WarehouseManager.Groups.Operations.Read.One;
using Application.Features.WarehouseManager.Groups.Operations.Update;
using Riok.Mapperly.Abstractions;

namespace Application.Features.WarehouseManager.Groups;

[Mapper]
public static partial class GroupMapper
{
    // MediatR
    public static partial GetGroupReturn GroupToGetGroupReturn(Group group);

    public static partial List<GetGroupsReturn> GroupsToGetGroupsReturn(IOrderedEnumerable<Group> groups);

    public static partial Group CreateGroupRequestToGroup(CreateGroupRequest createGroupRequest);

    public static partial void UpdateGroupRequestToGroup(UpdateGroupRequest updateGroupRequest, Group group);

    // Business Logic
    public static partial Group GetGroupFromGetGroupReturn(GetGroupReturn getGroupReturn);

    public static partial List<Group> GetGroupsFromGetGroupsReturn(List<GetGroupsReturn> getGroupsReturn);
}
