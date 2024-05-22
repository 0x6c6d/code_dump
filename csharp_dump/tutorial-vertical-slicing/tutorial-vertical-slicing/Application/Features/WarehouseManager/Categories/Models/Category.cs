namespace Application.Features.WarehouseManager.Categories.Models;
public class Category : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
