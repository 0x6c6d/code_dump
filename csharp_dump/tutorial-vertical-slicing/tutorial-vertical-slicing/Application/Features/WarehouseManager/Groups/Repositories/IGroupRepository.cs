using Application.Features.WarehouseManager.Groups.Models;

namespace Application.Features.WarehouseManager.Groups.Repositories;
public interface IGroupRepository : IRepository<Group>
{
    Task<bool> IsGroupNameUnique(string name);
}
