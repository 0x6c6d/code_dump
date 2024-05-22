using Application.Features.StoreManager.Events.Respositories;

namespace Application.Features.StoreManager.Events.Operations.Event.Delete.PerStore;
public class DeleteEventsStoreHandler : IRequestHandler<DeleteEventsStoreRequest, Unit>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventsStoreHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(DeleteEventsStoreRequest request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetAllByStoreIdAsync(request.StoreId);
        if (events == null)
            throw new NotFoundException(nameof(Event), request.StoreId);

        foreach (var @event in events)
        {
            await _eventRepository.DeleteAsync(@event);
        }

        return Unit.Value;
    }
}
