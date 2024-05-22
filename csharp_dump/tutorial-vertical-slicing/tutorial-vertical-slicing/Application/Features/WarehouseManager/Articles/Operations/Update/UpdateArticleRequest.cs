namespace Application.Features.WarehouseManager.Articles.Operations.Update;
public class UpdateArticleRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string InventoryNumber { get; set; } = string.Empty;
    public string Store { get; set; } = string.Empty;
    public Guid GroupId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid OperationAreaId { get; set; }
    public Guid StoragePlaceId { get; set; }
    public Guid ProcurementId { get; set; }
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string ShoppingUrl { get; set; } = string.Empty;
    public DateTime Disposed { get; set; }
}