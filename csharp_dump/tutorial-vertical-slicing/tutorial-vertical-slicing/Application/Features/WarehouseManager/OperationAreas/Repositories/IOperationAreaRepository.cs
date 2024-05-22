using Application.Features.WarehouseManager.OperationAreas.Models;

namespace Application.Features.WarehouseManager.OperationAreas.Repositories;
public interface IOperationAreaRepository : IRepository<OperationArea>
{
    Task<bool> IsOperationAreaNameUnique(string name);
}
