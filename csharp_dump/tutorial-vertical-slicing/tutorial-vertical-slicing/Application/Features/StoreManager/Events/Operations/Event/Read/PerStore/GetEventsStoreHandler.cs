using Application.Features.StoreManager.Events.Respositories;

namespace Application.Features.StoreManager.Events.Operations.Event.Read.PerStore;
public class GetEventsStoreHandler : IRequestHandler<GetEventsStoreRequest, List<GetEventsStoreReturn>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventsStoreHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<List<GetEventsStoreReturn>> Handle(GetEventsStoreRequest request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetAllByStoreIdAsync(request.StoreId);

        if (events == null)
            throw new NotFoundException(nameof(Event), request.StoreId);

        var eventsReturn = EventMapper.EventsToGetEventsStoreReturn(events);

        return eventsReturn;
    }
}
