using Application.Features.StoreManager.Events.Respositories;

namespace Application.Features.StoreManager.Events.Operations.Event.Delete.One;
public class DeleteEventHandler : IRequestHandler<DeleteEventRequest, Unit>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(DeleteEventRequest request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id);
        if (@event == null)
            throw new NotFoundException(nameof(Event), request.Id);

        await _eventRepository.DeleteAsync(@event);

        return Unit.Value;
    }
}
