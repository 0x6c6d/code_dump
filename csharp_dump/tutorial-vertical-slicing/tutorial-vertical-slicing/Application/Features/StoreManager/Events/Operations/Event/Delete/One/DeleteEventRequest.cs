namespace Application.Features.StoreManager.Events.Operations.Event.Delete.One;
public class DeleteEventRequest : IRequest<Unit>
{
    public Guid Id { get; set; }
}
