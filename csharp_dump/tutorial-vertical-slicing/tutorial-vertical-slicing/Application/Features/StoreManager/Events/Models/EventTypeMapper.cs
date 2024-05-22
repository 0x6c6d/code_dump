using Application.Features.StoreManager.Events.Models;
using Application.Features.StoreManager.Events.Operations.EventType.Read;
using Riok.Mapperly.Abstractions;

namespace Application.Features.StoreManager.Events;
[Mapper]
public static partial class EventTypeMapper
{
    // MediatR
    public static partial List<GetEventTypesReturn> EventTypesToGetEventTypesReturn(List<EventType> model);

    // Business Logik
    public static partial List<EventType> GetEventTypesReturnToEventTypes(List<GetEventTypesReturn> model);
}
