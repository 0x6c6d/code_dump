using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.StoragePlaces.Models;

namespace Application.Features.WarehouseManager.Articles.Operations.Read.One;
public class GetArticleReturn
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string InventoryNumber { get; set; } = string.Empty;
    public string Store { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ShoppingUrl { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public Group Group { get; set; } = new();
    public Category Category { get; set; } = new();
    public OperationArea OperationArea { get; set; } = new();
    public StoragePlace StoragePlace { get; set; } = new();
    public Procurement Procurement { get; set; } = new();
    public DateTime Disposed { get; set; }
}