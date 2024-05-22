
using Application.Features.WarehouseManager.Procurements.Models;

namespace Application.Features.WarehouseManager.Procurements.Services;

public interface IProcurementService
{
    Task<ServiceResponse<Guid>> CreateProcurementAsync(Procurement procurement);
    Task<ServiceResponse<bool>> DeleteProcurementAsync(Guid id);
    Task<ServiceResponse<Procurement>> GetProcurementAsync(Guid id);
    Task<ServiceResponse<List<Procurement>>> GetProcurementsAsync();
    Task<ServiceResponse<bool>> UpdateProcurementAsync(Procurement procurement);
}