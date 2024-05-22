namespace Application.Features.StoreManager.Events.Operations.Event.Read.All;
public class GetEventsReturn
{
    public Guid Id { get; set; }
    public string StoreId { get; set; } = string.Empty;
    public int EventId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}
