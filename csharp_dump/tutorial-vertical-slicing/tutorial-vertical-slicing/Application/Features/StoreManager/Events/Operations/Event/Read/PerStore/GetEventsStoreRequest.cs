namespace Application.Features.StoreManager.Events.Operations.Event.Read.PerStore;
public class GetEventsStoreRequest : IRequest<List<GetEventsStoreReturn>>
{
    public string StoreId { get; set; } = string.Empty;
}
