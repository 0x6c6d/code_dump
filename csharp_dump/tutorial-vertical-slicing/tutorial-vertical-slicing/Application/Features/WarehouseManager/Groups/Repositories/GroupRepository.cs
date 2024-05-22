using Application.Features.WarehouseManager.Groups.Models;

namespace Application.Features.WarehouseManager.Groups.Repositories;
public class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    public GroupRepository(DataContext dbContext) : base(dbContext) { }

    public Task<bool> IsGroupNameUnique(string name)
    {
        var match = _dbContext.Groups.Any(a => a.Name.ToLower() == name.ToLower());
        return Task.FromResult(match);
    }
}
