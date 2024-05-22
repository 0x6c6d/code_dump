namespace Application.Features.StoreManager.Events.Models;
public class Event : AuditableEntity
{
    // PK
    public Guid Id { get; set; }

    // Filialnummer
    public string StoreId { get; set; } = string.Empty;

    // Terminar ID
    public int EventId { get; set; }

    // Datum
    public DateOnly Date { get; set; }

    // Uhrzeit
    public TimeOnly Time { get; set; }
}
