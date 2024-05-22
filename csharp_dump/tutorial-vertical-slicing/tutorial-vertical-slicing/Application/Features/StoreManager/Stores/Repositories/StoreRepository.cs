using Application.Features.StoreManager.Stores.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StoreManager.Stores.Repositories;
public class StoreRepository : BaseRepository<Store>, IStoreRepository
{
    public StoreRepository(DataContext dataContext) : base(dataContext) { }

    public Task<bool> IsStoreIdUnique(string storeId)
    {
        var match = _dbContext.Stores.Any(a => a.StoreId.ToLower() == storeId.ToLower());
        return Task.FromResult(match);
    }

    public async Task<Store?> GetByStoreIdAsync(string storeId)
    {
        var store = await _dbContext.Stores.FirstOrDefaultAsync(s => s.StoreId == storeId);
        return store;
    }
}
