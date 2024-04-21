namespace Domain.Entities;
public class StoragePlace : AuditableEntity
{
    public Guid StoragePlaceId { get; set; }
    public string Name { get; set; } = string.Empty;
}