using Application.Features.StoreManager.Events.Respositories;

namespace Application.Features.StoreManager.Events.Operations.Event.Update;
public class UpdateEventHandler : IRequestHandler<UpdateEventRequest, Unit>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(UpdateEventRequest request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);
        if (@event == null)
            throw new NotFoundException(nameof(Event), request.StoreId);

        var validator = new UpdateEventValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);

        EventMapper.UpdateEventRequestToEvent(request, @event);
        @event.LastModifiedDate = DateTime.Now;
        await _eventRepository.UpdateAsync(@event);

        return Unit.Value;
    }
}
