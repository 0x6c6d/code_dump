using Application.Features.WarehouseManager.StoragePlaces.Models;

namespace Application.Features.WarehouseManager.StoragePlaces.Repositories;
public interface IStoragePlaceRepository : IRepository<StoragePlace>
{
    Task<bool> IsStoragePlaceNameUnique(string name);
}
