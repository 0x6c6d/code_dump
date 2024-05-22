
using Application.Features.StoreManager.Stores.Models;

namespace Application.Features.StoreManager.Stores.Services;

public interface IStoreService
{
    Task<ServiceResponse<List<Store>>> GetStoresAsync();
    Task<ServiceResponse<Store>> GetStoreAsync(string storeId);
    Task<ServiceResponse<string>> CreateStoreAsync(string storeId);
    Task<ServiceResponse<bool>> UpdateStoreAsync(string storeId, string description);
    Task<ServiceResponse<bool>> DeleteStoreAsync(string storeId);
}