namespace Domain.Entities;
public class Article : AuditableEntity
{
    public Guid ArticleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ItemNumber { get; set; } = string.Empty;
    public string StorageBin { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ShoppingUrl { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = default!;
    public Guid OperationAreaId { get; set; }
    public OperationArea OperationArea { get; set; } = default!;
    public Guid StoragePlaceId { get; set; }
    public StoragePlace StoragePlace { get; set; } = default!;
    public Guid ProcurementId { get; set; }
    public Procurement Procurement { get; set; } = default!;
}
