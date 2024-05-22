
using Application.Features.WarehouseManager.Groups.Models;

namespace Application.Features.WarehouseManager.Groups.Services;

public interface IGroupService
{
    Task<ServiceResponse<Guid>> CreateGroupAsync(string name);
    Task<ServiceResponse<bool>> DeleteGroupAsync(Guid id);
    Task<ServiceResponse<Group>> GetGroupAsync(Guid id);
    Task<ServiceResponse<List<Group>>> GetGroupsAsync();
    Task<ServiceResponse<bool>> UpdateGroupAsync(Guid id, string name);
}