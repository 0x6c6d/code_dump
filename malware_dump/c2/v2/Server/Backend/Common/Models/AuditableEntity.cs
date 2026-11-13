namespace Backend.Common.Models;

public class AuditableEntity
{
    public DateTime CreatedDateTime { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
}