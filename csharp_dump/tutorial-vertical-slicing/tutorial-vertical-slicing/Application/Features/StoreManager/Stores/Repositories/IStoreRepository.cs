using Application.Features.StoreManager.Stores.Models;

namespace Application.Features.StoreManager.Stores.Repositories;
public interface IStoreRepository : IRepository<Store>
{
    Task<bool> IsStoreIdUnique(string name);
    Task<Store?> GetByStoreIdAsync(string storeId);
}
