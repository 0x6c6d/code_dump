namespace Application.Features.WarehouseManager.Groups.Models;
public class Group : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
