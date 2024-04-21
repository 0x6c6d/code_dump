namespace Domain.Entities;
public class OperationArea : AuditableEntity
{
    public Guid OperationAreaId { get; set; }
    public string Name { get; set; } = string.Empty;
}
