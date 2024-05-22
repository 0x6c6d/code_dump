
using Application.Features.WarehouseManager.StoragePlaces.Models;

namespace Application.Features.WarehouseManager.StoragePlaces.Services;

public interface IStoragePlaceService
{
    Task<ServiceResponse<Guid>> CreateStoragePlaceAsync(string name);
    Task<ServiceResponse<bool>> DeleteStoragePlaceAsync(Guid id, string oldName);
    Task<ServiceResponse<StoragePlace>> GetStoragePlaceAsync(Guid id);
    Task<ServiceResponse<List<StoragePlace>>> GetStoragePlacesAsync();
    Task<ServiceResponse<bool>> UpdateStoragePlaceAsync(Guid id, string oldName, string newName);
}