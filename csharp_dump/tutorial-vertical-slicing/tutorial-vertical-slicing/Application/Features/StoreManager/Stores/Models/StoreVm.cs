using Application.Features.StoreManager.Stores.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.StoreManager.Stores.Models;
public class StoreVm
{
    [Required(ErrorMessage = "Bitte geben Sie eine Filialnummer an")]
    [StoreIdFormatValidation(ErrorMessage = "Das Format der Filialnummer ist fehlerhaft")]
    public string StoreId { get; set; } = string.Empty;
}
