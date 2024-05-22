namespace Application.Features.WarehouseManager.StoragePlaces.Models;
public class StoragePlace : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
