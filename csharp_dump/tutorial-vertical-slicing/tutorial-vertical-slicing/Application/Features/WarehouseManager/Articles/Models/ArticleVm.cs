using Application.Features.WarehouseManager.Articles.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.WarehouseManager.Articles.Models;
public class ArticleVm
{
    public Guid Id { get; set; } = Guid.Empty;

    [Required(ErrorMessage = "Bitte Namen eingeben.")]
    public string Name { get; set; } = string.Empty;

    public string SerialNumber { get; set; } = string.Empty;

    [GuidNotEmptyValidation(ErrorMessage = "Bitte Gruppe zuordnen.")]
    public Guid GroupId { get; set; }

    [GuidNotEmptyValidation(ErrorMessage = "Bitte Einsatzgebiet zuordnen.")]
    public Guid OperationAreaId { get; set; }

    [GuidNotEmptyValidation(ErrorMessage = "Bitte Lagerort zuordnen.")]
    public Guid StoragePlaceId { get; set; }

    public string? StoragePlace { get; set; }

    public string InventoryNumber { get; set; } = string.Empty;

    [GuidNotEmptyValidation(ErrorMessage = "Bitte Kategorie zuordnen.")]
    public Guid CategoryId { get; set; }

    public Guid? ProcurementId { get; set; }

    [Required(ErrorMessage = "Bitte Menge angeben.")]
    [Range(0, int.MaxValue, ErrorMessage = "Die Menge muss min. 0 betragen.")]
    public int Stock { get; set; } = 0;

    [Required(ErrorMessage = "Bitte Mindestbestand angeben.")]
    [MinStockValidation(nameof(CategoryId))]
    public int MinStock { get; set; } = 0;

    public string ImageUrl { get; set; } = string.Empty;

    public string ShoppingUrl { get; set; } = string.Empty;

    [StoragePlaceInUseValidation(nameof(StoragePlace))]
    public string Store { get; set; } = string.Empty;
}
