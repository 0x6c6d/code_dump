using Application.Features.StoreManager.Events.ValidationAttributes;
using Application.Features.StoreManager.Stores.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.StoreManager.Events.Models;
public class EventVm
{
    public Guid Id { get; set; }

    [StoreIdFormatValidation(ErrorMessage = "Die Filialnummer ist ungültig")]
    public string StoreId { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Bitte wählen Sie eine Terminart durch.")]
    public int EventId { get; set; }

    [DateValidation(ErrorMessage = "Das Datum muss in der Zukunft liegen")]
    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }
}
