namespace Application.Common.Models;
public class AuditableEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
