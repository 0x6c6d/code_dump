using Application.Features.WarehouseManager.Procurements.Models;

namespace Application.Features.WarehouseManager.Procurements.Repositories;
public interface IProcurementRepository : IRepository<Procurement>
{
    Task<bool> IsProcurementNameUnique(string name);
}
