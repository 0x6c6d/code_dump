namespace Application.Features.StoreManager.Events.Operations.Event.Read.One;
public class GetEventRequest : IRequest<GetEventReturn>
{
    public Guid Id { get; set; }
}
