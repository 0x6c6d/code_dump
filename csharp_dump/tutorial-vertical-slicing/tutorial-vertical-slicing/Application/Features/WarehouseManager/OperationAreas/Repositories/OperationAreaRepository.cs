using Application.Features.WarehouseManager.OperationAreas.Models;

namespace Application.Features.WarehouseManager.OperationAreas.Repositories;
public class OperationAreaRepository : BaseRepository<OperationArea>, IOperationAreaRepository
{
    public OperationAreaRepository(DataContext dbContext) : base(dbContext) { }

    public Task<bool> IsOperationAreaNameUnique(string name)
    {
        var match = _dbContext.OperationAreas.Any(a => a.Name.ToLower() == name.ToLower());
        return Task.FromResult(match);
    }
}
