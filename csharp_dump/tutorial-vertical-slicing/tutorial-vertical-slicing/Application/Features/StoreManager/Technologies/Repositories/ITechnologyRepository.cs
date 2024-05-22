using Application.Features.StoreManager.Technologies.Models;

namespace Application.Features.StoreManager.Technologies.Repositories;
public interface ITechnologyRepository : IRepository<Technology>
{
    Task<bool> IsStoreIdUnique(string storeId);
    Task<Technology?> GetByStoreIdAsync(string storeId);
    List<string> SearchTechnologyForString(string searchInput);
}
