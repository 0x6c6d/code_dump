namespace Domain.Entities;
public class Group : AuditableEntity
{
    public Guid GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
}
