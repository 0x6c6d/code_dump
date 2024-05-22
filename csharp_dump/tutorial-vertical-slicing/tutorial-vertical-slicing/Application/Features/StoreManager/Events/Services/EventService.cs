using Application.Features.StoreManager.Events.Models;
using Application.Features.StoreManager.Events.Operations.Event.Delete.One;
using Application.Features.StoreManager.Events.Operations.Event.Read.All;
using Application.Features.StoreManager.Events.Operations.Event.Read.One;
using Application.Features.StoreManager.Events.Operations.Event.Read.PerStore;
using Application.Features.StoreManager.Events.Operations.EventType.Read;

namespace Application.Features.StoreManager.Events.Services;
public class EventService : IEventService
{
    private readonly IMediator _mediator;

    public EventService(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region EvenType
    public async Task<ServiceResponse<List<EventType>>> GetEventTypesAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetEventTypesRequest());
            if (result == null)
            {
                return new ServiceResponse<List<EventType>>
                {
                    Success = false,
                    Message = "Es ist ein Fehler beim Holen der Terminarten aufgetreten."
                };
            }

            var events = EventTypeMapper.GetEventTypesReturnToEventTypes(result);
            return new ServiceResponse<List<EventType>> { Data = events };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<EventType>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    #endregion

    #region Events
    public async Task<ServiceResponse<List<Event>>> GetEventsAsync()
    {
        try
        {
            var result = await _mediator.Send(new GetEventsRequest());
            if (result == null)
            {
                return new ServiceResponse<List<Event>>
                {
                    Success = false,
                    Message = "Es ist ein Fehler beim Holen der Termine aufgetreten."
                };
            }

            var events = EventMapper.GetEventsReturnToEvents(result);
            return new ServiceResponse<List<Event>> { Data = events };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<Event>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<List<Event>>> GetEventsPerStoreAsync(string storeId)
    {
        try
        {
            var result = await _mediator.Send(new GetEventsStoreRequest() { StoreId = storeId });
            if (result == null)
            {
                return new ServiceResponse<List<Event>>
                {
                    Success = false,
                    Message = $"Es ist ein Fehler beim Holen der Termine für die Filiale {storeId} aufgetreten."
                };
            }

            var events = EventMapper.GetEventsStoreReturnToEvents(result);
            events = events.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();

            return new ServiceResponse<List<Event>> { Data = events };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<List<Event>>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<Event>> GetEventAsync(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetEventRequest { Id = id });
            if (result == null)
            {
                return new ServiceResponse<Event>
                {
                    Success = false,
                    Message = $"Kein Termin mit der Id '{id}' gefunden."
                };
            }

            var @event = EventMapper.GetEventReturnToEvent(result);
            return new ServiceResponse<Event> { Data = @event };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Event>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<Guid>> CreateEventAsync(Event @event)
    {
        try
        {
            var request = EventMapper.EventToCreateEventRequest(@event);
            var result = await _mediator.Send(request);
            if (result == Guid.Empty)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = $"Fehler beim Erstellen des Termines."
                };
            }

            return new ServiceResponse<Guid> { Data = result };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Guid>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<bool>> UpdateEventAsync(Event @event)
    {
        try
        {
            var request = EventMapper.EventToUpdateEventRequest(@event);
            await _mediator.Send(request);

            return new ServiceResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ServiceResponse<bool>> DeleteEventAsync(Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteEventRequest() { Id = id });
            return new ServiceResponse<bool> { Data = true };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    #endregion
}
