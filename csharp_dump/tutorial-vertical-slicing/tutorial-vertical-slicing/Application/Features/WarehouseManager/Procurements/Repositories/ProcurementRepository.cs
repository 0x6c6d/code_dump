using Application.Features.WarehouseManager.Procurements.Models;

namespace Application.Features.WarehouseManager.Procurements.Repositories;
public class ProcurementRepository : BaseRepository<Procurement>, IProcurementRepository
{
    public ProcurementRepository(DataContext dbContext) : base(dbContext) { }

    public Task<bool> IsProcurementNameUnique(string name)
    {
        var match = _dbContext.Procurements.Any(a => a.Name.ToLower() == name.ToLower());
        return Task.FromResult(match);
    }
}
