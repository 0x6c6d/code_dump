namespace Application.Features.WarehouseManager.OperationAreas.Models;
public class OperationArea : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
