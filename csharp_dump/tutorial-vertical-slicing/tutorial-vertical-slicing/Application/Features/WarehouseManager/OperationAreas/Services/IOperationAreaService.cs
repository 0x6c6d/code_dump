
using Application.Features.WarehouseManager.OperationAreas.Models;

namespace Application.Features.WarehouseManager.OperationAreas.Services;

public interface IOperationAreaService
{
    Task<ServiceResponse<Guid>> CreateOperationAreaAsync(string name);
    Task<ServiceResponse<bool>> DeleteOperationAreaAsync(Guid id);
    Task<ServiceResponse<OperationArea>> GetOperationAreaAsync(Guid id);
    Task<ServiceResponse<List<OperationArea>>> GetOperationAreasAsync();
    Task<ServiceResponse<bool>> UpdateOperationAreaAsync(Guid id, string name);
}