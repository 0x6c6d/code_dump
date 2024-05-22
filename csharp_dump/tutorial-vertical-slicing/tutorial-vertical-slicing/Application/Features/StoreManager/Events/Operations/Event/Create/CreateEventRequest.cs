namespace Application.Features.StoreManager.Events.Operations.Event.Create;
public class CreateEventRequest : IRequest<Guid>
{
    public string StoreId { get; set; } = string.Empty;
    public int EventId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
}