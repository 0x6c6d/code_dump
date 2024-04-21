using System.ComponentModel.DataAnnotations;
using Web.Helpers.ValidationAttributes;

namespace Web.Models.ViewModels;

public class ArticleVm
{
    public Guid ArticleId { get; set; } = Guid.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string ItemNumber { get; set; } = string.Empty;

    [GuidNotEmpty]
    public Guid GroupId { get; set; }

    [GuidNotEmpty]
    public Guid OperationAreaId { get; set; }

    [GuidNotEmpty]
    public Guid StoragePlaceId { get; set; }

    [Required]
    public string StorageBin { get; set; } = string.Empty;

    [GuidNotEmpty]
    public Guid ProcurementId { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Die Menge muss min. 0 betragen.")]
    public int Stock { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Der Mindestbestand muss min. 1 betragen.")]
    public int MinStock { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string ShoppingUrl { get; set; } = string.Empty;
}
