using System.ComponentModel.DataAnnotations;

namespace Application.Features.StoreManager.Stores.Models;

public class Store : AuditableEntity
{
    // Filialnummer - PK
    [Key]
    public string StoreId { get; set; } = string.Empty;

    // Filial Beschreibung - Name
    public string? Description { get; set; }
}
