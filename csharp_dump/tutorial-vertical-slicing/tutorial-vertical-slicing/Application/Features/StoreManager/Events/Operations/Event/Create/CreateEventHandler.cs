using Application.Features.StoreManager.Events.Respositories;

namespace Application.Features.StoreManager.Events.Operations.Event.Create;
public class CreateEventHandler : IRequestHandler<CreateEventRequest, Guid>
{
    private readonly IEventRepository _eventRepository;

    public CreateEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Guid> Handle(CreateEventRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateEventValidator(_eventRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        var @event = EventMapper.CreateEventRequestToEvent(request);
        @event.CreatedDate = DateTime.Now;
        @event.LastModifiedDate = DateTime.Now;
        @event = await _eventRepository.AddAsync(@event);

        return @event.Id;
    }
}
