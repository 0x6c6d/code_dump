using Application.Features.StoreManager.Events.Respositories;

namespace Application.Features.StoreManager.Events.Operations.Event.Read.All;
public class GetEventsHandler : IRequestHandler<GetEventsRequest, List<GetEventsReturn>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventsHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<List<GetEventsReturn>> Handle(GetEventsRequest request, CancellationToken cancellationToken)
    {
        var events = (await _eventRepository.GetAllAsync()).OrderBy(u => u.StoreId);

        var eventsReturn = EventMapper.EventsToGetEventsReturn(events);

        return eventsReturn;
    }
}
