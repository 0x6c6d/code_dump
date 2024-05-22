using Application.Features.StoreManager.Events.Respositories;

namespace Application.Features.StoreManager.Events.Operations.Event.Read.One;
public class GetEventHandler : IRequestHandler<GetEventRequest, GetEventReturn>
{
    private readonly IEventRepository _eventRepository;

    public GetEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<GetEventReturn> Handle(GetEventRequest request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);

        if (@event == null)
            throw new NotFoundException(nameof(Event), request.Id);

        var eventReturn = EventMapper.EventToGetEventReturn(@event);

        return eventReturn;
    }
}
