using Application.Features.StoreManager.Events.Respositories;

namespace Application.Features.StoreManager.Events.Operations.EventType.Read;
public class GetEventTypesHandler : IRequestHandler<GetEventTypesRequest, List<GetEventTypesReturn>>
{
    private readonly IEventTypeRepository _eventTypeRepository;

    public GetEventTypesHandler(IEventTypeRepository eventTypeRepository)
    {
        _eventTypeRepository = eventTypeRepository;
    }

    public async Task<List<GetEventTypesReturn>> Handle(GetEventTypesRequest request, CancellationToken cancellationToken)
    {
        var eventTypes = (await _eventTypeRepository.GetAllAsync()).OrderBy(x => x.EventName).ToList();

        var eventsReturn = EventTypeMapper.EventTypesToGetEventTypesReturn(eventTypes);

        return eventsReturn;
    }
}
