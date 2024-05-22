using Application.Features.WarehouseManager.Categories.Models;
using Application.Features.WarehouseManager.Groups.Models;
using Application.Features.WarehouseManager.OperationAreas.Models;
using Application.Features.WarehouseManager.Procurements.Models;
using Application.Features.WarehouseManager.StoragePlaces.Models;

namespace Application.Features.WarehouseManager.Articles.Models;
public class Article : AuditableEntity
{
    // Artikel ID
    public Guid Id { get; set; }

    // Artikelname
    public string Name { get; set; } = string.Empty;

    // Seriennummer
    public string SerialNumber { get; set; } = string.Empty;

    // Inventarnummer
    public string InventoryNumber { get; set; } = string.Empty;

    // Filiale
    public string Store { get; set; } = string.Empty;

    // Bild URL
    public string ImageUrl { get; set; } = string.Empty;

    // Shopping URL
    public string ShoppingUrl { get; set; } = string.Empty;

    // Bestand
    public int Stock { get; set; }

    // Mindestbestand
    public int MinStock { get; set; }

    // Artikelgruppe
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = new();

    // Oberkategorie
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = new();

    // Einsatzgebiet
    public Guid OperationAreaId { get; set; }
    public OperationArea OperationArea { get; set; } = new();

    // Lagerplatz
    public Guid StoragePlaceId { get; set; }
    public StoragePlace StoragePlace { get; set; } = new();

    // Lieferant
    public Guid? ProcurementId { get; set; }
    public Procurement? Procurement { get; set; }

    // Gelöscht
    public DateTime? Disposed { get; set; }
}
