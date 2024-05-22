using Application.Features.StoreManager.Events.Models;
using Application.Features.StoreManager.Events.Operations.Event.Create;
using Application.Features.StoreManager.Events.Operations.Event.Read.All;
using Application.Features.StoreManager.Events.Operations.Event.Read.One;
using Application.Features.StoreManager.Events.Operations.Event.Read.PerStore;
using Application.Features.StoreManager.Events.Operations.Event.Update;
using Riok.Mapperly.Abstractions;

namespace Application.Features.StoreManager.Events;
[Mapper]
public static partial class EventMapper
{
    // MediatR
    public static partial GetEventReturn EventToGetEventReturn(Event modelel);

    public static partial List<GetEventsReturn> EventsToGetEventsReturn(IOrderedEnumerable<Event> models);

    public static partial List<GetEventsStoreReturn> EventsToGetEventsStoreReturn(List<Event> models);

    public static partial Event CreateEventRequestToEvent(CreateEventRequest request);

    public static partial void UpdateEventRequestToEvent(UpdateEventRequest request, Event model);

    // Business Logic

    public static partial List<Event> GetEventsReturnToEvents(List<GetEventsReturn> @return);

    public static partial List<Event> GetEventsStoreReturnToEvents(List<GetEventsStoreReturn> @return);

    public static partial Event GetEventReturnToEvent(GetEventReturn @return);

    public static partial CreateEventRequest EventToCreateEventRequest(Event model);

    public static partial UpdateEventRequest EventToUpdateEventRequest(Event model);

    // Frontend
    public static partial Event EventVmToEvent(EventVm model);
}
