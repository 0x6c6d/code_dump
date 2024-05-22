
using Application.Features.StoreManager.Events.Models;

namespace Application.Features.StoreManager.Events.Services;

public interface IEventService
{
    Task<ServiceResponse<List<EventType>>> GetEventTypesAsync();
    Task<ServiceResponse<Guid>> CreateEventAsync(Event @event);
    Task<ServiceResponse<bool>> DeleteEventAsync(Guid id);
    Task<ServiceResponse<Event>> GetEventAsync(Guid id);
    Task<ServiceResponse<List<Event>>> GetEventsAsync();
    Task<ServiceResponse<List<Event>>> GetEventsPerStoreAsync(string storeId);
    Task<ServiceResponse<bool>> UpdateEventAsync(Event @event);
}