namespace Application.Features.StoreManager.Events.Operations.Event.Delete.PerStore;
public class DeleteEventsStoreRequest : IRequest<Unit>
{
    public string StoreId { get; set; } = string.Empty;
}
